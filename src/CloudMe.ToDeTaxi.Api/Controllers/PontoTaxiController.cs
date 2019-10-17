using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;

namespace CloudMe.ToDeTaxi.Api.Controllers
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
        /// Creates a new PontoTaxi.
        /// </summary>
        /// <param name="pontoTaxiSummary">PontoTaxi's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<PontoTaxiSummary>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<PontoTaxiSummary>> Post([FromBody] PontoTaxiSummary pontoTaxiSummary)
        {
            // cria o endereço do pontoTaxi
            var endereco = await this._enderecoService.CreateAsync(pontoTaxiSummary.Endereco);
            if (_enderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PontoTaxiSummary>(_enderecoService);
            }

            pontoTaxiSummary.Endereco.Id = endereco.Id;

            // cria o registro do pontoTaxi
            var pontoTaxi = await this._PontoTaxiService.CreateAsync(pontoTaxiSummary);
            if (_PontoTaxiService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PontoTaxiSummary>(_PontoTaxiService);
            }

            return await base.ResponseAsync(await _PontoTaxiService.GetSummaryAsync(pontoTaxi.Id), _PontoTaxiService);
        }

        /// <summary>
        /// Modifies an existing PontoTaxi.
        /// </summary>
        /// <param name="pontoTaxiSummary">Modified PontoTaxi list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<PontoTaxiSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<PontoTaxiSummary>> Put([FromBody] PontoTaxiSummary pontoTaxiSummary)
        {
            // OBS.: Qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback (vide método POST).

            var pontoTaxi = await this._PontoTaxiService.Get(pontoTaxiSummary.Id);

            // atualiza o registro do pontoTaxi
            await this._PontoTaxiService.UpdateAsync(pontoTaxiSummary);
            if (_PontoTaxiService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PontoTaxiSummary>(_PontoTaxiService);
            }

            if (pontoTaxiSummary.Endereco != null)
            {
                // atualiza o registro de endereço
                await this._enderecoService.UpdateAsync(pontoTaxiSummary.Endereco);
                if (_enderecoService.IsInvalid())
                {
                    return await base.ErrorResponseAsync<PontoTaxiSummary>(_enderecoService);
                }
            }

            return await base.ResponseAsync(await _PontoTaxiService.GetSummaryAsync(pontoTaxi.Id), unitOfWork);
        }

        /// <summary>
        /// Removes an existing PontoTaxi.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            // OBS.: Qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback (vide método POST).

            var pontoTaxiSummary = await this._PontoTaxiService.GetSummaryAsync(id);

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

