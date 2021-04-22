using Microsoft.EntityFrameworkCore;
using Movie_Store_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_API.Data
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Fluent API Many - Many
            builder.Entity<MovieDirector>().HasKey(key => new { key.IDMovie, key.IDDirector });

            builder.Entity<MovieDirector>()
                .HasOne(mv => mv.Movie)
                .WithMany(md => md.MovieDirectors)
                .HasForeignKey(key => key.IDMovie);

            builder.Entity<MovieDirector>()
                .HasOne(dir => dir.Director)
                .WithMany(md => md.MovieDirectors)
                .HasForeignKey(key => key.IDDirector);
        }


        public DbSet<Director> Directors { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieDirector> MovieDirectors { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
