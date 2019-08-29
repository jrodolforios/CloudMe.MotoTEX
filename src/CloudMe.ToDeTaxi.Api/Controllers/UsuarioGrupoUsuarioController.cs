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
    public class UsuarioGrupoUsuarioController : BaseController
    {
        IUsuarioGrupoUsuarioService _usuarioGrupoUsuarioService;

        public UsuarioGrupoUsuarioController(IUsuarioGrupoUsuarioService usuarioGrupoUsuarioService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _usuarioGrupoUsuarioService = usuarioGrupoUsuarioService;
        }

        /// <summary>
        /// Gets all usuarioGrupoUsuarios.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioGrupoUsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _usuarioGrupoUsuarioService.GetAllSummariesAsync(), _usuarioGrupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a UsuarioGrupoUsuario.
        /// <param name="id">UsuarioGrupoUsuario's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioGrupoUsuarioSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _usuarioGrupoUsuarioService.GetSummaryAsync(id), _usuarioGrupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new UsuarioGrupoUsuario.
        /// </summary>
        /// <param name="usuarioGrupoUsuarioSummary">UsuarioGrupoUsuario's summary</param>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] UsuarioGrupoUsuarioSummary usuarioGrupoUsuarioSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._usuarioGrupoUsuarioService.CreateAsync(usuarioGrupoUsuarioSummary) != null, _usuarioGrupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing UsuarioGrupoUsuario.
        /// </summary>
        /// <param name="usuarioGrupoUsuarioSummary">Modified UsuarioGrupoUsuario list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] UsuarioGrupoUsuarioSummary usuarioGrupoUsuarioSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._usuarioGrupoUsuarioService.UpdateAsync(usuarioGrupoUsuarioSummary) != null, _usuarioGrupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing UsuarioGrupoUsuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._usuarioGrupoUsuarioService.DeleteAsync(id), _usuarioGrupoUsuarioService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}