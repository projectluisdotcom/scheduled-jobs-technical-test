using System;
using System.Collections.Generic;

namespace Domain
{
    public class JobFactory
    {
        private readonly JobType _defaultJobType;

        public JobFactory(JobType defaultJobType)
        {
            _defaultJobType = defaultJobType; 
        }
        public Job Create(DateTime now, string jobType, string[] data)
        {
            return new Job
            {
                Id = new Guid(),
                State = JobState.Waiting,
                Type = GetJobType(jobType),
                CreatedAt = now,
                DataPoints = CreateDataPoints(data),
                
                // TODO: Register create initial log also
                Logs = new List<Log>(0)
            };
        }
        
        private List<DataPoint> CreateDataPoints(IEnumerable<string> data)
        {
            var dataPoints = new List<DataPoint>();
            foreach (var d in data)
            {
                var dataPoint = new DataPoint
                {
                    Id = new Guid(),
                    Data = d
                };
                dataPoints.Add(dataPoint);
            }

            return dataPoints;
        }

        private JobType GetJobType(string jobType)
        {
            var ok = Enum.TryParse<JobType>(jobType, out var result);

            // TODO: Launch exception when JobType is not correct
            if (!ok)
            {
                return _defaultJobType;
            }

            return result;
        }
    }
}