using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;
using prmToolkit.NotificationPattern;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class CorVeiculoController : BaseController
    {
        ICorVeiculoService _CorVeiculoService;

        public CorVeiculoController(ICorVeiculoService CorVeiculoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _CorVeiculoService = CorVeiculoService;
        }

        /// <summary>
        /// Gets all CorVeiculos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<CorVeiculoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<CorVeiculoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _CorVeiculoService.GetAllSummariesAsync(), _CorVeiculoService);
        }

        /// <summary>
        /// Gets a CorVeiculo.
        /// <param name="id">CorVeiculo's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<CorVeiculoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<CorVeiculoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _CorVeiculoService.GetSummaryAsync(id), _CorVeiculoService);
        }

        /// <summary>
        /// Creates a new CorVeiculo.
        /// </summary>
        /// <param name="CorVeiculoSummary">CorVeiculo's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] CorVeiculoSummary CorVeiculoSummary)
        {
            var entity = await this._CorVeiculoService.CreateAsync(CorVeiculoSummary);
            if (_CorVeiculoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_CorVeiculoService);
            }
            return await base.ResponseAsync(entity.Id, _CorVeiculoService);
        }

        /// <summary>
        /// Modifies an existing CorVeiculo.
        /// </summary>
        /// <param name="CorVeiculoSummary">Modified CorVeiculo list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] CorVeiculoSummary CorVeiculoSummary)
        {
            return await base.ResponseAsync(await this._CorVeiculoService.UpdateAsync(CorVeiculoSummary) != null, _CorVeiculoService);
        }

        /// <summary>
        /// Removes an existing CorVeiculo.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete([FromServices] IVeiculoService veiculoService, Guid id)
        {
            var corSummary = await _CorVeiculoService.GetSummaryAsync(id);
            if (corSummary.Id == Guid.Empty)
            {
                _CorVeiculoService.AddNotification(new Notification("Cores", "Cor não encontrada"));
            }

            // remove associações com veículos
            var veicsSummaries = await veiculoService.GetAllSummariesAsync(
                    await veiculoService.Search(veic => veic.IdCorVeiculo == corSummary.Id)
                );

            foreach (var veicSummary in veicsSummaries)
            {
                veicSummary.IdCorVeiculo = null;
                await veiculoService.UpdateAsync(veicSummary);
                if (veiculoService.IsInvalid())
                {
                    return await ErrorResponseAsync<bool>(veiculoService);
                }
            }

            return await base.ResponseAsync(await this._CorVeiculoService.DeleteAsync(id), _CorVeiculoService);
        }
    }
}