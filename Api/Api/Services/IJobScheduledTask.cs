using System.Threading.Tasks;

namespace Api.Services
{
    public interface IJobScheduledTask
    {
        public Task Process();
    }
}