using System;

namespace HelloCodeGen.Responses
{
    public class HelloGrainResponse
    {
        public Guid TraceId { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime CreatedDateTime { get; set; } 
    }
}