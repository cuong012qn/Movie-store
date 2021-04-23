using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Movie_Store_Data.Data
{
    public class MovieDbContextFactory : IDesignTimeDbContextFactory<MovieDBContext>
    {
        public MovieDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MovieDBContextConnection");

            var optionsBuilder = new DbContextOptionsBuilder<MovieDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new MovieDBContext(optionsBuilder.Options);
        }
    }
}
