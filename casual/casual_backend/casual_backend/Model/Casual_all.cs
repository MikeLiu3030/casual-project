using System.Reflection;

namespace casual_backend.Model
{
    public class Casual_all
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Location_raw { get; set; }
        public string? Location_classified { get; set; }
        public int? Location_id { get; set; }
        public string? Description_short { get; set; }
        public string? Description_long { get; set; }
        public string? Url_detail { get; set; }
        public string? Url_apply { get; set; }
        public string? Salary { get; set; }
        public DateTime? Created_at { get; set; }
        public string? Category_raw { get; set; }
        public string? Category_classified { get; set; }
        public int? Category_id { get; set; }
        public DateTime? Listing_date { get; set; }
        public bool Is_active { get; set; }

        public int? Job_source_id {  get; set; }
        public string? Job_type_raw { get; set; }
        public string? Job_type_classified { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }  

    }
}
