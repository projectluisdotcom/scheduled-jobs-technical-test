using System;
using System.Threading.Tasks;
using Domain;

namespace Api.Services
{
    // TODO: This class can be improved a LOT
    // TODO: Dispose and cancel tasks
    public class JobScheduledTask : IJobScheduledTask
    {
        private readonly IDataProcessor _processor;
        private readonly IJobRepository _jobRepository;

        public JobScheduledTask(IDataProcessor processor, IJobRepository jobRepository)
        {
            _processor = processor;
            _jobRepository = jobRepository;
        }
        
        public async Task Process()
        {
            var jobs = await _jobRepository.FindAllByState(JobState.Waiting);
            foreach (var job in jobs)
            {
                RegisterStarted(job, DateTime.Now);
                
                foreach (var dataPoint in job.DataPoints)
                {
                    try
                    {
                        var result =  await _processor.Process(dataPoint.Data);
                        RegisterSuccess(job, result, DateTime.Now);
                    }
                    catch (Exception e)
                    {
                        RegisterError(job, e.Message, DateTime.Now);
                        
                        if (!job.CanContinue())
                        {
                            RegisterJobCanceled(job, DateTime.Now);
                            break;
                        }
                    }
                }

                RegisterJobFinished(job, DateTime.Now);

            }
        }

        private void RegisterJobCanceled(Job job, in DateTime now)
        {
            // TODO: Extract to a Factory
            job.Finished();
            var log = new Log
            {
                Id = new Guid(),
                From = job.State,
                To = JobState.Canceled,
                Reason = "Previous Error",
                CreatedAt = now
            };
            job.Logs.Add(log);
            _jobRepository.Update(job);
        }

        private void RegisterStarted(Job job, DateTime now)
        {
            // TODO: Extract to a Factory
            job.Started();
            var log = new Log
            {
                Id = new Guid(),
                From = job.State,
                To = JobState.Running,
                Reason = "Started",
                CreatedAt = now
            };
            job.Logs.Add(log);
            _jobRepository.Update(job);
        }

        private void RegisterJobFinished(Job job, DateTime now)
        {
            // TODO: Extract to a Factory
            job.Finished();
            var log = new Log
            {
                Id = new Guid(),
                From = job.State,
                To = JobState.Finished,
                Reason = "Completed",
                CreatedAt = now
            };
            job.Logs.Add(log);
            _jobRepository.Update(job);
        }

        private void RegisterSuccess(Job job, string result, DateTime now)
        {
            // TODO: Extract to a Factory
            job.Finished();
            var log = new Log
            {
                Id = new Guid(),
                From = job.State,
                To = JobState.Running,
                Reason = result,
                CreatedAt = now
            };
            job.Logs.Add(log);
            _jobRepository.Update(job);
        }

        private void RegisterError(Job job, string error, DateTime now)
        {
            // TODO: Extract to a Factory
            var log = new Log
            {
                Id = new Guid(),
                From = job.State,
                To = JobState.Error,
                Reason = error,
                CreatedAt = now
            };
            job.Logs.Add(log);

            _jobRepository.Update(job);
        }
    }
}