using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using Microsoft.Extensions.DependencyInjection;

namespace CloudMe.MotoTEX.Domain.Services.Background
{
    public class PoolLocalizacaoTaxista : BackgroundService
    {
        public int Timeout { get; set; } = 5000;

        IProxyLocalizacao _ProxyNotificacoesLocalizacao;
        private readonly IConfiguration _Configuration;
        private readonly IServiceScopeFactory scopeFactory = null;


        public PoolLocalizacaoTaxista(
            //IProxyLocalizacao proxyNotificacoesLocalizacao,
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory) : base()
        {
            _ProxyNotificacoesLocalizacao = null;
            _Configuration = configuration;
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ProxyNotificacoesLocalizacao = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IProxyLocalizacao>();

            Timeout = _Configuration.GetSection("PoolLocalizacaoTaxista").GetValue<int>("Timeout");

            while (!stoppingToken.IsCancellationRequested)
            {
                await _ProxyNotificacoesLocalizacao.SolicitarLocalizacaoTaxistas();
                await Task.Delay(Timeout, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
