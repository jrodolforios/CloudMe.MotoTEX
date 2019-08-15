using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Domain.Model.Call;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public class CallService : ServiceBase<Call, CallSummary, Guid>, ICallService
    {
        private readonly ICallRepository _callRepository;

        public CallService(ICallRepository callRepository)
        {
            _callRepository = callRepository;
        }

        protected override Task<Call> CreateEntryAsync(CallSummary summary)
        {
            if (summary.Id.Equals(Guid.Empty))
                summary.Id = Guid.NewGuid();

            var call = new Call
            {
                Id = summary.Id,
                Name = summary.Name,
                CPF = summary.CPF,
                Number = summary.Number,
                Status = summary.Status,
                Type = summary.Type,
                Start = summary.Start,
                End = summary.End
            };
            return Task.FromResult(call);
        }

        protected override Task<CallSummary> CreateSummaryAsync(Call entry)
        {
            var call = new CallSummary
            {
                Id = entry.Id,
                Name = entry.Name,
                CPF = entry.CPF,
                Number = entry.Number,
                Status = entry.Status,
                Type = entry.Type,
                Start = entry.Start,
                End = entry.End
            };

            return Task.FromResult(call);
        }

        public override Task<IEnumerable<Call>> GetAll(string[] paths = null)
        {
            paths = new string[] { "Phone", "Extension", "Queue" };
            return base.GetAll(paths);
        }

        protected override Guid GetKeyFromSummary(CallSummary summary)
        {
            return summary.Id;
        }

        protected override IBaseRepository<Call> GetRepository()
        {
            return _callRepository;
        }

        protected override void UpdateEntry(Call entry, CallSummary summary)
        {
            entry.Name = summary.Name;
            entry.CPF = summary.CPF;
            entry.Number = summary.Number;
            entry.Status = summary.Status;
            entry.Type = summary.Type;
            entry.Start = summary.Start;
            entry.End = summary.End;
        }

        protected override void ValidateSummary(CallSummary summary)
        {
            if (summary is null)
            {
                this.AddNotification(new Notification("summary", "Call's summary is mandatory"));
            }

            if (string.IsNullOrEmpty(summary?.Name))
            {
                this.AddNotification(new Notification("Name", "Call's name is mandatory"));
            }

            if (string.IsNullOrEmpty(summary?.CPF))
            {
                this.AddNotification(new Notification("CPF", "Call's number is mandatory"));
            }

            if (string.IsNullOrEmpty(summary?.Number))
            {
                this.AddNotification(new Notification("Number", "Call's number is mandatory"));
            }

            if (!Regex.IsMatch(summary?.Number, @"^[0-9]*$"))
            {
                this.AddNotification(new Notification("DDD", "Call's number format is invalid"));
            }
        }
    }
}
