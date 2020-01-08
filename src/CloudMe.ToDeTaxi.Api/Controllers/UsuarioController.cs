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
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<IEnumerable<UsuarioSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<UsuarioSummary>>> GetAll()
        {
            var usuarios = await _UsuarioService.GetAll();
            return await base.ResponseAsync(await _UsuarioService.GetAllSummariesAsync(usuarios), _UsuarioService);
        }

        /// <summary>
        /// Gets all admin.
        /// </summary>
        [HttpGet("admins")]
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
            var isAdmin = User.IsInRole(AuthorizationConsts.AdministrationRole);
            Usuario reqUser = await GetRequestUser(_UsuarioService);

            var usuario = await _UsuarioService.Get(id);
            if (usuario == null)
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não encontrado"));
                return await ErrorResponseAsync<UsuarioSummary>(unitOfWork, HttpStatusCode.NotFound);
            }

            if (reqUser.Id != usuario.Id && !isAdmin) // somente o próprio usuário pode obter suas informações, exceto administradores
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não autorizado"));
                return await ErrorResponseAsync<UsuarioSummary>(unitOfWork, HttpStatusCode.Forbidden);
            }

            return await base.ResponseAsync(await _UsuarioService.GetSummaryAsync(usuario), _UsuarioService);
        }

        /// <summary>
        /// Verifica se um login está disponível.
        /// <param name="login">Login a ser testado</param>
        /// </summary>
        [HttpGet("login_disponivel")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> LoginDisponivel(string login)
        {
            var usuario = _UsuarioService.Search(x => x.Nome == login).FirstOrDefault();
            return await base.ResponseAsync(usuario == null, _UsuarioService);
        }

        /// <summary>
        /// Creates a new Usuario.
        /// </summary>
        /// <param name="UsuarioSummary">Usuario's summary</param>
        [HttpPost]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        //[ValidateAntiForgeryToken]
        public async Task<Response<Guid>> Post([FromBody] UsuarioSummary UsuarioSummary)
        {
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
            var isAdmin = User.IsInRole(AuthorizationConsts.AdministrationRole);
            Usuario reqUser = await GetRequestUser(_UsuarioService);

            var usuario = await _UsuarioService.Get(UsuarioSummary.Id.Value);
            if (usuario == null)
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não encontrado"));
                return await ErrorResponseAsync<bool>(unitOfWork, HttpStatusCode.NotFound);
            }

            if (reqUser.Id != usuario.Id && !isAdmin) // somente o próprio usuário pode alterar suas informações, exceto administradores
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não autorizado"));
                return await ErrorResponseAsync<bool>(unitOfWork, HttpStatusCode.Forbidden);
            }

            return await base.ResponseAsync(await this._UsuarioService.UpdateAsync(UsuarioSummary) != null, _UsuarioService);
        }

        /// <summary>
        /// Removes an existing Usuario.
        /// </summary>
        /// <param name="id">DialList's ID</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> Delete(Guid id)
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
            Usuario reqUser = await GetRequestUser(_UsuarioService);

            var usuario = await _UsuarioService.Get(id);
            if(usuario == null)
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não encontrado"));
                return await ErrorResponseAsync<bool>(unitOfWork, HttpStatusCode.NotFound);
            }

            if (reqUser.Id != usuario.Id && usuario.tipo != TipoUsuario.Administrador) // somente o próprio usuário pode alterar suas credenciais, exceto o administrador
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não autorizado"));
                return await ErrorResponseAsync<bool>(unitOfWork, HttpStatusCode.Forbidden);
            }

            if(credenciais.Senha != credenciais.ConfirmarSenha)
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Senhas não correspondem"));
                return await ErrorResponseAsync<bool>(unitOfWork, HttpStatusCode.PreconditionRequired);
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
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
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