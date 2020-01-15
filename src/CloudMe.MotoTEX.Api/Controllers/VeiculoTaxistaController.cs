using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class VeiculoTaxistaController : BaseController
    {
        IVeiculoTaxistaService _VeiculoTaxistaService;

        public VeiculoTaxistaController(IVeiculoTaxistaService VeiculoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _VeiculoTaxistaService = VeiculoTaxistaService;
        }

        /// <summary>
        /// Gets all VeiculoTaxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<VeiculoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<VeiculoTaxistaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _VeiculoTaxistaService.GetAllSummariesAsync(), _VeiculoTaxistaService);
        }

        /// <summary>
        /// Gets a VeiculoTaxista.
        /// <param name="id">VeiculoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<VeiculoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<VeiculoTaxistaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _VeiculoTaxistaService.GetSummaryAsync(id), _VeiculoTaxistaService);
        }

        /// <summary>
        /// Creates a new VeiculoTaxista.
        /// </summary>
        /// <param name="VeiculoTaxistaSummary">VeiculoTaxista's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] VeiculoTaxistaSummary VeiculoTaxistaSummary)
        {
            var entity = await this._VeiculoTaxistaService.CreateAsync(VeiculoTaxistaSummary);
            if (_VeiculoTaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_VeiculoTaxistaService);
            }
            return await base.ResponseAsync(entity.Id, _VeiculoTaxistaService);
        }

        /// <summary>
        /// Modifies an existing VeiculoTaxista.
        /// </summary>
        /// <param name="VeiculoTaxistaSummary">Modified VeiculoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] VeiculoTaxistaSummary VeiculoTaxistaSummary)
        {
            return await base.ResponseAsync(await this._VeiculoTaxistaService.UpdateAsync(VeiculoTaxistaSummary) != null, _VeiculoTaxistaService);
        }

        /// <summary>
        /// Removes an existing VeiculoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._VeiculoTaxistaService.DeleteAsync(id), _VeiculoTaxistaService);
        }


        /// <summary>
        /// Get by IdUser
        /// </summary>
        /// <param name="id">User Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpGet("consulta_veiculos_de_taxistas/{id}")]
        [ProducesResponseType(typeof(Response<List<VeiculoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<List<VeiculoTaxistaSummary>>> ConsultaVeiculosDeTaxistas(Guid id)
        {
            return await base.ResponseAsync(await _VeiculoTaxistaService.ConsultaVeiculosDeTaxista(id), _VeiculoTaxistaService);
        }
    }
}
