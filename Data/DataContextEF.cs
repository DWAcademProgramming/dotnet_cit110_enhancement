using System.Data; 
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    public class DataContextEF: DbContext
    {

        private IConfiguration _config; 
        public DataContextEF (IConfiguration config)
        {
            //_config = config; 
            _config = config; 
        }

        public DbSet<Computer> ? Computer {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConfiguration"),
                options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId);
                //.ToTable("Computer", "TutorialAppSchema");
        }

    }
}