using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Veiculo;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using RestSharp;
using Newtonsoft.Json;
using CloudMe.ToDeTaxi.Api.Models.FIPE;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class VeiculoController : BaseController
    {
        IVeiculoService _VeiculoService;

        public VeiculoController(IVeiculoService VeiculoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _VeiculoService = VeiculoService;
        }

        /// <summary>
        /// Gets all Veiculos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<VeiculoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<VeiculoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _VeiculoService.GetAllSummariesAsync(), _VeiculoService);
        }

        /// <summary>
        /// Gets a Veiculo.
        /// <param name="id">Veiculo's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<VeiculoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<VeiculoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _VeiculoService.GetSummaryAsync(id), _VeiculoService);
        }

        /// <summary>
        /// Obtém marcas de veículos.
        /// </summary>
        [HttpGet("marcas")]
        [ProducesResponseType(typeof(Response<IEnumerable<MarcaVeiculo>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<MarcaVeiculo>>> GetMarcas()
        {
            var client = new RestClient("https://parallelum.com.br/fipe/");
            var request = new RestRequest("api/v1/carros/marcas", Method.GET);

            var result = await client.ExecuteTaskAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var marcas = JsonConvert.DeserializeObject<IEnumerable<MarcaVeiculo>>(result.Content);

                return await ResponseAsync(marcas, unitOfWork);
            }
            else
            {
                unitOfWork.AddNotification("Consulta marcas de veículos", result.ErrorMessage);
                return await ErrorResponseAsync<IEnumerable<MarcaVeiculo>>(unitOfWork);
            }
        }

        /// <summary>
        /// Obtém os modelos de uma marca de veículo.
        /// </summary>
        [HttpGet("modelos/{codigo_marca}")]
        [ProducesResponseType(typeof(Response<InfoMarca>), (int)HttpStatusCode.OK)]
        public async Task<Response<InfoMarca>> GetModelos(string codigo_marca)
        {
            var client = new RestClient("https://parallelum.com.br/fipe/");
            var request = new RestRequest(string.Format("/api/v1/carros/marcas/{0}/modelos", codigo_marca), Method.GET);

            var result = await client.ExecuteTaskAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var marcas = JsonConvert.DeserializeObject<InfoMarca>(result.Content);

                return await ResponseAsync(marcas, unitOfWork);
            }
            else
            {
                unitOfWork.AddNotification("Consulta modelos de veículos", result.ErrorMessage);
                return await ErrorResponseAsync<InfoMarca>(unitOfWork);
            }
        }

        /// <summary>
        /// Creates a new Veiculo.
        /// </summary>
        /// <param name="VeiculoSummary">Veiculo's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] VeiculoSummary VeiculoSummary)
        {
            var entity = await this._VeiculoService.CreateAsync(VeiculoSummary);
            if (_VeiculoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_VeiculoService);
            }
            return await base.ResponseAsync(entity.Id, _VeiculoService);
        }

        /// <summary>
        /// Modifies an existing Veiculo.
        /// </summary>
        /// <param name="VeiculoSummary">Modified Veiculo list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] VeiculoSummary VeiculoSummary)
        {
            return await base.ResponseAsync(await this._VeiculoService.UpdateAsync(VeiculoSummary) != null, _VeiculoService);
        }

        /// <summary>
        /// Removes an existing Veiculo.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._VeiculoService.DeleteAsync(id), _VeiculoService);
        }
    }
}