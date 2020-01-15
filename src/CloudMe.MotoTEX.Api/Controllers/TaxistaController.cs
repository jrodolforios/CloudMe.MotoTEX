using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Domain.Model.Foto;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;
using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using CloudMe.MotoTEX.Configuration.Library.Constants;
using Microsoft.AspNetCore.Identity;

namespace CloudMe.MotoTEX.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class TaxistaController : BaseController
    {
        ITaxistaService _TaxistaService;
        IUsuarioService _usuarioService;
        IEnderecoService _enderecoService;
        IFotoService _fotoService;
        ILocalizacaoService _localizacaoService;

        public TaxistaController(
            ITaxistaService taxistaService,
            IUsuarioService usuarioService,
            IEnderecoService enderecoService,
            IFotoService fotoService,
            ILocalizacaoService localizacaoService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _TaxistaService = taxistaService;
            _usuarioService = usuarioService;
            _enderecoService = enderecoService;
            _localizacaoService = localizacaoService;
            _fotoService = fotoService;
        }

        /// <summary>
        /// Gets all Taxistas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Response<IEnumerable<TaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<TaxistaSummary>>> GetAll([FromServices] RoleManager<IdentityRole<Guid>> roleManager)
        {
            return await base.ResponseAsync(await _TaxistaService.GetAllSummariesAsync(), _TaxistaService);
        }

        /// <summary>
        /// Gets a Taxista.
        /// <param name="id">Taxista's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<TaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<TaxistaSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _TaxistaService.GetSummaryAsync(id), _TaxistaService);
        }

        /// <summary>
        /// Get by IdUser
        /// </summary>
        /// <param name="id">User Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpGet("consulta_id_taxista/{id}")]
        [ProducesResponseType(typeof(Response<TaxistaSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<TaxistaSummary>> GetByUserId(Guid id)
        {
            return await base.ResponseAsync(await _TaxistaService.GetByUserId(id), _TaxistaService);
        }

        /// <summary>
        /// Get by IdUser
        /// </summary>
        /// <param name="id">User Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpGet("marcar_taxista_disponivel/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> MarcarTaxistaDisponivel(Guid id, bool disponivel)
        {
            return await base.ResponseAsync(await _TaxistaService.MakeTaxistOnlineAsync(id, disponivel), _TaxistaService);
        }

        /// <summary>
        /// Get by IdUser
        /// </summary>
        /// <param name="id">User Id from taxist</param>
        /// <returns>passenger</returns>
        [HttpGet("classificacao_taxista/{id}")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<Response<int>> ClassificacaoTaxista(Guid id)
        {
            return await base.ResponseAsync(await _TaxistaService.ClassificacaoTaxista(id), _TaxistaService);
        }

        /// <summary>
        /// Creates a new Taxista.
        /// </summary>
        /// <param name="taxistaSummary">Taxista's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] TaxistaSummary taxistaSummary)
        {
            // OBS.: A criação das entidades gerenciadas pela API é feita antes da criação do
            // usuário, pois este último é gerenciado externamente pelo AspNet Identity,
            // não nos permitindo interferir na persistência de suas informações. Assim,
            // qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback.

            // cria o endereço do taxista
            var endereco = await this._enderecoService.CreateAsync(taxistaSummary.Endereco);
            if(_enderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_enderecoService);
            }

            taxistaSummary.Endereco.Id = endereco.Id;

            // cria o registro de foto do taxista
            var foto = await _fotoService.CreateAsync(new FotoSummary());
            if (_fotoService.IsInvalid())
            {
                return await ErrorResponseAsync<Guid>(_fotoService);
            }

            taxistaSummary.IdFoto = foto.Id;

            // cria o registro de localização atual do taxista
            var localizacaoSummary = new LocalizacaoSummary();
            localizacaoSummary.Latitude = localizacaoSummary.Longitude = "0";
            var localizacao = await this._localizacaoService.CreateAsync(localizacaoSummary);
            if (_localizacaoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_localizacaoService);
            }

            taxistaSummary.IdLocalizacaoAtual = localizacaoSummary.Id = localizacao.Id;

            // cria o registro do taxista
            var taxista = await this._TaxistaService.CreateAsync(taxistaSummary);
            if(_TaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_TaxistaService);
            }

            // cria um usuario para o taxista
            taxistaSummary.Usuario.Tipo = Domain.Enums.TipoUsuario.Taxista;
            var usuario = await this._usuarioService.CreateAsync(taxistaSummary.Usuario);
            if (_usuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_usuarioService);
            }

            // aplica o id de usuário
            taxistaSummary.Usuario.Id = usuario.Id;
            await _TaxistaService.UpdateAsync(taxistaSummary);
            if (_TaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_usuarioService);
            }

            // associa o registro de localização ao usuário
            localizacaoSummary.IdUsuario = usuario.Id;
            await _localizacaoService.UpdateAsync(localizacaoSummary);
            if (_localizacaoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_localizacaoService);
            }

            return await base.ResponseAsync(taxista.Id, _TaxistaService);
        }

        /// <summary>
        /// Modifies an existing Taxista.
        /// </summary>
        /// <param name="taxistaSummary">Modified Taxista list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] TaxistaSummary taxistaSummary)
        {
            // atualiza o registro do taxista
            return await ResponseAsync(await _TaxistaService.UpdateAsync(taxistaSummary) != null, _TaxistaService);
        }

        /// <summary>
        /// Removes an existing Taxista.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(
            [FromServices]IVeiculoTaxistaService veiculoTaxistaService,
            [FromServices]IFormaPagamentoTaxistaService formaPagamentoTaxistaService,
            [FromServices]IFaixaDescontoTaxistaService faixaDescontoTaxistaService,
            Guid id)
        {
            // OBS.: Qualquer validação nas entidades da API é feita antes da manipulação dos dados
            // de usuários para permitir o rollback (vide método POST).

            var taxistaSummary = await this._TaxistaService.GetSummaryAsync(id);
            if (taxistaSummary.Id == Guid.Empty)
            {
                _TaxistaService.AddNotification(new Notification("Taxistas", "Taxista não encontrado"));
            }

            // remove associações com veículos
            var veicsTaxista = veiculoTaxistaService.Search(veicTx => veicTx.IdTaxista == taxistaSummary.Id);
            foreach (var veicTx in veicsTaxista)
            {
                await veiculoTaxistaService.DeleteAsync(veicTx.Id);
                if (veiculoTaxistaService.IsInvalid())
                {
                    return await ErrorResponseAsync<bool>(veiculoTaxistaService);
                }
            }

            // remove associações com formas de pagamento
            var frmsPgtoTaxista = formaPagamentoTaxistaService.Search(frmPgtoTx => frmPgtoTx.IdTaxista == taxistaSummary.Id);
            foreach (var frmPgtoTx in frmsPgtoTaxista)
            {
                await formaPagamentoTaxistaService.DeleteAsync(frmPgtoTx.Id);
                if (formaPagamentoTaxistaService.IsInvalid())
                {
                    return await ErrorResponseAsync<bool>(formaPagamentoTaxistaService);
                }
            }

            // remove associações com faixas de desconto
            var fxDescTaxista = faixaDescontoTaxistaService.Search(fxDescTx => fxDescTx.IdTaxista == taxistaSummary.Id);
            foreach (var fxDescTx in fxDescTaxista)
            {
                await faixaDescontoTaxistaService.DeleteAsync(fxDescTx.Id);
                if (faixaDescontoTaxistaService.IsInvalid())
                {
                    return await ErrorResponseAsync<bool>(faixaDescontoTaxistaService);
                }
            }

            // primeiro, remove o registro do taxista
            await this._TaxistaService.DeleteAsync(taxistaSummary.Id);
            if(_TaxistaService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_TaxistaService);
            }

            // remove o registro de endereço
            await this._enderecoService.DeleteAsync(taxistaSummary.Endereco.Id);
            if(_enderecoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_enderecoService);
            }

            // remove o registro de foto
            await this._fotoService.DeleteAsync(taxistaSummary.IdFoto);
            if (_fotoService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_fotoService);
            }

            // remove o registro de localização
            if (taxistaSummary.IdLocalizacaoAtual.HasValue)
            {
                await this._localizacaoService.DeleteAsync(taxistaSummary.IdLocalizacaoAtual.Value);
                if (_localizacaoService.IsInvalid())
                {
                    return await base.ErrorResponseAsync<bool>(_localizacaoService);
                }
            }

            // necessita do commit das alterações acumuladas pra evitar erro de violação de integridade entre localização e usuário
            if (!await unitOfWork.CommitAsync())
            {
                return await base.ErrorResponseAsync<bool>(unitOfWork);
            }

            // remove o registro do usuário
            await this._usuarioService.DeleteAsync((Guid)taxistaSummary.Usuario.Id);
            if(_usuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<bool>(_usuarioService);
            }

            return await base.ResponseAsync(true, unitOfWork);
        }

        /// <summary>
        /// Informa a localização de um Taxista.
        /// </summary>
        /// <param name="localizacao">Sumário da nova localização do taxista (necessário apenas latitude e longitude)</param>
        [HttpPost("informar_localizacao/{id}")]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> InformarLocalizacao(Guid id, [FromBody] LocalizacaoSummary localizacao)
        {
            // atualiza o registro do passageiro
            return await ResponseAsync(await _TaxistaService.InformarLocalizacao(id, localizacao), _TaxistaService);
        }

        /*
        /// <summary>
        /// Associa uma foto ao taxista.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="idFoto">ID da foto</param>
        [HttpPost("associar_foto/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> AssociarFoto(Guid id, Guid idFoto)
        {
            return await ResponseAsync( await _TaxistaService.AssociarFoto(id, idFoto), _TaxistaService );
        }

        /// <summary>
        /// Ativa/desativa um taxista.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="ativar">Indica se o usuário será ativado/desativado</param>
        [HttpPost("ativar/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Ativar(Guid id, bool ativar)
        {
            // ativa/desativa o taxista
            return await ResponseAsync(await _TaxistaService.Ativar(id, ativar), _TaxistaService);
        }*/

        /// <summary>
        /// Obtém os veículos associados a um taxista.
        /// </summary>
        /// <param name="id">ID do taxista</param>
        [HttpGet("{id}/veiculos")]
        [ProducesResponseType(typeof(Response<IEnumerable<VeiculoTaxistaSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<VeiculoTaxistaSummary>>> GetVeiculos([FromServices]IVeiculoTaxistaService veicTaxistaService, Guid id)
        {
            var result = await veicTaxistaService.GetAllSummariesAsync( veicTaxistaService.Search(tx => tx.IdTaxista == id) );

            return await base.ResponseAsync(result, veicTaxistaService);
        }

    }
}

