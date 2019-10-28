using System;
using System.Threading.Tasks;
using Orleans;

namespace HelloDashboard.Grains
{
    public class RandomGrain : Grain, IRandomGrain
    {
        private Random random = new Random((int) DateTime.Now.Ticks);
        
        public Task<int> GetNumberAsync()
        {
            return Task.FromResult(random.Next(0, 1000));
        }
    }
}