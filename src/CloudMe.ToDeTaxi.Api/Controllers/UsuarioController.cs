using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using Microsoft.AspNetCore.Cors;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions;
using CloudMe.ToDeTaxi.Api.Models;
using Microsoft.AspNetCore.Authorization;
using CloudMe.ToDeTaxi.Configuration.Library.Constants;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Infraestructure.Entries;

namespace CloudMe.ToDeTaxi.Api.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class UsuarioController : BaseController
    {
        IUsuarioService _UsuarioService;

        public UsuarioController(IUsuarioService UsuarioService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _UsuarioService = UsuarioService;
        }

        /// <summary>
        /// Gets all Usuarios.
        /// </summary>
        [HttpGet]
        //[Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<IEnumerable<UsuarioSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<UsuarioSummary>>> GetAll()
        {
            var usuarios = _UsuarioService.Search(x => x.tipo != TipoUsuario.Administrador);
            return await base.ResponseAsync(await _UsuarioService.GetAllSummariesAsync(usuarios), _UsuarioService);
        }

        /// <summary>
        /// Obtém usuários admnistradores.
        /// </summary>
        [HttpGet("get_all_admin")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<IEnumerable<UsuarioSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<UsuarioSummary>>> GetAllAdmin()
        {
            var usuarios = _UsuarioService.Search(x => x.tipo == TipoUsuario.Administrador);
            return await base.ResponseAsync(await _UsuarioService.GetAllSummariesAsync(usuarios), _UsuarioService);
        }

        /// <summary>
        /// Gets a Usuario.
        /// <param name="id">Usuario's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<UsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<UsuarioSummary>> Get(Guid id)
        {
            var usuario = _UsuarioService.Search(x => x.Id == id && x.tipo != TipoUsuario.Administrador).FirstOrDefault();
            return await base.ResponseAsync(await _UsuarioService.GetSummaryAsync(usuario), _UsuarioService);
        }

        /// <summary>
        /// Gets um usuário administrador.
        /// <param name="id">Usuario's ID</param>
        /// </summary>
        [HttpGet("get_admin/{id}")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<UsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<UsuarioSummary>> GetAdmin(Guid id)
        {
            var usuario = _UsuarioService.Search(x => x.Id == id && x.tipo == TipoUsuario.Administrador).FirstOrDefault();
            return await base.ResponseAsync(await _UsuarioService.GetSummaryAsync(usuario), _UsuarioService);
        }

        /// <summary>
        /// Obtém um usuário por nome.
        /// <param name="name">Nome do usuário</param>
        /// </summary>
        [HttpGet("by_name")]
        [ProducesResponseType(typeof(Response<UsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<UsuarioSummary>> GetByName(string name)
        {
            var usuario = _UsuarioService.Search(x => x.Nome == name && x.tipo != TipoUsuario.Administrador).FirstOrDefault();
            return await base.ResponseAsync(await _UsuarioService.GetSummaryAsync(usuario), _UsuarioService);
        }

        /// <summary>
        /// Creates a new Usuario.
        /// </summary>
        /// <param name="UsuarioSummary">Usuario's summary</param>
        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] UsuarioSummary UsuarioSummary)
        {
            if (UsuarioSummary.Tipo == TipoUsuario.Administrador)
            {
                _UsuarioService.AddNotification(new Notification("Novo usuário", "ação não autorizada"));
                return await base.ErrorResponseAsync<Guid>(_UsuarioService);
            }

            var entity = await this._UsuarioService.CreateAsync(UsuarioSummary);
            if (_UsuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_UsuarioService);
            }
            return await base.ResponseAsync(entity.Id, _UsuarioService);
        }

        /// <summary>
        /// Creates a new Usuario.
        /// </summary>
        /// <param name="UsuarioSummary">Usuario's summary</param>
        [HttpPost("post_admin")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> PostAdmin([FromBody] UsuarioSummary UsuarioSummary)
        {
            UsuarioSummary.Tipo = TipoUsuario.Administrador;

            var entity = await this._UsuarioService.CreateAsync(UsuarioSummary);
            if (_UsuarioService.IsInvalid())
            {
                return await base.ErrorResponseAsync<Guid>(_UsuarioService);
            }
            return await base.ResponseAsync(entity.Id, _UsuarioService);
        }

        /// <summary>
        /// Modifies an existing Usuario.
        /// </summary>
        /// <param name="UsuarioSummary">Modified Usuario list's properties summary</param>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Put([FromBody] UsuarioSummary UsuarioSummary)
        {
            var usuario = _UsuarioService.Search(x => x.Id == UsuarioSummary.Id).FirstOrDefault();

            if ((usuario != null && usuario.tipo == TipoUsuario.Administrador) ||  UsuarioSummary.Tipo == TipoUsuario.Administrador)
            {
                _UsuarioService.AddNotification(new Notification("Atualizar usuário", "ação não autorizada"));
                return await base.ErrorResponseAsync<bool>(_UsuarioService);
            }

            return await base.ResponseAsync(await this._UsuarioService.UpdateAsync(UsuarioSummary) != null, _UsuarioService);
        }

        /// <summary>
        /// Modifies an existing Usuario.
        /// </summary>
        /// <param name="UsuarioSummary">Modified Usuario list's properties summary</param>
        [HttpPut("put_admin")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> PutAdmin([FromBody] UsuarioSummary UsuarioSummary)
        {
            return await base.ResponseAsync(await this._UsuarioService.UpdateAsync(UsuarioSummary) != null, _UsuarioService);
        }

        /// <summary>
        /// Removes an existing Usuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
        {
            var usuario = _UsuarioService.Search(x => x.Id == id).FirstOrDefault();

            if (usuario != null && usuario.tipo == TipoUsuario.Administrador)
            {
                _UsuarioService.AddNotification(new Notification("Remover usuário", "ação não autorizada"));
                return await base.ErrorResponseAsync<bool>(_UsuarioService);
            }

            return await base.ResponseAsync(await this._UsuarioService.DeleteAsync(id), _UsuarioService);
        }

        /// <summary>
        /// Removes an existing Usuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("delete_admin{id}")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> DeleteAdmin(Guid id)
        {
            return await base.ResponseAsync(await this._UsuarioService.DeleteAsync(id), _UsuarioService);
        }

        /// <summary>
        /// Altera credenciais do usuario.
        /// </summary>
        /// <param name="id">User ID</param>
        [HttpPost("altera_credenciais/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> AlterarCredenciais(Guid id, [FromBody] CredenciaisUsuario credenciais)
        {
            var usuario= await _UsuarioService.Get(id);
            if(usuario == null)
            {
                unitOfWork.AddNotification(new Notification("Alterar credenciais", "Usuário não encontrado"));
                return await ErrorResponseAsync<bool>(unitOfWork);
            }

            if (usuario.tipo == TipoUsuario.Administrador && (User.Identity as Usuario).tipo != TipoUsuario.Administrador)
            {

            }

            if(credenciais.Senha != credenciais.ConfirmarSenha)
            {
                unitOfWork.AddNotification(new Notification("Alterar credenciais", "Senhas não correspondem"));
                return await ErrorResponseAsync<bool>(unitOfWork);
            }

            if (usuario.UserName != credenciais.Login)
            {
                await _UsuarioService.ChangeLoginAsync(id, credenciais.Login);
            }

            await _UsuarioService.ChangePasswordAsync(id, credenciais.SenhaAnterior, credenciais.Senha);

            if (_UsuarioService.IsInvalid())
            {
                return await ErrorResponseAsync<bool>(_UsuarioService);
            }

            return await ResponseAsync(true, _UsuarioService);
        }

        /// <summary>
        /// Bloqueia/desbloqueia um usuario.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="bloquear">Indica se o usuário será ou não bloqueado</param>
        [HttpPost("bloquear/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Bloquear(Guid id, bool bloquear)
        {
            // bloqueia/desbloqueia o usuário do taxista
            return await base.ResponseAsync(
                await this._UsuarioService.BloquearAsync(id, bloquear), 
                this._UsuarioService);
        }
    }
}