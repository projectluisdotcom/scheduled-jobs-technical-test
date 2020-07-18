using System.Threading.Tasks;

namespace Api
{
    public interface IGetJobState
    {
        Task<JobsStateResponse> Get();
    }
}