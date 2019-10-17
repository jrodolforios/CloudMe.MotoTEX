using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using CloudMe.ToDeTaxi.Api.Models.ViaCEP;
using RestSharp;
using System.Threading;
using Newtonsoft.Json;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class EnderecoController : BaseController
    {
        IEnderecoService _EnderecoService;

        public EnderecoController(IEnderecoService EnderecoService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _EnderecoService = EnderecoService;
        }

        /// <summary>
        /// Gets all Enderecos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<EnderecoSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<EnderecoSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _EnderecoService.GetAllSummariesAsync(), _EnderecoService);
        }

        /// <summary>
        /// Gets a Endereco.
        /// <param name="id">Endereco's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<EnderecoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<EnderecoSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _EnderecoService.GetSummaryAsync(id), _EnderecoService);
        }

        /// <summary>
        /// Obtém um endereço por CEP.
        /// <param name="cep">CEP do endereco a ser consultado</param>
        /// </summary>
        [HttpGet("consulta_cep/{cep}")]
        [ProducesResponseType(typeof(Response<EnderecoSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<EnderecoSummary>> ConsultaCEP(string cep)
        {
            var client = new RestClient("https://viacep.com.br/");
            var request = new RestRequest(string.Format("ws/{0}/json/", cep), Method.GET);

            var result = await client.ExecuteTaskAsync(request);
            if(result.StatusCode == HttpStatusCode.OK)
            {
                var endViaCEP = JsonConvert.DeserializeObject<Endereco>(result.Content);

                if (endViaCEP.erro.HasValue)
                {
                    unitOfWork.AddNotification("Consulta CEP", "CEP inexistente");
                    return await ErrorResponseAsync<EnderecoSummary>(unitOfWork);
                }

                return await ResponseAsync(new EnderecoSummary
                {
                    Id = Guid.Empty,
                    CEP = endViaCEP.cep ?? "",
                    Bairro = endViaCEP.bairro ?? "",
                    Localidade = endViaCEP.localidade ?? "",
                    Logradouro = endViaCEP.logradouro ?? "",
                    UF = endViaCEP.uf ?? ""
                }, unitOfWork);
            }
            else
            {
                unitOfWork.AddNotification("Consulta CEP", result.ErrorMessage);
                return await ErrorResponseAsync<EnderecoSummary>(unitOfWork);
            }
        }

        /// <summary>
        /// Creates a new Endereco.
        /// </summary>
        /// <param name="EnderecoSummary">Endereco's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] EnderecoSummary EnderecoSummary)
        {
            var entity = await this._EnderecoService.CreateAsync(EnderecoSummary);
            if (_EnderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_EnderecoService);
            }
            return await base.ResponseAsync(entity.Id, _EnderecoService);
        }

        /// <summary>
        /// Modifies an existing Endereco.
        /// </summary>
        /// <param name="EnderecoSummary">Modified Endereco list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] EnderecoSummary EnderecoSummary)
        {
            return await base.ResponseAsync(await this._EnderecoService.UpdateAsync(EnderecoSummary) != null, _EnderecoService);
        }

        /// <summary>
        /// Removes an existing Endereco.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            return await base.ResponseAsync(await this._EnderecoService.DeleteAsync(id), _EnderecoService);
        }
    }
}