namespace casual_backend.Model
{
    public class MikeJobs
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateTime? PostedAt { get; set; }
        public string?  UrlDetail { get; set; }
        public string? Description { get; set; }
    }
}
