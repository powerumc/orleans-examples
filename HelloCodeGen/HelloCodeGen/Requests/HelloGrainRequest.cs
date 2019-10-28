using System;

namespace HelloCodeGen.Requests
{
    public class HelloGrainRequest
    {
        public Guid TraceId { get; set; }
        public string Message { get; set; }
    }
}