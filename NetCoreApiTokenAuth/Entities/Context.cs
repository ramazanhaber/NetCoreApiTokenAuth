using Microsoft.EntityFrameworkCore;
using NetCoreApiTokenAuth.Helpers;
using NetCoreApiTokenAuth.Models;

namespace NetCoreApiTokenAuth.Entities
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer($"Data Source={Sabitler.servername};Initial Catalog={Sabitler.dbname};User ID={Sabitler.username};Password={Sabitler.password};TrustServerCertificate=True");

        }

        public DbSet<IpYetki> IpYetki { get; set; }
    }
}
