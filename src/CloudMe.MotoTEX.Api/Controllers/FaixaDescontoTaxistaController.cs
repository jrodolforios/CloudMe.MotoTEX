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
    public class FaixaDescontoTaxistaController : BaseController
    {
        IFaixaDescontoTaxistaService _FaixaDescontoTaxistaService;

        public FaixaDescontoTaxistaController(IFaixaDescontoTaxistaService FaixaDescontoTaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FaixaDescontoTaxistaService = FaixaDescontoTaxistaService;
        }

        /// <summary>
        /// Gets all FaixaDescontoTaxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FaixaDescontoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaixaDescontoTaxistaSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FaixaDescontoTaxistaService.GetAllSummariesAsync(), _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Gets a FaixaDescontoTaxista.
        /// <param name="id">FaixaDescontoTaxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FaixaDescontoTaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FaixaDescontoTaxistaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FaixaDescontoTaxistaService.GetSummaryAsync(id), _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Creates a new FaixaDescontoTaxista.
        /// </summary>
        /// <param name="FaixaDescontoTaxistaSummary">FaixaDescontoTaxista's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FaixaDescontoTaxistaSummary FaixaDescontoTaxistaSummary)
        {
            var entity = await this._FaixaDescontoTaxistaService.CreateAsync(FaixaDescontoTaxistaSummary);
            if (_FaixaDescontoTaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FaixaDescontoTaxistaService);
            }
            return await base.ResponseAsync(entity.Id, _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Modifies an existing FaixaDescontoTaxista.
        /// </summary>
        /// <param name="FaixaDescontoTaxistaSummary">Modified FaixaDescontoTaxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FaixaDescontoTaxistaSummary FaixaDescontoTaxistaSummary)
        {
            return await base.ResponseAsync(await this._FaixaDescontoTaxistaService.UpdateAsync(FaixaDescontoTaxistaSummary) != null, _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Removes an existing FaixaDescontoTaxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(
            [FromServices]IFaixaDescontoTaxistaService faixaDescontoTaxistaService,
            Guid id)
        {
            // remove associações com taxistas
            var taxistasFxDesc = await faixaDescontoTaxistaService.Search(txFxDesc => txFxDesc.IdFaixaDesconto == id);
            foreach (var txFxDesc in taxistasFxDesc)
            {
                await faixaDescontoTaxistaService.DeleteAsync(txFxDesc.Id);
                if (faixaDescontoTaxistaService.IsInvalid())
                {
                    return await ErrorResponseAsync<bool>(faixaDescontoTaxistaService);
                }
            }

            return await base.ResponseAsync(await this._FaixaDescontoTaxistaService.DeleteAsync(id), _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Get by IdTaxista
        /// </summary>
        /// <param name="id">Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpGet("consulta_id_taxista/{id}")]
        [ProducesResponseType(typeof(Response<IEnumerable<FaixaDescontoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaixaDescontoTaxistaSummary>>> GetByUserId(Guid id)
        {
            return await base.ResponseAsync(await _FaixaDescontoTaxistaService.GetByTaxistId(id), _FaixaDescontoTaxistaService);
        }

        /// <summary>
        /// Get by IdTaxista
        /// </summary>
        /// <param name="id">Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpDelete("Deletar_por_taxista/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> DeletePorTaxista(Guid id)
        {
            return await base.ResponseAsync(await _FaixaDescontoTaxistaService.DeleteByTaxistId(id), _FaixaDescontoTaxistaService);
        }
    }
}