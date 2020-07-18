using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IJobRepository : ICrudRepository<Guid, Job>
    {
        Task<ICollection<Job>> FindAllByState(JobState state);
    }
}