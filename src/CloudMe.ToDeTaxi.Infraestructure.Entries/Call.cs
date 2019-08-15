using System;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries
{
    public class Call : EntryBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Number { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public CallStatus Status { get; set; }
        public CallType Type { get; set; }

        public Guid? ExtensionId { get; set; }

        public Guid QueueId { get; set; }

        public Guid PhoneId { get; set; }
    }
}
