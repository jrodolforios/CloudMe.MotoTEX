using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Usuario;
using Microsoft.AspNetCore.Cors;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions;
using CloudMe.MotoTEX.Api.Models;
using Microsoft.AspNetCore.Authorization;
using CloudMe.MotoTEX.Configuration.Library.Constants;
using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Infraestructure.Entries;

namespace CloudMe.MotoTEX.Api.Controllers
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
            var usuariosSummaries = await _UsuarioService.GetAllSummariesAsync(await _UsuarioService.GetAll());
            var response = await base.ResponseAsync(usuariosSummaries, _UsuarioService);
            response.count = usuariosSummaries.Count();
            return response;
        }

        /// <summary>
        /// Gets all admin.
        /// </summary>
        [HttpGet("admins")]
        [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
        [ProducesResponseType(typeof(Response<IEnumerable<UsuarioSummary>>), (int)HttpStatusCode.OK)]
        public async Task<Response<IEnumerable<UsuarioSummary>>> GetAllAdmin()
        {
            var usuarios = await _UsuarioService.Search(x => x.tipo == TipoUsuario.Administrador);
            var usuariosSummaries = await _UsuarioService.GetAllSummariesAsync(usuarios);
            var response = await base.ResponseAsync(usuariosSummaries, _UsuarioService);
            response.count = usuariosSummaries.Count();
            return response;
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
            var usuario = (await _UsuarioService.Search(x => x.Nome == login)).FirstOrDefault();
            return await base.ResponseAsync(usuario == null, _UsuarioService);
        }

        /// <summary>
        /// Verifica se um login está disponível.
        /// <param name="login">Login a ser testado</param>
        /// </summary>
        [AllowAnonymous]
        [HttpGet("get_data_hora")]
        [ProducesResponseType(typeof(Response<DateTime>), (int)HttpStatusCode.OK)]
        public async Task<Response<DateTime>> GetDataHora()
        {
            return await base.ResponseAsync(DateTime.Now, _UsuarioService);
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

            if (reqUser.Id != usuario.Id && reqUser.tipo > TipoUsuario.Administrador) // somente o próprio usuário pode alterar suas credenciais, exceto o administrador
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

        /// <summary>
        /// Informa token do dispositivo de um usuário.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="token">Token de dispositivo</param>
        [HttpPost("informar_device_token/{id}")]
        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        public async Task<Response<bool>> InformarDeviceToken(Guid id, string token)
        {
            Usuario reqUser = await GetRequestUser(_UsuarioService);

            var usuario = await _UsuarioService.Get(id);
            if (usuario == null)
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não encontrado"));
                return await ErrorResponseAsync<bool>(unitOfWork, HttpStatusCode.NotFound);
            }

            if (reqUser.Id != usuario.Id && reqUser.tipo > TipoUsuario.Administrador) // somente o próprio usuário pode informar sua token, exceto o administrador
            {
                unitOfWork.AddNotification(new Notification("Usuários", "Usuário não autorizado"));
                return await ErrorResponseAsync<bool>(unitOfWork, HttpStatusCode.Forbidden);
            }

            return await base.ResponseAsync(
                await this._UsuarioService.InformarDeviceToken(id, token),
                this._UsuarioService);
        }
    }
}