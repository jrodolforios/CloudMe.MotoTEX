using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class UsuarioGrupoUsuarioController : BaseController
    {
        IUsuarioGrupoUsuarioService _UsuarioGrupoUsuarioService;

        public UsuarioGrupoUsuarioController(IUsuarioGrupoUsuarioService UsuarioGrupoUsuarioService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _UsuarioGrupoUsuarioService = UsuarioGrupoUsuarioService;
        }

        /// <summary>
        /// Gets all UsuarioGrupoUsuarios.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<UsuarioGrupoUsuarioSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<UsuarioGrupoUsuarioSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _UsuarioGrupoUsuarioService.GetAllSummariesAsync(), _UsuarioGrupoUsuarioService);
        }

        /// <summary>
        /// Gets a UsuarioGrupoUsuario.
        /// <param name="id">UsuarioGrupoUsuario's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<UsuarioGrupoUsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<UsuarioGrupoUsuarioSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _UsuarioGrupoUsuarioService.GetSummaryAsync(id), _UsuarioGrupoUsuarioService);
        }

        /// <summary>
        /// Creates a new UsuarioGrupoUsuario.
        /// </summary>
        /// <param name="UsuarioGrupoUsuarioSummary">UsuarioGrupoUsuario's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] UsuarioGrupoUsuarioSummary UsuarioGrupoUsuarioSummary)
        {
            var entity = await this._UsuarioGrupoUsuarioService.CreateAsync(UsuarioGrupoUsuarioSummary);
            if (_UsuarioGrupoUsuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_UsuarioGrupoUsuarioService);
            }
            return await base.ResponseAsync(entity.Id, _UsuarioGrupoUsuarioService);
        }

        /// <summary>
        /// Modifies an existing UsuarioGrupoUsuario.
        /// </summary>
        /// <param name="UsuarioGrupoUsuarioSummary">Modified UsuarioGrupoUsuario list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] UsuarioGrupoUsuarioSummary UsuarioGrupoUsuarioSummary)
        {
            return await base.ResponseAsync(await this._UsuarioGrupoUsuarioService.UpdateAsync(UsuarioGrupoUsuarioSummary) != null, _UsuarioGrupoUsuarioService);
        }

        /// <summary>
        /// Removes an existing UsuarioGrupoUsuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._UsuarioGrupoUsuarioService.DeleteAsync(id), _UsuarioGrupoUsuarioService);
        }
    }
}