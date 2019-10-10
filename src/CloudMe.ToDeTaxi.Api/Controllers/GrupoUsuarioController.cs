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
    public class GrupoUsuarioController : BaseController
    {
        IGrupoUsuarioService _GrupoUsuarioService;

        public GrupoUsuarioController(IGrupoUsuarioService GrupoUsuarioService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _GrupoUsuarioService = GrupoUsuarioService;
        }

        /// <summary>
        /// Gets all GrupoUsuarios.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<GrupoUsuarioSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<GrupoUsuarioSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _GrupoUsuarioService.GetAllSummariesAsync(), _GrupoUsuarioService);
        }

        /// <summary>
        /// Gets a GrupoUsuario.
        /// <param name="id">GrupoUsuario's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<GrupoUsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<GrupoUsuarioSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _GrupoUsuarioService.GetSummaryAsync(id), _GrupoUsuarioService);
        }

        /// <summary>
        /// Creates a new GrupoUsuario.
        /// </summary>
        /// <param name="GrupoUsuarioSummary">GrupoUsuario's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] GrupoUsuarioSummary GrupoUsuarioSummary)
        {
            var entity = await this._GrupoUsuarioService.CreateAsync(GrupoUsuarioSummary);
            if (_GrupoUsuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_GrupoUsuarioService);
            }
            return await base.ResponseAsync(entity.Id, _GrupoUsuarioService);
        }

        /// <summary>
        /// Modifies an existing GrupoUsuario.
        /// </summary>
        /// <param name="GrupoUsuarioSummary">Modified GrupoUsuario list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] GrupoUsuarioSummary GrupoUsuarioSummary)
        {
            return await base.ResponseAsync(await this._GrupoUsuarioService.UpdateAsync(GrupoUsuarioSummary) != null, _GrupoUsuarioService);
        }

        /// <summary>
        /// Removes an existing GrupoUsuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._GrupoUsuarioService.DeleteAsync(id), _GrupoUsuarioService);
        }
    }
}