using System;

namespace CloudMe.ToDeTaxi.Infraestructure.Entries.Responses
{
    public class AgentStatusResponse
    {
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentStatus { get; set; }
        public DateTime Timestamp { get; set; }
        public string CallId { get; set; }
        public string QueueId { get; set; }
    }
}
