using AgileRap_Process.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileRap_Process.Data
{
    public class AgileRap_ProcessContext : DbContext
    {
        public DbSet<Work> Work { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Status> Status{ get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WorkLog> WorkLog { get; set; }
        public DbSet<ProviderLog> ProviderLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-SEVOJJB6;Initial Catalog=AgileRapPro;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
