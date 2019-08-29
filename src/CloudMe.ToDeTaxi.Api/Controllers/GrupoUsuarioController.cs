using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class GrupoUsuarioController : BaseController
    {
        IGrupoUsuarioService _grupoUsuarioService;

        public GrupoUsuarioController(IGrupoUsuarioService grupoUsuarioService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _grupoUsuarioService = grupoUsuarioService;
        }

        /// <summary>
        /// Gets all grupoUsuarios.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GrupoUsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _grupoUsuarioService.GetAllSummariesAsync(), _grupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a GrupoUsuario.
        /// <param name="id">GrupoUsuario's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GrupoUsuarioSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _grupoUsuarioService.GetSummaryAsync(id), _grupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new GrupoUsuario.
        /// </summary>
        /// <param name="grupoUsuarioSummary">GrupoUsuario's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] GrupoUsuarioSummary grupoUsuarioSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._grupoUsuarioService.CreateAsync(grupoUsuarioSummary) != null, _grupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing GrupoUsuario.
        /// </summary>
        /// <param name="grupoUsuarioSummary">Modified GrupoUsuario list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] GrupoUsuarioSummary grupoUsuarioSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._grupoUsuarioService.UpdateAsync(grupoUsuarioSummary) != null, _grupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing GrupoUsuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._grupoUsuarioService.DeleteAsync(id), _grupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}