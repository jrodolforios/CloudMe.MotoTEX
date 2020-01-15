using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class RotaController : BaseController
    {
        IRotaService _RotaService;

        public RotaController(IRotaService RotaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _RotaService = RotaService;
        }

        /// <summary>
        /// Gets all Rotas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<RotaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<RotaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _RotaService.GetAllSummariesAsync(), _RotaService);
        }

        /// <summary>
        /// Gets a Rota.
        /// <param name="id">Rota's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<RotaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<RotaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _RotaService.GetSummaryAsync(id), _RotaService);
        }

        /// <summary>
        /// Creates a new Rota.
        /// </summary>
        /// <param name="RotaSummary">Rota's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] RotaSummary RotaSummary)
        {
            var entity = await this._RotaService.CreateAsync(RotaSummary);
            if (_RotaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_RotaService);
            }
            return await base.ResponseAsync(entity.Id, _RotaService);
        }

        /// <summary>
        /// Modifies an existing Rota.
        /// </summary>
        /// <param name="RotaSummary">Modified Rota list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] RotaSummary RotaSummary)
        {
            return await base.ResponseAsync(await this._RotaService.UpdateAsync(RotaSummary) != null, _RotaService);
        }

        /// <summary>
        /// Removes an existing Rota.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._RotaService.DeleteAsync(id), _RotaService);
        }
    }
}