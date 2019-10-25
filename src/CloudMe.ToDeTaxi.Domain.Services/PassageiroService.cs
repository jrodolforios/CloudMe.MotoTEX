using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using System.Linq;
using System.Linq.Expressions;
using CloudMe.ToDeTaxi.Domain.Model;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class PassageiroService : ServiceBase<Passageiro, PassageiroSummary, Guid>, IPassageiroService
    {
        private string[] defaultPaths = {"Endereco", "Usuario", "Foto"};
        private readonly IPassageiroRepository _PassageiroRepository;
        private readonly IFotoService _FotoService;

        public PassageiroService(
            IPassageiroRepository PassageiroRepository,
            IFotoService FotoService)
        {
            _PassageiroRepository = PassageiroRepository;
            _FotoService = FotoService;
        }

        protected override Task<Passageiro> CreateEntryAsync(PassageiroSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Passageiro = new Passageiro
            {
                Id = summary.Id,
                Ativo = summary.Ativo,
                IdUsuario = summary.Usuario.Id,
                IdEndereco = summary.Endereco.Id,
                IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                IdFoto = summary.IdFoto,
            };
            return Task.FromResult(Passageiro);
        }

        protected override Task<PassageiroSummary> CreateSummaryAsync(Passageiro entry)
        {
            var Passageiro = new PassageiroSummary
            {
                Id = entry.Id,
                Ativo = entry.Ativo,
                IdFoto = entry.IdFoto,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                Usuario = new UsuarioSummary()
                {
                    Id = entry.Usuario.Id,
                    Nome = entry.Usuario.Nome,
                    Email = entry.Usuario.Email,
                    Telefone = entry.Usuario.PhoneNumber,
                    CPF = entry.Usuario.CPF,
                    RG = entry.Usuario.RG
                },
                Endereco = new EnderecoSummary()
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
                },
            };

            return Task.FromResult(Passageiro);
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
            entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
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

        public override IEnumerable<Passageiro> Search(Expression<Func<Passageiro, bool>> where, string[] paths = null, Pagination options = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, options);
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
