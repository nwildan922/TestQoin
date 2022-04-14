using Microsoft.EntityFrameworkCore;
using System;

namespace Task2.Model
{
    public class EFContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3305;database=Task1;user=root;password=P@ssw0rd";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MariaDbServerVersion(new Version(10, 7));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        public DbSet<Test01> Test01 { get; set; }
    }
}
