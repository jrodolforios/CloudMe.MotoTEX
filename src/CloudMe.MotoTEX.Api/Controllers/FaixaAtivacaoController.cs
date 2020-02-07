using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FaixaAtivacaoController : BaseController
    {
        IFaixaAtivacaoService _FaixaAtivacaoService;

        public FaixaAtivacaoController(IFaixaAtivacaoService FaixaAtivacaoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _FaixaAtivacaoService = FaixaAtivacaoService;
        }

        /// <summary>
        /// Gets all FaixaAtivacaos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<FaixaAtivacaoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaixaAtivacaoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _FaixaAtivacaoService.GetAllSummariesAsync(), _FaixaAtivacaoService);
        }

        /// <summary>
        /// Gets all FaixaAtivacaos.
        /// </summary>
        [HttpGet("by_radius")]
        [ProducesResponseType(typeof(Response<IEnumerable<FaixaAtivacaoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<FaixaAtivacaoSummary>>> GetAllByRadius()
        {
            var faixas = await _FaixaAtivacaoService.GetAllByRadius();
            return await base.ResponseAsync(await _FaixaAtivacaoService.GetAllSummariesAsync(faixas), _FaixaAtivacaoService);
        }

        /// <summary>
        /// Gets a FaixaAtivacao.
        /// <param name="id">FaixaAtivacao's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FaixaAtivacaoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<FaixaAtivacaoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _FaixaAtivacaoService.GetSummaryAsync(id), _FaixaAtivacaoService);
        }

        /// <summary>
        /// Creates a new FaixaAtivacao.
        /// </summary>
        /// <param name="FaixaAtivacaoSummary">FaixaAtivacao's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] FaixaAtivacaoSummary FaixaAtivacaoSummary)
        {
            var entity = await this._FaixaAtivacaoService.CreateAsync(FaixaAtivacaoSummary);
            if (_FaixaAtivacaoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_FaixaAtivacaoService);
            }
            return await base.ResponseAsync(entity.Id, _FaixaAtivacaoService);
        }

        /// <summary>
        /// Modifies an existing FaixaAtivacao.
        /// </summary>
        /// <param name="FaixaAtivacaoSummary">Modified FaixaAtivacao list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] FaixaAtivacaoSummary FaixaAtivacaoSummary)
        {
            return await base.ResponseAsync(await this._FaixaAtivacaoService.UpdateAsync(FaixaAtivacaoSummary) != null, _FaixaAtivacaoService);
        }

        /// <summary>
        /// Removes an existing FaixaAtivacao.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._FaixaAtivacaoService.DeleteAsync(id), _FaixaAtivacaoService);
        }
    }
}