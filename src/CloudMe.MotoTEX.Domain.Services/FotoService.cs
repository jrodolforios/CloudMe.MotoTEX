using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Domain.Model.Foto;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services
{
    public class FotoService : ServiceBase<Foto, FotoSummary, Guid>, IFotoService
    {
        private readonly IFotoRepository _FotoRepository;

        public FotoService(IFotoRepository FotoRepository)
        {
            _FotoRepository = FotoRepository;
        }

        public override string GetTag()
        {
            return "foto";
        }

        protected override async Task<Foto> CreateEntryAsync(FotoSummary summary)
        {
            return await Task.Run(() =>
            {
                if (summary.Id.Equals(Guid.Empty))
                    summary.Id = Guid.NewGuid();

                return new Foto
                {
                    Id = summary.Id,
                    Nome = summary.Nome,
                    NomeArquivo = summary.NomeArquivo,
                    Dados = summary.Dados != null ? summary.Dados.ToArray() : null
                };
            });
        }

        protected override async Task<FotoSummary> CreateSummaryAsync(Foto entry)
        {
            return await Task.Run(() =>
            {
                if (entry == null) return default;

                return new FotoSummary
                {
                    Id = entry.Id,
                    Nome = entry.Nome,
                    NomeArquivo = entry.NomeArquivo,
                    Dados = entry.Dados?.ToArray()
                };
            });
        }

        protected override Guid GetKeyFromSummary(FotoSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Foto> GetRepository()
        {
            return _FotoRepository;
        }

        protected override void UpdateEntry(Foto entry, FotoSummary summary)
        {
            entry.Nome = summary.Nome;
            entry.NomeArquivo = summary.NomeArquivo;
            entry.Dados = summary.Dados != null ? summary.Dados.ToArray() : null;
        }

        protected override void ValidateSummary(FotoSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Foto: sumário é obrigatório"));
            }

            /*if (string.IsNullOrEmpty(summary.NomeArquivo))
            {
                this.AddNotification(new Notification("NomeArquivo", "Foto: nome de arquivo não fornecido"));
            }

            if (summary.Dados == null || summary.Dados.Length == 0)
            {
                this.AddNotification(new Notification("Foto", "Foto: dados não fornecidos"));
            }*/
        }
    }
}
