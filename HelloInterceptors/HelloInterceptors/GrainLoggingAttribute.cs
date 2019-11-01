using System;

namespace HelloInterceptors
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class GrainLoggingAttribute : Attribute
    {
    }
}