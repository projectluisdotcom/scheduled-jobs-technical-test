using System;
using System.Threading.Tasks;
using Domain;

namespace Api
{
    public class CreateJob : ICreateJob
    {
        private readonly IJobRepository _jobRepository;
        private readonly JobFactory _jobFactory;

        public CreateJob(IJobRepository jobRepository, JobFactory jobFactory)
        {
            _jobRepository = jobRepository;
            _jobFactory = jobFactory;
        }

        public async Task<CreateJobResponse> Process(CreateJobsRequest createJobsRequest, DateTime now)
        {
            var job = _jobFactory.Create(now, createJobsRequest.Type, createJobsRequest.Data);
            await _jobRepository.Save(job);

            return new CreateJobResponse
            {
                DataPointsCreated = createJobsRequest.Data.Length
            };
        }
    }
}