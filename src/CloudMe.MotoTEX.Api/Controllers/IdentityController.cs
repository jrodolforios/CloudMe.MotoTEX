using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;

namespace CloudMe.MotoTEX.Api.Controllers
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
