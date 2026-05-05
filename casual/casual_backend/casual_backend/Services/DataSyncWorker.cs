using casual_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace casual_backend.Services
{
    public class DataSyncWorker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public DataSyncWorker(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
            int count = 0;

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                count++; // Track sync count 统计同步次数
                try
                {
                    using var scope = serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                    // Set target date
                    DateTime targetDate = DateTime.Now.AddDays(-1);// Sync yesterday's data

                    // 1. Get the data of specified date
                    var rawJobs = await db.Casual_all
                        .AsNoTracking()
                        .Where(j => j.Listing_date == targetDate.Date)
                        .Select(j => new
                        {
                            j.Id,
                            j.Title,
                            PostedAt = j.Listing_date,
                            Location = j.Location_classified,
                            UrlDetail = j.Url_detail,
                            Description = j.Description_short,
                        })
                        .ToListAsync();

                    if (!rawJobs.Any())
                    {
                        Console.WriteLine("No data yesterday!");
                        continue; // no data
                    }

                    // 2. Remove data containing nulls in-memory (在内存中剔除含有空值的数据)
                    var validJobs = rawJobs.Where(j =>
                        !string.IsNullOrWhiteSpace(j.Title) &&
                        !string.IsNullOrWhiteSpace(j.Location) &&
                        !string.IsNullOrWhiteSpace(j.UrlDetail) &&
                        !string.IsNullOrWhiteSpace(j.Description)
                    ).ToList();

                    if (!validJobs.Any())
                    {
                        Console.WriteLine("There is no validJobs!");
                        continue;
                    }

                    // 3.1 Extract all IDs from the valid data. (提取出这批有效数据的所有Id)
                    var validJobsIds = validJobs.Select(j => j.Id).ToArray();

                    // 3.2 Identify existing IDs in the MikeJobs table (查询MikeJobs 表里面，这些Id 有哪些是已经存储过的)
                    var existingIds = await db.MikeJobs
                        .AsNoTracking()
                        .Where(m => m.PostedAt == targetDate.Date)
                        .Select(m => m.Id)
                        .ToListAsync();

                    // 3.3 Filter out existing records to avoid duplicates (再次过滤：只保留不在existingIds 里的数据)
                    var jobsToSave = validJobs
                        .Where(j => !existingIds.Contains(j.Id))
                        .Select(j => new MikeJobs
                        {
                            Id = j.Id,
                            Title = j.Title,
                            PostedAt = j.PostedAt,
                            Location = j.Location,
                            UrlDetail = j.UrlDetail,
                            Description = j.Description,
                        }).ToList();

                    if (jobsToSave.Any())
                    {
                        await db.MikeJobs.AddRangeAsync(jobsToSave);
                        await db.SaveChangesAsync();
                        Console.WriteLine($"[{DateTime.Now}] Successfully synced {jobsToSave.Count} new records.");
                    }

                    Console.WriteLine($"[{DateTime.Now}] Auto-sync cycle {count} completed。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{DateTime.Now}] Auto-sync error：{ex.Message}");
                }
            }
        }
    }
}
