using casual_backend.Model;

namespace casual_backend.DTO;
public class StatItemDto
{
    public string Name { get; set; }
    public int Count { get; set; }
}

public class JobTrendDto
{
    public DateTime? Date { get; set; }
    public int NewJobsCount { get; set; }
}


public class DashboardCompositeDto
{
    public List<MikeJobs>? DailyJobs { get; set; }
    public DateTime GenerateAt { get; set; }
    public List<StatItemDto>? JobsByCategory {  get; set; }
    public List<StatItemDto>? JobsByLocation { get; set; }
    public List<JobTrendDto>? JobTrends {  get; set; }
}

