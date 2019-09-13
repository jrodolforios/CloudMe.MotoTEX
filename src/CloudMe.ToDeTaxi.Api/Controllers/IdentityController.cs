using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class IdentityController : BaseController
    {
        public IdentityController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [AllowAnonymous]
        [HttpGet("error")]
        public async Task<object> error(
            string errorId,
            [FromServices] IIdentityServerInteractionService identity)
        {
            return await identity.GetErrorContextAsync(errorId);
        }
    }
}
