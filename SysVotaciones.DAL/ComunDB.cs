using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVotaciones.EN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace SysVotaciones.DAL
{
    public  class ComunDB : DbContext
    {
        private readonly string _dbConnectionString = "Data Source=; Initial Catalog=; Integrated Security=True; TrustServerCertificate=True";
        public DbSet<Student>? Estudiante { get; set; }
        public DbSet<Role>? Role { get; set; }



        protected override  void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnectionString, sqlServerOptions =>
            {
                sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
            });
        }

    }
}
