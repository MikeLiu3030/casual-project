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
        public GetJobController(IJobDataService jobDataService)
            {
                this.jobDataService = jobDataService;
            }

        [HttpGet]
        public async Task<ActionResult<List<MikeJobs>>> GetJob([FromQuery] DateTime? date)
        {
            DateTime targetDate = date ?? DateTime.Now.AddDays(-1);
            Console.WriteLine(date);
            var jobs = await jobDataService.GetJobsByDateAsync(targetDate);
            if (!jobs.Any())
            {
                return NotFound($"[{targetDate.Date}]:今天没有数据！");
            }

            return Ok( jobs );
        }
    }

}
