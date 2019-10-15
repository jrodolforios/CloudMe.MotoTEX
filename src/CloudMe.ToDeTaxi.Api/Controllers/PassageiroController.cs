using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class PassageiroController : BaseController
    {
        IPassageiroService _PassageiroService;
        IUsuarioService _usuarioService;
        IEnderecoService _enderecoService;
        IFotoService _fotoService;

        public PassageiroController(
            IPassageiroService passageiroService,
            IUsuarioService usuarioService,
            IEnderecoService localizacaoService,
            IFotoService fotoService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _PassageiroService = passageiroService;
            _usuarioService = usuarioService;
            _enderecoService = localizacaoService;
            _fotoService = fotoService;
        }

        /// <summary>
        /// Gets all Passageiros.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<PassageiroSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<PassageiroSummary>>> GetAll()
        {
            return await base.ResponseAsync(await _PassageiroService.GetAllSummariesAsync(), _PassageiroService);
        }

        /// <summary>
        /// Gets a Passageiro.
        /// <param name="id">Passageiro's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<PassageiroSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<PassageiroSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _PassageiroService.GetSummaryAsync(id), _PassageiroService);
        }

        /// <summary>
        /// Creates a new Passageiro.
        /// </summary>
        /// <param name="passageiroSummary">Passageiro's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<PassageiroSummary>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<PassageiroSummary>> Post([FromBody] PassageiroSummary passageiroSummary)
        {
            // OBS.: A criação das entidades gerenciadas pela API é feita antes da criação do
            // usuário, pois este último é gerenciado externamente pelo AspNet Identity,
            // não nos permitindo interferir na persistência de suas informações. Assim,
            // qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback.

            // cria o endereço do passageiro
            var endereco = await this._enderecoService.CreateAsync(passageiroSummary.Endereco);
            if (_enderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PassageiroSummary>(_enderecoService);
            }

            passageiroSummary.Endereco.Id = endereco.Id;

            // cria o registro do passageiro
            var passageiro = await this._PassageiroService.CreateAsync(passageiroSummary);
            if (_PassageiroService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PassageiroSummary>(_PassageiroService);
            }

            // cria um usuario para o passageiro
            var usuario = await this._usuarioService.CreateAsync(passageiroSummary.Usuario);
            if (_usuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PassageiroSummary>(_usuarioService);
            }

            // aplica o id de usuário
            passageiroSummary.Usuario.Id = usuario.Id;
            await _PassageiroService.UpdateAsync(passageiroSummary);
            if (_PassageiroService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PassageiroSummary>(_usuarioService);
            }

            return await base.ResponseAsync(await _PassageiroService.GetSummaryAsync(passageiro.Id), _PassageiroService);
        }

        /// <summary>
        /// Modifies an existing Passageiro.
        /// </summary>
        /// <param name="passageiroSummary">Modified Passageiro list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<PassageiroSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<PassageiroSummary>> Put([FromBody] PassageiroSummary passageiroSummary)
        {
            // OBS.: Qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback (vide método POST).

            var passageiro = await this._PassageiroService.Get(passageiroSummary.Id);

            // atualiza o registro do passageiro
            await this._PassageiroService.UpdateAsync(passageiroSummary);
            if (_PassageiroService.IsInvalid())
            {
                return await base.ErrorResponseAsync<PassageiroSummary>(_PassageiroService);
            }

            if (passageiroSummary.Usuario != null)
            {
                // atualiza o registro do usuário
                await this._usuarioService.UpdateAsync(passageiroSummary.Usuario);
                if (_usuarioService.IsInvalid())
                {
                    return await base.ErrorResponseAsync<PassageiroSummary>(_usuarioService);
                }
            }

            if (passageiroSummary.Endereco != null)
            {
                // atualiza o registro de endereço
                await this._enderecoService.UpdateAsync(passageiroSummary.Endereco);
                if (_enderecoService.IsInvalid())
                {
                    return await base.ErrorResponseAsync<PassageiroSummary>(_enderecoService);
                }
            }

            return await base.ResponseAsync(await _PassageiroService.GetSummaryAsync(passageiro.Id), unitOfWork);
        }

        /// <summary>
        /// Removes an existing Passageiro.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            // OBS.: Qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback (vide método POST).

            var passageiroSummary = await this._PassageiroService.GetSummaryAsync(id);

            // primeiro, remove o registro do passageiro
            await this._PassageiroService.DeleteAsync(passageiroSummary.Id);
            if (_PassageiroService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_PassageiroService);
            }

            // remove o registro de endereço
            await this._enderecoService.DeleteAsync(passageiroSummary.Endereco.Id);
            if (_enderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_enderecoService);
            }

            // remove o registro do usuário
            await this._usuarioService.DeleteAsync((Guid)passageiroSummary.Usuario.Id);
            if (_usuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_usuarioService);
            }

            return await base.ResponseAsync(true, unitOfWork);
        }

        /// <summary>
        /// Associa uma foto ao passageiro.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="idFoto">ID da foto</param>
        [HttpPost("associar_foto/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> AssociarFoto(Guid id, Guid idFoto)
        {
            return await ResponseAsync(await _PassageiroService.AssociarFoto(id, idFoto), _PassageiroService);
        }

        /// <summary>
        /// Ativa/desativa um passageiro.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="ativar">Indica se o usuário será ativado/desativado</param>
        [HttpPost("ativar/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Ativar(Guid id, bool ativar)
        {
            // ativa/desativa o passageiro
            return await ResponseAsync(await _PassageiroService.Ativar(id, ativar), _PassageiroService);
        }
    }
}

