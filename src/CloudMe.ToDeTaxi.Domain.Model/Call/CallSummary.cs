using System;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Domain.Model.Call
{
    public class CallSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Number { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public CallStatus Status { get; set; }
        public CallType Type { get; set; }
    }
}
