using System;
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
            return await base.ResponseAsync(await _UsuarioService.GetAllSummariesAsync(), _UsuarioService);
        }

        /// <summary>
        /// Gets a Usuario.
        /// <param name="id">Usuario's ID</param>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<UsuarioSummary>), (int)HttpStatusCode.OK)]
        public async Task<Response<UsuarioSummary>> Get(Guid id)
        {
            return await base.ResponseAsync(await _UsuarioService.GetSummaryAsync(id), _UsuarioService);
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