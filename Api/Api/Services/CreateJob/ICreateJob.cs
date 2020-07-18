using System;
using System.Threading.Tasks;

namespace Api
{
    public interface ICreateJob
    {
        Task<CreateJobResponse> Process(CreateJobsRequest createJobsRequest, DateTime now);
    }
}