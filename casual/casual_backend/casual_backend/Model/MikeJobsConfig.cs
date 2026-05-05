using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace casual_backend.Model
{
    public class MikeJobsConfig : IEntityTypeConfiguration<MikeJobs>
    {
        public void Configure(EntityTypeBuilder<MikeJobs> builder)
        {
            builder.ToTable("MikeJobs");
            builder.HasKey(j => j.Id);
            builder.Property(j => j.Title).HasMaxLength(255);
            builder.Property(j => j.Location).HasMaxLength(255);
            builder.Property(j => j.PostedAt).HasColumnType("date");
            builder.Property(j => j.UrlDetail).HasMaxLength(255);
            builder.Property(j => j.Description).HasColumnType("longtext");
            builder.Property(j => j.Salary).HasMaxLength(255);
            builder.Property(j => j.Category).HasMaxLength(255);
        }
    }
}
