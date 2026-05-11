using casual_backend.DTO;
using casual_backend.Model;
using casual_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace casual_backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GetJobController : ControllerBase
    {
        private readonly IJobDataService jobDataService;
        private readonly DataStatisticsWorker dataStatisticsWorker;
        // Inject two services via constructor
        public GetJobController(IJobDataService jobDataService, DataStatisticsWorker dataStatisticsWorker)
            {
                this.jobDataService = jobDataService;
                this.dataStatisticsWorker = dataStatisticsWorker;
            }

        [HttpGet]
        public async Task<ActionResult<List<MikeJobs>>> GetJob([FromQuery] DateTime? date)
        {
            DateTime targetDate = date ?? DateTime.Now.AddDays(-1);
            Console.WriteLine(date);
            
            // 1. Get today's job data
            var jobs = await jobDataService.GetJobsByDateAsync(targetDate);

            // 2. Get the status of job categories for the past 7 days.
            var categoryStats = await dataStatisticsWorker.GetJobsByCategory();

            // 3. Get the distribution of job locations for the past 7 days.
            var categoryLocation = await dataStatisticsWorker.GetJobsByLocation();

            // 4. Get the distribution of job trends for the past 7 days.
            var jobTrends = await dataStatisticsWorker.GetJobTrend();

            var dashboardData = new DashboardCompositeDto
            {
                DailyJobs = jobs,
                JobsByCategory = categoryStats,
                JobsByLocation = categoryLocation,
                JobTrends = jobTrends
            };


            return Ok( dashboardData );
        }
    }

}
