using System;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class TaxistaController : BaseController
    {
        ITaxistaService _taxistaService;
        IUsuarioService _usuarioService;
        IEnderecoService _enderecoService;

        public TaxistaController(
            ITaxistaService taxistaService,
            IUsuarioService usuarioService,
            IEnderecoService localizacaoService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _taxistaService = taxistaService;
            _usuarioService = usuarioService;
            _enderecoService = localizacaoService;
        }

        /// <summary>
        /// Gets all taxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return await base.ResponseAsync(await _taxistaService.GetAllSummariesAsync(), _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Gets a Taxista.
        /// <param name="id">Taxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaxistaSummary), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await _taxistaService.GetSummaryAsync(id), _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Creates a new Taxista.
        /// </summary>
        /// <param name="taxistaSummary">Taxista's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody] TaxistaSummary taxistaSummary)
        {
            try
            {
                // cria um usuario para o taxista
                var usuario = await this._usuarioService.CreateAsync(taxistaSummary.Usuario);
                if (usuario == null)
                {
                    throw new Exception("Não foi possível criar usuário para o taxista"); // TODO: Tirar essa gambiarra (exception)
                }
                taxistaSummary.Usuario.Id = usuario.Id;

                // cria um endereço para o taxista
                var endereco = await this._enderecoService.CreateAsync(taxistaSummary.Endereco);
                if (endereco == null)
                {
                    throw new Exception("Não foi possível criar endereço para o taxista"); // TODO: Tirar essa gambiarra (exception)
                }
                taxistaSummary.Endereco.Id = endereco.Id;

                var taxista = await this._taxistaService.CreateAsync(taxistaSummary);
                if(taxista == null)
                {
                    throw new Exception("Não foi possível criar registro do taxista"); // TODO: Tirar essa gambiarra (exception)
                }
                return await base.ResponseAsync(taxista.Id, _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Modifies an existing Taxista.
        /// </summary>
        /// <param name="taxistaSummary">Modified Taxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] TaxistaSummary taxistaSummary)
        {
            try
            {
                return await base.ResponseAsync(await this._taxistaService.UpdateAsync(taxistaSummary) != null, _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }

        /// <summary>
        /// Removes an existing Taxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return await base.ResponseAsync(await this._taxistaService.DeleteAsync(id), _taxistaService);
            }
            catch (Exception ex)
            {
                return await base.ResponseExceptionAsync(ex);
            }
        }
    }
}