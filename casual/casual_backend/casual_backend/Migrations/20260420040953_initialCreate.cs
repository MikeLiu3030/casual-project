using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace casual_backend.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            //migrationBuilder.CreateTable(
            //    name: "casual_all",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "char(36)", nullable: false),
            //        Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Location_raw = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Location_classified = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
            //        Location_id = table.Column<int>(type: "int", nullable: true),
            //        Description_short = table.Column<string>(type: "longtext", nullable: true),
            //        Description_long = table.Column<string>(type: "longtext", nullable: true),
            //        Url_detail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Url_apply = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Salary = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Created_at = table.Column<DateTime>(type: "timestamp", nullable: true),
            //        Category_raw = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Category_classified = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
            //        Category_id = table.Column<int>(type: "int", nullable: true),
            //        Listing_date = table.Column<DateOnly>(type: "date", nullable: true),
            //        Is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
            //        Job_source_id = table.Column<int>(type: "int", nullable: true),
            //        Job_type_raw = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
            //        Job_type_classified = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
            //        Latitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: true),
            //        Longitude = table.Column<decimal>(type: "decimal(11,7)", precision: 11, scale: 7, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_casual_all", x => x.Id);
            //    })
            //    .Annotation("MySQL:Charset", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "unique_url_detail",
            //    table: "casual_all",
            //    column: "Url_detail",
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "casual_all");
        }
    }
}
