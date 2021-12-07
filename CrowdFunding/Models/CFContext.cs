using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Models
{
    public class CFContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }

        public DbSet<FundingPackage> FundingPackages { get; set;}

        public CFContext(DbContextOptions options) : base(options) { }

        public CFContext() : base() { }

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
                .HasMany(p => p.Photos)
                .WithOne(p => p.Project);

            builder.Entity<Project>()
             .HasMany(p => p.Videos)
             .WithOne(p => p.Project);

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Project>()
               .HasIndex(u => u.Name)
               .IsUnique();


            base.OnModelCreating(builder);
        }

    }
}
