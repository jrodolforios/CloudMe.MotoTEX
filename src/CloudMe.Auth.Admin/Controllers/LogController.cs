using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skoruba.IdentityServer4.Admin.BusinessLogic.Services.Interfaces;
using CloudMe.Auth.Admin.Configuration.Constants;
using Skoruba.IdentityServer4.Admin.EntityFramework.DbContexts;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;

namespace CloudMe.Auth.Admin.Controllers
{
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    public class LogController : BaseController
    {
        private readonly ILogService<CloudMeToDeTaxiContext> _logService;

        public LogController(ILogService<CloudMeToDeTaxiContext> logService,
            ILogger<ConfigurationController> logger) : base(logger)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> ErrorsLog(int? page, string search)
        {
            ViewBag.Search = search;
            var logs = await _logService.GetLogsAsync(search, page ?? 1);

            return View(logs);
        }
    }
}