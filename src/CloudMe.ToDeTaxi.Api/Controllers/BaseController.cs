using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
     [Authorize, EnableCors("AllowOrigin")] 
    public class BaseController : Controller
    {

        private IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        protected async Task<IActionResult> ResponseAsync(object result, INotifiable notifiable)
        {

            if (!notifiable.Notifications.Any())
            {
                try
                {
                    if (await _unitOfWork.CommitAsync())
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(new { errors = _unitOfWork.Notifications });
                    }

                    //return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception ex)
                {
                    // Aqui devo logar o erro
                    return BadRequest($"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista. Erro interno: {ex.Message}");
                    //return Request.CreateResponse(HttpStatusCode.Conflict, $"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista. Erro interno: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(new { errors = notifiable.Notifications });
                //return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = serviceBase.Notifications });
            }
        }
        protected async Task<IActionResult> ResponseExceptionAsync(Exception ex)
        {
            return await Task.FromResult(BadRequest(new { errors = ex.Message, exception = ex.ToString() }));
            //return Request.CreateResponse(HttpStatusCode.InternalServerError, new { errors = ex.Message, exception = ex.ToString() });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
