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
using System.Linq;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class LocalizacaoController : BaseController
    {
        ILocalizacaoService _LocalizacaoService;
        ITaxistaService _taxistaService;

        public LocalizacaoController(ILocalizacaoService LocalizacaoService, ITaxistaService TaxistaService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _LocalizacaoService = LocalizacaoService;
            _taxistaService = TaxistaService;
        }

        /// <summary>
        /// Gets all Localizacaos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<LocalizacaoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<LocalizacaoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _LocalizacaoService.GetAllSummariesAsync(), _LocalizacaoService);
        }

        /// <summary>
        /// Gets a Localizacao.
        /// <param name="id">Localizacao's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<LocalizacaoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<LocalizacaoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _LocalizacaoService.GetSummaryAsync(id), _LocalizacaoService);
        }

        /// <summary>
        /// Creates a new Localizacao.
        /// </summary>
        /// <param name="LocalizacaoSummary">Localizacao's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] LocalizacaoSummary LocalizacaoSummary)
        {
            var entity = await this._LocalizacaoService.CreateAsync(LocalizacaoSummary);
            if (_LocalizacaoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_LocalizacaoService);
            }
            return await base.ResponseAsync(entity.Id, _LocalizacaoService);
        }

        /// <summary>
        /// Get by IdUser
        /// </summary>
        /// <returns>passenger</returns>
        [HttpGet("get_qt_taxistas_online")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<Response<int>> GetQtTaxistasOnline()
        {
            return await base.ResponseAsync(await _LocalizacaoService.GetQtTaxistasOnline(), _LocalizacaoService);
        }

        /// <summary>
        /// Get by IdUser
        /// </summary>
        /// <returns>passenger</returns>
        [HttpGet("get_localizacao_usuario")]
        [ProducesResponseType(typeof(Response<LocalizacaoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<LocalizacaoSummary>> GetLocalizacaoUsuario(Guid IdUsuario)
        {
            var localizacao = (await _LocalizacaoService.Search(x => x.IdUsuario == IdUsuario)).FirstOrDefault();

            var localizacaoSummary = new LocalizacaoSummary()
            {
                Endereco = localizacao.Endereco,
                Id = localizacao.Id,
                IdUsuario = localizacao.IdUsuario,
                Latitude = localizacao.Latitude,
                Longitude = localizacao.Longitude,
                NomePublico = localizacao.NomePublico
            };

            return await base.ResponseAsync(localizacaoSummary, _LocalizacaoService); ;
        }

        /// <summary>
        /// Get by IdUser
        /// </summary>
        /// <returns>passenger</returns>
        [HttpGet("get_localizacao_taxista")]
        [ProducesResponseType(typeof(Response<LocalizacaoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<LocalizacaoSummary>> GetLocalizacaoTaxista(Guid IdTaxista)
        {
            var taxista = await _taxistaService.GetSummaryAsync(IdTaxista);
            var localizacao = (await _LocalizacaoService.Search(x => x.IdUsuario == taxista.Usuario.Id)).FirstOrDefault();

            var localizacaoSummary = new LocalizacaoSummary()
            {
                Endereco = localizacao.Endereco,
                Id = localizacao.Id,
                IdUsuario = localizacao.IdUsuario,
                Latitude = localizacao.Latitude,
                Longitude = localizacao.Longitude,
                NomePublico = localizacao.NomePublico
            };

            return await base.ResponseAsync(localizacaoSummary, _LocalizacaoService); ;
        }

        /// <summary>
        /// Modifies an existing Localizacao.
        /// </summary>
        /// <param name="LocalizacaoSummary">Modified Localizacao list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] LocalizacaoSummary LocalizacaoSummary)
        {
            return await base.ResponseAsync(await this._LocalizacaoService.UpdateAsync(LocalizacaoSummary) != null, _LocalizacaoService);
        }

        /// <summary>
        /// Removes an existing Localizacao.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._LocalizacaoService.DeleteAsync(id), _LocalizacaoService);
        }
    }
}