using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public class CFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<FundingPackage> FundingPackages { get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlServer("Data Source = localhost; Initial Catalog = CrowdFunding; Integrated Security=true");
            builder.UseSqlServer("Data Source = localhost; Initial Catalog = CrowdFunding; User ID = sa; Password = admin!@#123");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                 .HasMany(u => u.CreatedProjects)
                 .WithOne(p => p.ProjectCreator)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(a => a.BackedProjects)
                .WithMany(p => p.Backers)
                .UsingEntity<ProjectBacker>
                (
                  ap => ap.HasOne<Project>().WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(pb => pb.ProjectId),
                  ap => ap.HasOne<User>().WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(pb => pb.BackerId)
                )
                .Property(ap => ap.DateTime)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Entity<Project>()
                .HasMany(p => p.Posts)
                .WithOne(p => p.Project);

            builder.Entity<Project>()
                .HasMany(p => p.FundingPackages)
                .WithOne(p => p.Project);

            builder.Entity<Project>()
                .HasMany(p => p.Medias)
                .WithOne(p => p.Project);


            base.OnModelCreating(builder);
        }

    }
}
