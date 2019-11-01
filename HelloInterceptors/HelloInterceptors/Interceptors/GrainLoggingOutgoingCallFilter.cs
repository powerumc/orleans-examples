using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Orleans;

namespace HelloInterceptors.Interceptors
{
    public class GrainLoggingOutgoingCallFilter : IOutgoingGrainCallFilter
    {
        private void LogWrite(IOutgoingGrainCallContext context, Action action)
        {
            var hasLoggingAttribute = context.InterfaceMethod.GetCustomAttributes<GrainLoggingAttribute>().Any() ||
                                      (context.InterfaceMethod.DeclaringType?.GetCustomAttributes<GrainLoggingAttribute>().Any() ?? false);
            if (hasLoggingAttribute)
            {
                action();
            }
        }
        
        public async Task Invoke(IOutgoingGrainCallContext context)
        {
            LogWrite(context, () =>
            {
                Console.WriteLine($"{this.GetType().Name} Grain Before Invoke");
                Console.WriteLine($"{this.GetType().Name} Grain Name: {context.Grain.GetType().FullName}");
                Console.WriteLine($"{this.GetType().Name} Grain Interface Type: {context.InterfaceMethod.DeclaringType?.FullName}");
                Console.WriteLine($"{this.GetType().Name} Grain PrimaryKey: {context.Grain.GetPrimaryKeyLong()}");
                Console.WriteLine($"{this.GetType().Name} Grain Method Arguments: {string.Join(",", context.Arguments ?? new object[] {})}");
            });
            
            try
            {
                await context.Invoke();
                LogWrite(context, () => Console.WriteLine($"{this.GetType().Name} Grain After Invoke"));
            }
            catch (Exception e)
            {
                LogWrite(context, () => Console.WriteLine($"{this.GetType().Name} Grain Exception: {e}"));
                throw;
            }
            finally
            {
                LogWrite(context, () => Console.WriteLine($"{this.GetType().Name} Grain Result: {context.Result}"));
            }
        }
    }
}