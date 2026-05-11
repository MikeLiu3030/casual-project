using casual_backend.Model;
using casual_backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace casual_backend.Services
{
    
    public interface IJobDataService
    {
        Task<List<MikeJobs>> GetJobsByDateAsync(DateTime targetDate);
    }
    public class JobDataService: IJobDataService
    {

        private readonly MyDbContext db;
        public JobDataService(MyDbContext db)
        {
            this.db = db;
        }

        public async Task<List<MikeJobs>> GetJobsByDateAsync(DateTime targetDate)
        {
            List<MikeJobs> Jobs = new List<MikeJobs>();
            Jobs =  await db.MikeJobs.AsNoTracking()               
                .Where(j => j.PostedAt == targetDate.Date)
                .Take(10)
                .ToListAsync();
            return Jobs;
        }
    }
}
