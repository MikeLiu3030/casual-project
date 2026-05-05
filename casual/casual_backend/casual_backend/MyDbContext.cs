using casual_backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace casual_backend
{
    public class MyDbContext:DbContext
    {


        public DbSet<Casual_all> Casual_all {  get; set; }
        public DbSet<MikeJobs> MikeJobs { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) :base(options)
        { 
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Loading assembly
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
