using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Zabronim.Net.Models {
    public class WClientContext : DbContext {
        public DbSet<WClient> WClients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}