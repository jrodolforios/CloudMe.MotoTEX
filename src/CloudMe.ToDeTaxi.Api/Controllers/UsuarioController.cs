using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using System.Collections.Generic;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Configuration.Library.Constants;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    [ApiController, Route("api/v1/[controller]")]
    public class UsuarioController : BaseController
    {
        IUsuarioService _UsuarioService;

        public UsuarioController(IUsuarioService UsuarioService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _UsuarioService = UsuarioService;
        }

        /// <summary>
        /// Gets all Usuarios.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(string search, int? page=1, int? pageSize=10)
        {
            try
            {
                return await base.ResponseAsync(await _UsuarioService.GetAllSummariesAsync(), _UsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Usuario.
        /// <param name="id">Usuario's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _UsuarioService.GetSummaryAsync(id), _UsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Usuario.
        /// </summary>
        /// <param name="UsuarioSummary">Usuario's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] UsuarioSummary UsuarioSummary)
        {
            try
            {
                return await base.ResponseAsync((await this._UsuarioService.CreateAsync(UsuarioSummary)).Id, _UsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Usuario.
        /// </summary>
        /// <param name="UsuarioSummary">Modified Usuario list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] UsuarioSummary UsuarioSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._UsuarioService.UpdateAsync(UsuarioSummary) != null, _UsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Usuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._UsuarioService.DeleteAsync(id), _UsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}