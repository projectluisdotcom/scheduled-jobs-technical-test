using Api.HostedServices;
using Api.Services;
using Data;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddDbContext<MyContext>(DataInstaller.Install(_env.IsDevelopment()), ServiceLifetime.Singleton);
            services.AddSingleton(typeof(IJobRepository), typeof(JobRepository));
            
            services.AddSingleton(typeof(JobType), defaultJobType => JobType.Batch );
            services.AddSingleton(typeof(JobFactory));
            services.AddSingleton(typeof(IGetJobReport), typeof(GetJobReport));
            services.AddSingleton(typeof(IGetJobState), typeof(GetJobState));
            services.AddSingleton(typeof(ICreateJob), typeof(CreateJob));

            services.AddControllers();
            services.AddHealthChecks();
            services.AddOptions();
            services.AddMvc().AddControllersAsServices();
            
            services.AddSingleton(typeof(IDataProcessor), typeof(FastFakeDataProcessor));
            services.AddSingleton(typeof(IJobScheduledTask), typeof(JobScheduledTask));
            services.AddHostedService<TimedHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapControllers();
            });
        }
    }
}
