using System.Threading.Tasks;
using Orleans;

namespace HelloDashboard
{
    public interface IRandomGrain : IGrainWithIntegerKey
    {
        Task<int> GetNumberAsync();
    }
}