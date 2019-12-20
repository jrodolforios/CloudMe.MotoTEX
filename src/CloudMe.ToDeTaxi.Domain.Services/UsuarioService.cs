using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using prmToolkit.NotificationPattern;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Microsoft.AspNetCore.Identity;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario, UsuarioSummary, Guid>, IUsuarioService
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly IIdentityService<CloudMeToDeTaxiContext, UserDto<Guid>, Guid, RoleDto<Guid>, Guid, Guid, Guid, CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,  IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> _identityService;
        UserManager<Usuario> _userManager;

        public UsuarioService(
            IUsuarioRepository userRepository,
            UserManager<Usuario> userManager,
            IIdentityService<CloudMeToDeTaxiContext, UserDto<Guid>, Guid, RoleDto<Guid>, Guid, Guid, Guid, CloudMe.ToDeTaxi.Infraestructure.Entries.Usuario,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,  IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> identityService)
        {
            _userRepository = userRepository;
            _identityService = identityService;
            _userManager = userManager;
        }

        public override string GetTag()
        {
            return "usuario";
        }

        protected override Task<Usuario> CreateEntryAsync(UsuarioSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Usuario = new Usuario
            {
                Id = (Guid)summary.Id,
                Nome = summary.Nome,
                UserName = summary.Credenciais.Login,
                Email = summary.Email,
                PhoneNumber = summary.Telefone,
                CPF = summary.CPF,
                RG = summary.RG,
                tipo = summary.Tipo
            };
            return Task.FromResult(Usuario);
        }

        protected override Task<UsuarioSummary> CreateSummaryAsync(Usuario entry)
        {
            var Usuario = new UsuarioSummary
            {
                Id = entry.Id,
                Nome = entry.Nome,
                Email = entry.Email,
                Telefone = entry.PhoneNumber,
                CPF = entry.CPF,
                RG = entry.RG,
                Credenciais = new CredenciaisUsuario()
                {
                    Login = entry.UserName,
                },
                Tipo = entry.tipo
            };

            return Task.FromResult(Usuario);
        }

        protected override Guid GetKeyFromSummary(UsuarioSummary summary)
        {
            return (Guid)summary.Id;
        }

        protected override IBaseRepository<Usuario> GetRepository()
        {
            return _userRepository;
        }

        protected override void UpdateEntry(Usuario entry, UsuarioSummary summary)
        {
            entry.Nome = summary.Nome;
            //entry.UserName = summary.Credenciais.Login;
            entry.Email = summary.Email;
            entry.PhoneNumber = summary.Telefone;
            entry.CPF = summary.CPF;
            entry.RG = summary.RG;
            //entry.tipo = summary.Tipo;
        }

        protected override void ValidateSummary(UsuarioSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Usuario: sumário é obrigatório"));
            }

            //if (string.IsNullOrEmpty(summary.Credenciais.Login))
            //{
            //    this.AddNotification(new Notification("Login", "Usuario: Login não informado"));
            //}

            if (string.IsNullOrEmpty(summary.Nome))
            {
                this.AddNotification(new Notification("Nome", "Usuario: Nome não informado"));
            }

            if (string.IsNullOrEmpty(summary.Email))
            {
                this.AddNotification(new Notification("Email", "Usuario: email não informado"));
            }
        }

        public async Task<IEnumerable<Usuario>> FindAllAsync(string search, int page=1, int pageSize=10)
        {
            return this._userManager.Users;
        }

        public async Task<Usuario> FindByIdAsync(Guid id)
        {
            return await this._userManager.FindByIdAsync(id.ToString());
        }

        public async Task<UsuarioSummary> GetSummaryByNameAsync(string name)
        {
            var usr = await _userManager.FindByNameAsync(name);
            if (usr == null)
            {
                AddNotification(new Notification("GetSummaryByNameAsync", string.Format("Usuário não encontrado com o nome {0}", name)));
                return default;
            }

            return await CreateSummaryAsync(usr);
        }

        public override async Task<Usuario> CreateAsync(UsuarioSummary summary)
        {
            ValidateSummary(summary);

            // verifica se existe outro usuário com o mesmo CPF
            var usrCPF = _userRepository.Search(usr => usr.CPF == summary.CPF).FirstOrDefault();
            if (usrCPF != null && !string.IsNullOrEmpty(summary.CPF))
            {
                AddNotification("Usuários", string.Format("CPF '{0}' está sendo utilizado por outro usuário", summary.CPF));
            }

            if (IsInvalid())
            {
                return null;
            }

            var user = new Usuario
            {
                Nome = summary.Nome,
                UserName = summary.Credenciais.Login,
                Email = summary.Email,
                EmailConfirmed = true,
                PhoneNumber = summary.Telefone,
                CPF = summary.CPF,
                RG = summary.RG,
                tipo = summary.Tipo
            };

            try
            {
                var createResult = await _userManager.CreateAsync(user, summary.Credenciais.Senha);

                if (createResult.Succeeded)
                {
                    if (user.tipo == TipoUsuario.Administrador)
                    {
                        await _userManager.AddToRoleAsync(user, "Administrator");
                    }

                    return user;
                }
                else
                {
                    foreach(var error in createResult.Errors)
                    {
                        this.AddNotification(new Notification(error.Code, error.Description));
                    }
                    return null;
                }
            }
            catch(Exception ex)
            {
                this.AddNotification(new Notification("Create", ex.Message));
                return null;
            }            
        }

        public async Task<bool> BloquearAsync(Guid key, bool bloquear)
        {
            try
            {
                var usuario = await this._userManager.FindByIdAsync(key.ToString());
                if(usuario == null)
                {
                    return false;
                }

                var lockoutEnabled = await this._userManager.GetLockoutEnabledAsync(usuario);
                if(lockoutEnabled == bloquear)
                {
                    return false;
                }

                var lockoutResult = await this._userManager.SetLockoutEnabledAsync(usuario, bloquear);

                if(lockoutResult.Succeeded)
                {
                    if(bloquear)
                    {
                        var lockoutResultEndDate = await this._userManager.SetLockoutEndDateAsync(usuario, DateTimeOffset.MaxValue);
                        if(!lockoutResultEndDate.Succeeded)
                        {
                            foreach(var error in lockoutResultEndDate.Errors)
                            {
                                this.AddNotification(new Notification(error.Code, error.Description));
                            }

                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    foreach(var error in lockoutResult.Errors)
                    {
                        this.AddNotification(new Notification(error.Code, error.Description));
                    }
                    return false;
                }                
            }
            catch(Exception ex)
            {
                this.AddNotification(new Notification("Delete", ex.Message));
                return false;
            }            
        }

        public override async Task<bool> DeleteAsync(Guid key, bool logical = true)
        {
            try
            {
                var usuario = await this._userManager.FindByIdAsync(key.ToString());
                if(usuario == null)
                {
                    return false;
                }

                var deleteResult = await this._userManager.DeleteAsync(usuario);

                if(!deleteResult.Succeeded)
                {
                    foreach(var error in deleteResult.Errors)
                    {
                        this.AddNotification(new Notification(error.Code, error.Description));
                    }

                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                this.AddNotification(new Notification("Delete", ex.Message));
                return false;
            }            
        }

        public override async Task<Usuario> UpdateAsync(UsuarioSummary summary)
        {
            try
            {
                ValidateSummary(summary);

                // verifica se existe outro usuário com o mesmo CPF
                var usrCPF = _userRepository.Search(usr => usr.CPF == summary.CPF && usr.Id != summary.Id).FirstOrDefault();
                if (usrCPF != null && !string.IsNullOrEmpty(summary.CPF))
                {
                    AddNotification("Usuários", string.Format("CPF '{0}' está sendo utilizado por outro usuário", summary.CPF));
                }

                if (IsInvalid())
                {
                    return null;
                }

                var usuario = await this._userManager.FindByIdAsync(summary.Id.ToString());
                if(usuario == null)
                {
                    return null;
                }

                UpdateEntry(usuario, summary);
                if (IsInvalid())
                {
                    return null;
                }

                var updateResult = await this._userManager.UpdateAsync(usuario);
                if(updateResult.Succeeded)
                {
                    if (usuario.tipo == TipoUsuario.Administrador)
                    {
                        await _userManager.AddToRoleAsync(usuario, "Administrator");
                    }
                    return usuario;
                }
                else
                {
                    foreach(var error in updateResult.Errors)
                    {
                        this.AddNotification(new Notification(error.Code, error.Description));
                    }
                    return null;
                }
            }
            catch(Exception ex)
            {
                this.AddNotification(new Notification("Update", ex.Message));
                return null;
            }            
        }

        public async Task<bool> ChangePasswordAsync(Guid key, string old_password, string new_password)
        {
            try
            {
                var usuario = await this._userManager.FindByIdAsync(key.ToString());
                if(usuario == null)
                {
                    return false;
                }

                var passwordResult = await this._userManager.ChangePasswordAsync(usuario, old_password, new_password);

                if(passwordResult.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new Exception(passwordResult.Errors.ToString());
                }
            }
            catch(Exception ex)
            {
                this.AddNotification(new Notification("ChangePassword", ex.Message));
                return false;
            }            
        }

        public async Task<bool> ChangeLoginAsync(Guid key, string new_login)
        {
            try
            {
                var usuario = await this._userManager.FindByIdAsync(key.ToString());
                if (usuario == null)
                {
                    return false;
                }

                var loginResult = await this._userManager.SetUserNameAsync(usuario, new_login);

                if (loginResult.Succeeded)
                {
                    return true;
                }
                else
                {
                    throw new Exception(loginResult.Errors.ToString());
                }
            }
            catch (Exception ex)
            {
                this.AddNotification(new Notification("ChangeLogin", ex.Message));
                return false;
            }
        }

        public Task<bool> CheckLogin(string login)
        {
            return Task.FromResult(_userRepository.FindAll().Any(x => x.UserName == login));
        }
    }
}
