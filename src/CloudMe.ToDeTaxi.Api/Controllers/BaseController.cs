using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using System;
using System.Linq;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Api.Models;
using System.Collections.Generic;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using System.Net;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [EnableCors("AllowOrigin")] 
    [Authorize] 
    public class BaseController : Controller
    {

        private IUnitOfWork _unitOfWork;
        public IUnitOfWork unitOfWork { get {return _unitOfWork;}}

        public BaseController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        protected bool HasNotifications(INotifiable notifiable)
        {
            return notifiable.Notifications.Any();
        } 

        protected async Task<Response<T>> ResponseAsync<T>(T result, INotifiable notifiable)
        {
            if (!notifiable.Notifications.Any())
            {
                try
                {
                    if (await _unitOfWork.CommitAsync())
                    {
                        return new Response<T>()
                        {
                            data = result,
                            success = true
                        };
                    }
                    else
                    {
                        return new Response<T>()
                        {
                            success = false,
                            notifications = _unitOfWork.Notifications
                        };
                    }

                    //return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    // Aqui devo logar o erro
                    return new Response<T>()
                    {
                        success = false,
                        notifications = new List<Notification> 
                        {
                            new Notification("TransactionError", ex.Message)
                        }
                    };
                }
            }
            else
            {
                return new Response<T>()
                {
                    success = false,
                    notifications = notifiable.Notifications
                };
            }
        }

        protected async Task<Response<T>> ErrorResponseAsync<T>(INotifiable notifiable, HttpStatusCode code = HttpStatusCode.OK)
        {
            return new Response<T>()
            {
                success = false,
                notifications = notifiable.Notifications,
                responseCode = code
            };
        }

        protected async Task<Response<T>> ResponseExceptionAsync<T>(Exception ex)
        {
            return new Response<T>()
            {
                success = false,
                notifications = new List<Notification> 
                {
                    new Notification("TransactionError", ex.Message)
                }
            };
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected async Task<Usuario> GetRequestUser(IUsuarioService usuarioService)
        {
            if (Guid.TryParse(User.FindFirst("sub")?.Value, out Guid usrId))
            {
                return await usuarioService.Get(usrId);
            }

            return null;
        }
    }
}
