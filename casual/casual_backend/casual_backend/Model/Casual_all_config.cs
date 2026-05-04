using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace casual_backend.Model
{
    public class Casual_all_config : IEntityTypeConfiguration<Casual_all>
    {
        void IEntityTypeConfiguration<Casual_all>.Configure(EntityTypeBuilder<Casual_all> builder)
        {
            builder.ToTable("casual_all");
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Url_detail).IsUnique().HasDatabaseName("unique_url_detail");
            builder.Property(c => c.Title).HasMaxLength(255);
            builder.Property(c => c.Location_raw).HasMaxLength(255);
            builder.Property(c => c.Location_classified).HasMaxLength(20);
            builder.Property(c => c.Description_short).HasColumnType("longtext");
            builder.Property(c => c.Description_long).HasColumnType("longtext");
            builder.Property(c => c.Url_detail).HasMaxLength(255);
            builder.Property(c => c.Url_apply).HasMaxLength(255);
            builder.Property(c => c.Salary).HasMaxLength(255);
            builder.Property(c => c.Created_at).HasColumnType("timestamp");
            builder.Property(c => c.Category_raw).HasMaxLength(255);
            builder.Property(c => c.Category_classified).HasMaxLength(100);
            builder.Property(c => c.Listing_date).HasColumnType("date");
            builder.Property(c => c.Job_type_raw).HasMaxLength(255);
            builder.Property(c => c.Job_type_classified).HasMaxLength(50);
            builder.Property(c => c.Latitude).HasPrecision(10,7);
            builder.Property(c => c.Longitude).HasPrecision(11,7);

        }
    }
}
