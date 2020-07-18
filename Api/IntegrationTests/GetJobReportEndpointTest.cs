using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTests
{
    public class GetJobReportEndpointTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GetJobReportEndpointTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CanFetchJobReports()
        {
            var client = _factory.CreateClient();
            var request = new CreateJobsRequest
            {
                Data = new []
                {
                    "random data 1",
                    "random data 2"
                },
                Type = "Bulk"
            };
            
            var jsonRequest = JsonConvert.SerializeObject(request);
            await client.PostAsync("/api/v1/job", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            
            var createResponse = await client.GetAsync("/api/v1/job/state");
            var statesBody = await createResponse.GetJsonBody<JobsStateResponse>();
            
            var reportResponse = await client.GetAsync($"/api/v1/job/report/{statesBody.States[0].Id}");
            reportResponse.EnsureSuccessStatusCode();
            var reportBody = await reportResponse.GetJsonBody<JobReportResponse>();
            Assert.NotNull(reportBody.Logs);
        }
    }
}