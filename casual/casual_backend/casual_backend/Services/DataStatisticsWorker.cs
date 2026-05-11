using casual_backend.DTO;
using Microsoft.EntityFrameworkCore;

namespace casual_backend.Services;

public class DataStatisticsWorker
{
    private readonly MyDbContext db;

    public DataStatisticsWorker(MyDbContext db)
    {
        this.db = db;
    }

    public  async Task<List<StatItemDto>> GetJobsByCategory()
    {
        var cutoffDate = DateTime.Now.AddDays(-30);

        
        List<StatItemDto> categoryStats = await db.Casual_all
            .AsNoTracking()
            .Where(j => j.Is_active == true && j.Listing_date >= cutoffDate)
            .GroupBy(j => j.Category_classified)
            .Select(g => new StatItemDto
            {
                Name = g.Key ?? "Uncategorized",
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync();
        return categoryStats;
    }

    public async Task<List<StatItemDto>> GetJobsByLocation()
    {
        var cutoffDate = DateTime.Now.AddDays(-30);

        List<StatItemDto> categoryLocation = await db.Casual_all
            .AsNoTracking()
            .Where(j => j.Is_active == true && j.Listing_date >= cutoffDate)
            .GroupBy(j => j.Location_classified)
            .Select(g => new StatItemDto
            {
                Name = g.Key ?? "Uncategorized",
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToListAsync();
        return categoryLocation;
    }

    public async Task<List<JobTrendDto>> GetJobTrend()
    {
        var cutoffDate = DateTime.Now.AddDays(-14);
        var yesterday = DateTime.Today.AddDays(-1);

        List<JobTrendDto> jobTrends = await db.Casual_all
            .AsNoTracking()
            .Where(j => j.Is_active == true && j.Listing_date >= cutoffDate && j.Listing_date <=yesterday )
            .GroupBy(j => j.Listing_date)
            .Select(g => new JobTrendDto
            {
                Date = g.Key,
                NewJobsCount = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToListAsync();
        
        return jobTrends;
    }

}





