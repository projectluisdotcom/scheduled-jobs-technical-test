using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.HostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IEnumerable<IJobScheduledTask> _processors;
        private readonly ILogger<TimedHostedService> _logger;
        
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, IEnumerable<IJobScheduledTask> processors)
        {
            _processors = processors;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            foreach (var processor in _processors)
            {
                processor.Process();
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            foreach (var processor in _processors)
            {
                // TODO: Dispose current running processors
                // processor.Dispose();
            }
            
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}