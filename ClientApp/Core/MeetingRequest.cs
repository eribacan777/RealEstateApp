using System;

namespace ClientApp.Core
{
    public class MeetingRequest
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string AgentUsername { get; set; }
        public string PropertyId { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Message { get; set; }
    }
}
