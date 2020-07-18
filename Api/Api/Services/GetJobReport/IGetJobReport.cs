using System.Threading.Tasks;

namespace Api
{
    public interface IGetJobReport
    {
        Task<JobReportResponse> Get(string id);
    }
}