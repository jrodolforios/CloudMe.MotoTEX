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

        protected async Task<Response<T>> ErrorResponseAsync<T>(INotifiable notifiable)
        {
            return new Response<T>()
            {
                success = false,
                notifications = notifiable.Notifications
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
    }
}
