using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class JobRepository : IJobRepository
    {
        private readonly MyContext _context;

        public JobRepository(MyContext context)
        {
            _context = context;
        }
        
        public async Task Save(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Job>> FindAll()
        {
            return await _context.Set<Job>().ToListAsync();
        }

        public async Task<Job> FindById(Guid id)
        {
            return await _context.Jobs
                .Include(j => j.Logs)
                .Include(j => j.DataPoints)
                .FirstOrDefaultAsync(job => job.Id.Equals(id));
        }

        public async Task Update(Job job)
        {
            _context.Set<Job>().Update(job);
            await _context.SaveChangesAsync();
        }
        
        public async Task Update(ICollection<Job> jobs)
        {
            foreach (var job in jobs)
            {
                _context.Set<Job>().Update(job);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Job job)
        {
            _context.Set<Job>().Remove(job);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Job>> FindAllByState(JobState state)
        {
            return await _context.Jobs
                .Include(j => j.Logs)
                .Include(j => j.DataPoints)
                .Where(job => job.State.Equals(state)).ToListAsync();
        }
    }
}