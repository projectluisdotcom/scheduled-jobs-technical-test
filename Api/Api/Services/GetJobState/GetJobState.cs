using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Api
{
    public class GetJobState : IGetJobState
    {
        private readonly IJobRepository _jobRepository;

        public GetJobState(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        
        public async Task<JobsStateResponse> Get()
        {
            var jobs = await _jobRepository.FindAll();
            var states = new List<JobsStateResponse.State>();

            foreach (var job in jobs)
            {
                var state = new JobsStateResponse.State
                {
                    Id = job.Id.ToString(),
                    JobState = job.State,
                };
                states.Add(state);
            }
            
            return new JobsStateResponse
            {
                States = states.ToArray()
            };
        }
    }
}