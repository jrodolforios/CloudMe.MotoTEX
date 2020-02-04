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
using RestSharp;
using Newtonsoft.Json;
using CloudMe.MotoTEX.Api.Models.FIPE;

namespace CloudMe.MotoTEX.Api.Controllers
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

        private void AddFIPERequestHeaders(RestRequest request)
        {
            request.AddHeader("Accept", "application/json, text/javascript, /; q=0.01");
            request.AddHeader("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            request.AddHeader("Origin", "https://veiculos.fipe.org.br");
            request.AddHeader("Referer", "https://veiculos.fipe.org.br/");
        }

        /// <summary>
        /// Obtém marcas de veículos.
        /// </summary>
        [HttpGet("marcas")]
        [ProducesResponseType(typeof(Response<IEnumerable<MarcaVeiculo>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<MarcaVeiculo>>> GetMarcas()
        {
            //var client = new RestClient("https://parallelum.com.br/fipe/");
            //var request = new RestRequest("api/v1/carros/marcas", Method.GET);
            //var result = await client.ExecuteTaskAsync(request);

            var client = new RestClient("https://veiculos.fipe.org.br/");
            var requestTabelaReferencia = new RestRequest("api/veiculos/ConsultarTabelaDeReferencia", Method.POST);
            AddFIPERequestHeaders(requestTabelaReferencia);
            var result = await client.ExecuteAsync(requestTabelaReferencia);
            var tabelasReferencia = JsonConvert.DeserializeObject<List<TabelaReferencia>>(result.Content);

            if (tabelasReferencia.Count > 0)
            {
                var tabela = tabelasReferencia[0];
                var requestMarcas = new RestRequest("api/veiculos/ConsultarMarcas", Method.POST);
                AddFIPERequestHeaders(requestMarcas);

                var requestMarcasBody = new
                {
                    codigoTabelaReferencia = tabela.Codigo,
                    codigoTipoVeiculo = TipoVeiculo.Motos
                };
                requestMarcas.AddJsonBody(requestMarcasBody);
                result = await client.ExecuteAsync(requestMarcas);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var marcas = JsonConvert.DeserializeObject<IEnumerable<MarcaVeiculo>>(result.Content);

                    var response = await ResponseAsync(marcas, unitOfWork);
                    response.count = (marcas as List<MarcaVeiculo>).Count;
                    return response;
                }
                else
                {
                    unitOfWork.AddNotification("Consulta marcas de veículos", "Erro ao obter marcas de veículos");
                    return await ErrorResponseAsync<IEnumerable<MarcaVeiculo>>(unitOfWork);
                }
            }
            else
            {
                unitOfWork.AddNotification("Consulta marcas de veículos", "Tabela de referência não encontrada");
                return await ErrorResponseAsync<IEnumerable<MarcaVeiculo>>(unitOfWork);
            }
        }

        /// <summary>
        /// Obtém os modelos de uma marca de veículo.
        /// </summary>
        [HttpGet("modelos/{codigo_marca}")]
        [ProducesResponseType(typeof(Response<IEnumerable<ModeloVeiculo>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<ModeloVeiculo>>> GetModelos(string codigo_marca)
        {
            /*var client = new RestClient("https://parallelum.com.br/fipe/");
            var request = new RestRequest(string.Format("/api/v1/carros/marcas/{0}/modelos", codigo_marca), Method.GET);

            var result = await client.ExecuteTaskAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var info_marca = JsonConvert.DeserializeObject<InfoMarca>(result.Content);

                return await ResponseAsync(info_marca.modelos, unitOfWork);
            }
            else
            {
                unitOfWork.AddNotification("Consulta modelos de veículos", result.ErrorMessage);
                return await ErrorResponseAsync<IEnumerable<ModeloVeiculo>>(unitOfWork);
            }*/

            var client = new RestClient("https://veiculos.fipe.org.br/");
            var requestTabelaReferencia = new RestRequest("api/veiculos/ConsultarTabelaDeReferencia", Method.POST);
            AddFIPERequestHeaders(requestTabelaReferencia);
            var result = await client.ExecuteAsync(requestTabelaReferencia);
            var tabelasReferencia = JsonConvert.DeserializeObject<List<TabelaReferencia>>(result.Content);

            if (tabelasReferencia.Count > 0)
            {
                var tabela = tabelasReferencia[0];
                var requestModelosMarca = new RestRequest("api/veiculos/ConsultarModelos", Method.POST);
                AddFIPERequestHeaders(requestModelosMarca);

                var requestMarcasBody = new
                {
                    codigoTabelaReferencia = tabela.Codigo,
                    codigoTipoVeiculo = TipoVeiculo.Motos,
                    codigoMarca = codigo_marca
                };
                requestModelosMarca.AddJsonBody(requestMarcasBody);
                result = await client.ExecuteAsync(requestModelosMarca);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var info_marca = JsonConvert.DeserializeObject<InfoMarca>(result.Content);

                    var response = await ResponseAsync(info_marca.modelos, unitOfWork);
                    response.count = (info_marca.modelos as List<ModeloVeiculo>).Count;
                    return response;
                }
                else
                {
                    unitOfWork.AddNotification("Consulta marcas de veículos", "Erro ao obter modelos de veículos");
                    return await ErrorResponseAsync<IEnumerable<ModeloVeiculo>>(unitOfWork);
                }
            }
            else
            {
                unitOfWork.AddNotification("Consulta marcas de veículos", "Tabela de referência não encontrada");
                return await ErrorResponseAsync<IEnumerable<ModeloVeiculo>>(unitOfWork);
            }
        }

        /// <summary>
        /// Obtém os anos de fabricação e versões (combustível) de um modelo de veículo.
        /// </summary>
        [HttpGet("anos_versoes/{codigo_modelo}")]
        [ProducesResponseType(typeof(Response<IEnumerable<AnoVersao>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<AnoVersao>>> GetAnosVersoes(string codigo_marca, string codigo_modelo)
        {
            /*var client = new RestClient("https://parallelum.com.br/fipe/");
            var request = new RestRequest(string.Format("/api/v1/carros/marcas/{0}/modelos/{1}/anos", codigo_marca, codigo_modelo), Method.GET);

            var result = await client.ExecuteTaskAsync(request);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var anos_modelos = JsonConvert.DeserializeObject<IEnumerable<AnoVersao>>(result.Content);

                foreach(var ano_modelo in anos_modelos)
                {
                    string[] tokens = ano_modelo.nome.Split(' ');
                    if(tokens.Length == 2)
                    {
                        ano_modelo.ano = tokens[0];
                        ano_modelo.versao = tokens[1];
                    }
                }
                return await ResponseAsync(anos_modelos, unitOfWork);
            }
            else
            {
                unitOfWork.AddNotification("Consulta modelos de veículos", result.ErrorMessage);
                return await ErrorResponseAsync<IEnumerable<AnoVersao>>(unitOfWork);
            }*/

            var client = new RestClient("https://veiculos.fipe.org.br/");
            var requestTabelaReferencia = new RestRequest("api/veiculos/ConsultarTabelaDeReferencia", Method.POST);
            AddFIPERequestHeaders(requestTabelaReferencia);
            var result = await client.ExecuteAsync(requestTabelaReferencia);
            var tabelasReferencia = JsonConvert.DeserializeObject<List<TabelaReferencia>>(result.Content);

            if (tabelasReferencia.Count > 0)
            {
                var tabela = tabelasReferencia[0];
                var requestAnoModelo = new RestRequest("api/veiculos/ConsultarAnoModelo", Method.POST);
                AddFIPERequestHeaders(requestAnoModelo);

                requestAnoModelo.AddJsonBody(new
                {
                    codigoTabelaReferencia = tabela.Codigo,
                    codigoTipoVeiculo = TipoVeiculo.Motos,
                    codigoMarca = codigo_marca,
                    codigoModelo = codigo_modelo
                });
                result = await client.ExecuteAsync(requestAnoModelo);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var anos_modelos = JsonConvert.DeserializeObject<IEnumerable<AnoVersao>>(result.Content);

                    foreach (var ano_modelo in anos_modelos)
                    {
                        string[] tokens = ano_modelo.nome.Split(' ');
                        if (tokens.Length == 2)
                        {
                            ano_modelo.ano = tokens[0];
                            ano_modelo.versao = tokens[1];
                        }
                    }

                    var response = await ResponseAsync(anos_modelos, unitOfWork);
                    response.count = (anos_modelos as List<AnoVersao>).Count;
                    return response;

                }
                else
                {
                    unitOfWork.AddNotification("Consulta marcas de veículos", "Erro ao obter anos de modelos de veículos");
                    return await ErrorResponseAsync<IEnumerable<AnoVersao>>(unitOfWork);
                }
            }
            else
            {
                unitOfWork.AddNotification("Consulta marcas de veículos", "Tabela de referência não encontrada");
                return await ErrorResponseAsync<IEnumerable<AnoVersao>>(unitOfWork);
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
        public async Task<Response<bool>> Delete(
            [FromServices]IVeiculoTaxistaService veiculoTaxistaService,
            Guid id)
        {
            // remove associações com taxistas
            var taxistasVeic = veiculoTaxistaService.Search(txVeic => txVeic.IdVeiculo == id);
            foreach (var txVeic in taxistasVeic)
            {
                await veiculoTaxistaService.DeleteAsync(txVeic.Id);
                if (veiculoTaxistaService.IsInvalid())
                {
                    return await ErrorResponseAsync<bool>(veiculoTaxistaService);
                }
            }

            return await base.ResponseAsync(await this._VeiculoService.DeleteAsync(id), _VeiculoService);
        }
    }
}