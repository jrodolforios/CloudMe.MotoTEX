using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Dtos.Identity;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.Services.Interfaces;
using CloudMe.Auth.Admin.ExceptionHandling;
using CloudMe.Auth.Admin.Configuration.Constants;
using Skoruba.IdentityServer4.Admin.EntityFramework.DbContexts;
using Skoruba.IdentityServer4.Admin.EntityFramework.Identity.Entities.Identity;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using Microsoft.AspNetCore.Identity;
using CloudMe.Auth.Admin.Model;

namespace CloudMe.Auth.Admin.Controllers
{
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    public class IdentityController : BaseIdentityController<CloudMeToDeTaxiContext, CloudMeUserDto, Guid, RoleDto<Guid>, Guid, Guid, Guid, CloudMe.ToDeTaxi.Infraestructure.Entries.User,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,  IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public IdentityController(IIdentityService<CloudMeToDeTaxiContext, CloudMeUserDto, Guid, RoleDto<Guid>, Guid, Guid, Guid, CloudMe.ToDeTaxi.Infraestructure.Entries.User,IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,  IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> identityService, ILogger<ConfigurationController> logger, IStringLocalizer<IdentityController> localizer)
            : base(identityService, logger, localizer)
        {
        }
    }
}