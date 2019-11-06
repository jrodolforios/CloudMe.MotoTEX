using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CloudMe.ToDeTaxi.Domain.Model.Foto;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class TaxistaService : ServiceBase<Taxista, TaxistaSummary, Guid>, ITaxistaService
    {
        private string[] defaultPaths = {"Endereco", "Usuario", "Foto"};

        private readonly ITaxistaRepository _TaxistaRepository;
        private readonly IFotoService _FotoService;

        public TaxistaService(
            ITaxistaRepository TaxistaRepository,
            IFotoService FotoService)
        {
            _TaxistaRepository = TaxistaRepository;
            _FotoService = FotoService;
        }

        protected override Task<Taxista> CreateEntryAsync(TaxistaSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Taxista = new Taxista
            {
                Id = summary.Id,
                Ativo = summary.Ativo,
                IdUsuario = summary.Usuario.Id,
                IdFoto = summary.Foto.Id,
                IdLocalizacaoAtual = summary.IdLocalizacaoAtual,
                IdPontoTaxi = summary.IdPontoTaxi,
                IdEndereco = summary.Endereco.Id,
            };
            return Task.FromResult(Taxista);
        }

        protected override Task<TaxistaSummary> CreateSummaryAsync(Taxista entry)
        {
            var Taxista = new TaxistaSummary
            {
                Id = entry.Id,
                Ativo = entry.Ativo,
                IdLocalizacaoAtual = entry.IdLocalizacaoAtual,
                IdPontoTaxi = entry.IdPontoTaxi,
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
                Foto = new FotoSummary()
                {
                    Id = entry.Foto.Id,
                    Nome = entry.Foto.Nome,
                    NomeArquivo = entry.Foto.NomeArquivo,
                    Dados = entry.Foto.Dados
                }
            };

            return Task.FromResult(Taxista);
        }

        protected override Guid GetKeyFromSummary(TaxistaSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Taxista> GetRepository()
        {
            return _TaxistaRepository;
        }

        protected override void UpdateEntry(Taxista entry, TaxistaSummary summary)
        {
            entry.Ativo = summary.Ativo;

            if(!entry.IdUsuario.HasValue || entry.IdUsuario.Value == Guid.Empty)
            {
                entry.IdUsuario = summary.Usuario.Id;
            }
            //entry.IdFoto = summary.IdFoto;
            //entry.IdLocalizacaoAtual = summary.IdLocalizacaoAtual;
            entry.IdPontoTaxi = summary.IdPontoTaxi;
            //entry.IdEndereco = summary.Endereco.Id;
        }

        protected override void ValidateSummary(TaxistaSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Taxista: sumário é obrigatório"));
            }
        }

        /*public override async Task<Taxista> UpdateAsync(TaxistaSummary summary)
        {
            Guid oldFotoID = Guid.Empty;

            var entry = await GetRepository().FindByIdAsync(GetKeyFromSummary(summary));
            if (entry != null)
            {
                oldFotoID = entry.IdFoto ?? Guid.Empty;
            }

            var updatedEntry = await base.UpdateAsync(summary);

            if(updatedEntry != null && oldFotoID != Guid.Empty && oldFotoID != summary.IdFoto)
            {
                // remove permanentemente a foto anterior
                await _FotoService.DeleteAsync(oldFotoID, false);
            }

            return updatedEntry;
        }*/

        public override async Task<Taxista> Get(Guid key, string[] paths = null)
        {
            return await base.Get(key, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override async Task<IEnumerable<Taxista>> GetAll(string[] paths = null)
        {
            return await base.GetAll(paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths);
        }

        public override IEnumerable<Taxista> Search(Expression<Func<Taxista, bool>> where, string[] paths = null, Pagination pagination = null)
        {
            return base.Search(where, paths != null ? paths.Union(defaultPaths).ToArray() : defaultPaths, pagination);
        }

        public async Task<TaxistaSummary> GetByUserId(Guid id)
        {
            TaxistaSummary returner = null;
            var taxista = _TaxistaRepository.FindAll().FirstOrDefault(x => x.IdUsuario == id);
            var taxistaSummary = await this.GetSummaryAsync(taxista.Id);

            return taxistaSummary;

        }

        /*
        public async Task<bool> AssociarFoto(Guid Key, Guid idFoto)
        {
            var summary = await GetSummaryAsync(Key);
            if (summary.Id == null || summary.Id == Guid.Empty)
            {
                AddNotification(new Notification("AssociarFoto", "Usuário não encontrado"));
                return false;
            }

            summary.IdFoto = idFoto;

            return UpdateAsync(summary) != null;
        }

        public async Task<bool> Ativar(Guid Key, bool ativar)
        {
            var summary = await GetSummaryAsync(Key);
            if (summary.Id == null || summary.Id == Guid.Empty)
            {
                AddNotification(new Notification("Ativar", "Usuário não encontrado"));
                return false;
            }

            summary.Ativo = ativar;

            return UpdateAsync(summary) != null;
        }
        */
    }
}
