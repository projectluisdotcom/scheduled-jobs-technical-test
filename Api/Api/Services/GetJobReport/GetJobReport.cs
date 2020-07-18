using System;
using System.Threading.Tasks;
using Domain;

namespace Api
{
    public class GetJobReport : IGetJobReport
    {
        private readonly IJobRepository _jobRepository;

        public GetJobReport(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<JobReportResponse> Get(string id)
        {
            var job = await _jobRepository.FindById(new Guid(id));
            
            return new JobReportResponse
            {
                Logs = job.Logs.ToArray()
            };
        }
    }
}