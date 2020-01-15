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
    public class LocalizacaoController : BaseController
    {
        ILocalizacaoService _LocalizacaoService;

        public LocalizacaoController(ILocalizacaoService LocalizacaoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _LocalizacaoService = LocalizacaoService;
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