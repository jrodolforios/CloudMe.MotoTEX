using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Foto;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
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

        protected override Task<Foto> CreateEntryAsync(FotoSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var Foto = new Foto
            {
                Id = summary.Id,
                Nome = summary.Nome,
                NomeArquivo = summary.NomeArquivo,
                Dados = summary.Dados != null ? summary.Dados.ToArray() : null
            };
            return Task.FromResult(Foto);
        }

        protected override Task<FotoSummary> CreateSummaryAsync(Foto entry)
        {
            var Foto = new FotoSummary
            {
                Id = entry.Id,
                Nome = entry.Nome,
                NomeArquivo = entry.NomeArquivo,
                Dados = entry.Dados?.ToArray()
            };

            return Task.FromResult(Foto);
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
