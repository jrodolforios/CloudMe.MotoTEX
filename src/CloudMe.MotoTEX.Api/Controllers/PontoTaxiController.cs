using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;
using prmToolkit.NotificationPattern;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class PontoTaxiController : BaseController
    {
        IPontoTaxiService _PontoTaxiService;
        IEnderecoService _enderecoService;

        public PontoTaxiController(
            IPontoTaxiService pontoTaxiService,
            IEnderecoService localizacaoService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _PontoTaxiService = pontoTaxiService;
            _enderecoService = localizacaoService;
        }

        /// <summary>
        /// Gets all PontoTaxis.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<PontoTaxiSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<PontoTaxiSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _PontoTaxiService.GetAllSummariesAsync(), _PontoTaxiService);
        }

        /// <summary>
        /// Gets a PontoTaxi.
        /// <param name="id">PontoTaxi's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<PontoTaxiSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<PontoTaxiSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _PontoTaxiService.GetSummaryAsync(id), _PontoTaxiService);
        }

        /// <summary>
        /// Obtém os taxistas do ponto de taxi.
        /// <param name="id">ID do ponto de taxi</param>
        /// </summary>
        [HttpGet("{id}/taxistas")]
        [ProducesResponseType(typeof(Response<IEnumerable<TaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<TaxistaSummary>>> GetTaxistas([FromServices]ITaxistaService taxistaService, Guid id)
        {
            return await ResponseAsync(
                await taxistaService.GetAllSummariesAsync(
                    taxistaService.Search(tx => tx.IdPontoTaxi == id, new[] { "PontoTaxi" })),
                    taxistaService);
        }

        /// <summary>
        /// Creates a new PontoTaxi.
        /// </summary>
        /// <param name="pontoTaxiSummary">PontoTaxi's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] PontoTaxiSummary pontoTaxiSummary)
        {
            // cria o endereço do pontoTaxi
            var endereco = await this._enderecoService.CreateAsync(pontoTaxiSummary.Endereco);
            if (_enderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_enderecoService);
            }

            pontoTaxiSummary.Endereco.Id = endereco.Id;

            // cria o registro do pontoTaxi
            var pontoTaxi = await this._PontoTaxiService.CreateAsync(pontoTaxiSummary);
            if (_PontoTaxiService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_PontoTaxiService);
            }

            return await base.ResponseAsync(pontoTaxi.Id, _PontoTaxiService);
        }

        /// <summary>
        /// Modifies an existing PontoTaxi.
        /// </summary>
        /// <param name="pontoTaxiSummary">Modified PontoTaxi list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] PontoTaxiSummary pontoTaxiSummary)
        {
            // atualiza o registro do pontoTaxi
            return await base.ResponseAsync(await _PontoTaxiService.UpdateAsync(pontoTaxiSummary) != null, _PontoTaxiService);
        }

        /// <summary>
        /// Removes an existing PontoTaxi.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(
            [FromServices] ITaxistaService taxistaService,
            Guid id)
        {
            // OBS.: Qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback (vide método POST).

            var pontoTaxiSummary = await this._PontoTaxiService.GetSummaryAsync(id);
            if (pontoTaxiSummary.Id == Guid.Empty)
            {
                _PontoTaxiService.AddNotification(new Notification("Ponto de táxi", "Ponto de táxi não encontrado"));
            }

            // remove associações com os taxistas
            var taxistasSummaries = await taxistaService.GetAllSummariesAsync(
                    taxistaService.Search(tx => tx.IdPontoTaxi == pontoTaxiSummary.Id)
                );

            foreach (var txSummary in taxistasSummaries)
            {
                txSummary.IdPontoTaxi = null;
                await taxistaService.UpdateAsync(txSummary);
                if (taxistaService.IsInvalid())
                {
                    return await ErrorResponseAsync<bool>(taxistaService);
                }
            }

            // primeiro, remove o registro do pontoTaxi
            await this._PontoTaxiService.DeleteAsync(pontoTaxiSummary.Id);
            if (_PontoTaxiService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_PontoTaxiService);
            }

            // remove o registro de endereço
            await this._enderecoService.DeleteAsync(pontoTaxiSummary.Endereco.Id);
            if (_enderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_enderecoService);
            }

            return await base.ResponseAsync(true, unitOfWork);
        }
    }
}

