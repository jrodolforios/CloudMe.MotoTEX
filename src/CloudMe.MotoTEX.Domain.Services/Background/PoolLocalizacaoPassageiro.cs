using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Background
{
    public class PoolLocalizacaoPassageiro : BackgroundService
    {
        public int Timeout { get; set; } = 5000;

        IProxyLocalizacao _ProxyNotificacoesLocalizacao;
        private readonly IServiceScopeFactory scopeFactory = null;
        private readonly IConfiguration _Configuration;

        public PoolLocalizacaoPassageiro(
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

            Timeout = _Configuration.GetSection("PoolLocalizacaoPassageiro").GetValue<int>("Timeout");

            while (!stoppingToken.IsCancellationRequested)
            {
                await _ProxyNotificacoesLocalizacao.SolicitarLocalizacaoPassageiros();
                await Task.Delay(Timeout, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
