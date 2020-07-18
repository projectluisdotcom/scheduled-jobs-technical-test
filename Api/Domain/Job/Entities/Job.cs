using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Job
    {
        [Key]
        public Guid Id { get; set; }
        public JobState State { get; set; }
        public JobType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<DataPoint> DataPoints { get; set; }
        public List<Log> Logs { get; set; }

        public bool CanContinue()
        {
            // TODO: Avoid enum and enable Open/Close principle since more types are incoming
            return Type == JobType.Batch;
        }

        public void Started()
        {
            State = JobState.Running;
        }

        public void Finished()
        {
            State = JobState.Finished;
        }
    }
}