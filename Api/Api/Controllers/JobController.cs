using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IGetJobReport _getJobReport;
        private readonly IGetJobState _getJobState;
        private readonly ICreateJob _createJob;

        public JobController(ILogger<JobController> logger, IGetJobReport jobReport, IGetJobState jobState, ICreateJob createJob)
        {
            _logger = logger;
            _getJobReport = jobReport;
            _getJobState = jobState;
            _createJob = createJob;
        }
        
        [HttpPost]
        [Route("api/v1/job")]
        public async Task<CreateJobResponse> Process([FromBody] CreateJobsRequest createJobsRequest)
        {
            var now = DateTime.Now;
            var result = await _createJob.Process(createJobsRequest, now);
            return result;
        }
        
        [HttpGet]
        [Route("api/v1/job/state")]
        public async Task<JobsStateResponse> State()
        {
            var result = await _getJobState.Get();
            return result;
        }

        [HttpGet]
        [Route("api/v1/job/report/{id}")]
        public async Task<JobReportResponse> Report([FromRoute] string id)
        {
            var result = await _getJobReport.Get(id);
            return result;
        }
    }
}
