using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Passageiro;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Usuario;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using System.Linq;
using System.Linq.Expressions;
using CloudMe.MotoTEX.Domain.Model;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using GeoCoordinatePortable;
using System.Globalization;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class PassageiroService : ServiceBase<Passageiro, PassageiroSummary, Guid>, IPassageiroService
    {
        private string[] defaultPaths = { "Endereco", "Usuario", "Foto", "LocalizacaoAtual" };
        private readonly IPassageiroRepository _PassageiroRepository;
        private readonly IFotoService _FotoService;
        private readonly ILocalizacaoService _LocalizacaoService;
        private readonly ICorridaRepository _corridaRepository;
        private readonly ISolicitacaoCorridaRepository _solicitacaoCorridaRepository;
        private readonly IProxyNotificacoesLocalizacao _proxyNotificacoesLocalizacao;

        public PassageiroService(
            IPassageiroRepository PassageiroRepository,
            IFotoService FotoService,
            ILocalizacaoService LocalizacaoService,
            ICorridaRepository corridaRepository,
            ISolicitacaoCorridaRepository solicitacaoCorridaRepository,
            IProxyNotificacoesLocalizacao proxyNotificacoesLocalizacao)
        {
            _PassageiroRepository = PassageiroRepository;
            _FotoService = FotoService;
            _LocalizacaoService = LocalizacaoService;
            _corridaRepository = corridaRepository;
            _solicitacaoCorridaRepository = solicitacaoCorridaRepository;
            _proxyNotificacoesLocalizacao = proxyNotificacoesLocalizacao;
        }

        public override string GetTag()
        {
            return "passageiro";
        }

        public async Task<int> ClassificacaoPassageiro(Guid id)
        {
            var avaliacoesPassageiro = await _corridaRepository.Search(
                x =>
                x.Solicitacao.IdPassageiro == id &&
                x.AvaliacaoPassageiro != null && x.AvaliacaoPassageiro != Enums.AvaliacaoUsuario.Indefinido,
                new []{ "Solicitacao" });

            if (avaliacoesPassageiro.Any())
            {
                return avaliacoesPassageiro.Sum(x => (int)x.AvaliacaoPassageiro) / avaliacoesPassageiro.Count();
            }

            return 0;
        }

        protected override async Task<Passageiro> CreateEntryAsync(PassageiroSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Passageiro
                {
                    Id = summary.Id,
                    Ativo = summary.Ativo,
                    IdUsuario = summary.Usuario.Id,
                    IdEndereco = summary.Endereco.Id,
                    IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                    IdFoto = summary.IdFoto,
                };
            });
        }

        protected override async Task<PassageiroSummary> CreateSummaryAsync(Passageiro entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                var Passageiro = new PassageiroSummary
                {
                    Id = entry.Id,
                    Ativo = entry.Ativo,
                    IdFoto = entry.IdFoto,
                    IdLocalizacaoAtual = entry.IdLocalizacaoAtual
                };

                if (entry.Endereco != null)
                {
                    Passageiro.Endereco = new EnderecoSummary()
                    {
                        Id = entry.Endereco.Id,
                        CEP = entry.Endereco.CEP,
                        Logradouro = entry.Endereco.Logradouro,
                        Numero = entry.Endereco.Numero,
                        Complemento = entry.Endereco.Complemento,
                        Bairro = entry.Endereco.Bairro,
                        Localidade = entry.Endereco.Localidade,
                        UF = entry.Endereco.UF,
                        IdLocalizacao = entry.Endereco.IdLocalizacao
                    };
                }

                if (entry.Usuario != null)
                {
                    Passageiro.Usuario = new UsuarioSummary()
                    {
                        Id = entry.Usuario.Id,
                        Nome = entry.Usuario.Nome,
                        Email = entry.Usuario.Email,
                        Telefone = entry.Usuario.PhoneNumber,
                        CPF = entry.Usuario.CPF,
                        RG = entry.Usuario.RG
                    };
                }

                return Passageiro;
            });
        }

        protected override Guid GetKeyFromSummary(PassageiroSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Passageiro> GetRepository()
        {
            return _PassageiroRepository;
        }

        protected override void UpdateEntry(Passageiro entry, PassageiroSummary summary)
        {
            entry.Ativo = summary.Ativo;

            if (!entry.IdUsuario.HasValue || entry.IdUsuario.Value == Guid.Empty)
            {
                entry.IdUsuario = summary.Usuario.Id;
            }

            //entry.IdEndereco = summary.Endereco.Id;
            //entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            //entry.IdFoto = summary.IdFoto;
        }

        protected override void ValidateSummary(PassageiroSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Passageiro: sumário é obrigatório"));
            }
        }

        /*public override async Task<Passageiro> UpdateAsync(PassageiroSummary summary)
        {
            Guid oldFotoID = Guid.Empty;

            var entry = await GetRepository().FindByIdAsync(GetKeyFromSummary(summary));
            if (entry != null)
            {
                oldFotoID = entry.IdFoto ?? Guid.Empty;
            }

            var updatedEntry = await base.UpdateAsync(summary);

            if (updatedEntry != null && oldFotoID != Guid.Empty && oldFotoID != summary.IdFoto)
            {
                // remove permanentemente a foto anterior
                await _FotoService.DeleteAsync(oldFotoID, false);
            }

            return updatedEntry;
        }*/

        public override async Task<Passageiro> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<Passageiro>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override Task<IEnumerable<Passageiro>> Search(Expression<Func<Passageiro, bool>> where, string[] paths = null, Pagination options = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, options);
        }

        public async Task<PassageiroSummary> GetByUserId(Guid Key)
        {
            var passageiro = (await base.Search(x => x.IdUsuario == Key, defaultPaths, null)).FirstOrDefault();
            if(passageiro == null)
            {
                AddNotification(new Notification("Passageiros", "Passageiro não encontrado."));
                return default;
            }

            return await CreateSummaryAsync(passageiro);
        }

        public async Task<bool> InformarLocalizacao(Guid Key, LocalizacaoSummary localizacao)
        {
            var passageiro = await Get(Key);
            if (passageiro is null)
            {
                AddNotification(new Notification("Passageiros", "Informar localização: passageiro não localizado"));
                return false;
            }

            var localizacaoSummmary = await _LocalizacaoService.GetSummaryAsync(passageiro.LocalizacaoAtual);

            localizacaoSummmary.Latitude = localizacao.Latitude;
            localizacaoSummmary.Longitude = localizacao.Longitude;
            localizacaoSummmary.IdUsuario = passageiro.IdUsuario;

            var resultado = (await _LocalizacaoService.UpdateAsync(localizacaoSummmary)) != null;

            if (resultado)
            {
                await _proxyNotificacoesLocalizacao.InformarLocalizacaoPassageiro(
                    passageiro.Id,
                    Convert.ToDouble(localizacao.Latitude, CultureInfo.InvariantCulture.NumberFormat),
                    Convert.ToDouble(localizacao.Longitude, CultureInfo.InvariantCulture.NumberFormat));
            }

            return resultado;
        }

        /*public async Task<bool> AssociarFoto(Guid Key, Guid idFoto)
        {
            var summary = await GetSummaryAsync(Key);
            if (summary.Id == null || summary.Id == Guid.Empty)
            {
                AddNotification(new Notification("AssociarFoto", "Usuário não encontrado"));
                return false;
            }

            summary.IdFoto = idFoto;

            return UpdateAsync(summary) != null;
        }*/

        /*public async Task<bool> Ativar(Guid Key, bool ativar)
        {
            var summary = await GetSummaryAsync(Key);
            if (summary.Id == null || summary.Id == Guid.Empty)
            {
                AddNotification(new Notification("AssociarFoto", "Usuário não encontrado"));
                return false;
            }

            summary.Ativo = ativar;

            return UpdateAsync(summary) != null;
        }*/

    }
}
