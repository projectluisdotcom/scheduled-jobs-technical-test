using System;
using System.Threading.Tasks;
using Api;
using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests
{
    public class JobProcessorTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public JobProcessorTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanProccessJobs()
        {
            var client = _factory.CreateClient();
            var request = new CreateJobsRequest
            {
                Data = new[]
                {
                    "random data 1",
                    "random data 2"
                },
                Type = "Bulk"
            };

            var response = await client.PostJsonAsync("/api/v1/job", request);
            response.EnsureSuccessStatusCode();

            var aFewSeconds = new TimeSpan(0, 0, 10);
            await Task.Delay(aFewSeconds);
            
            var createResponse = await client.GetAsync("/api/v1/job/state");
            var statesBody = await createResponse.GetJsonBody<JobsStateResponse>();
            Assert.NotEmpty(statesBody.States);
            Assert.NotEmpty(statesBody.States[0].Id);
            Assert.Equal(JobState.Finished ,statesBody.States[0].JobState);
            
            var reportResponse = await client.GetAsync($"/api/v1/job/report/{statesBody.States[0].Id}");
            reportResponse.EnsureSuccessStatusCode();
            var reportBody = await reportResponse.GetJsonBody<JobReportResponse>();
        
            Assert.NotEmpty(reportBody.Logs);
            Assert.Equal(JobState.Running, reportBody.Logs[0].To);
        }
    }
}