using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Zabronim.Net.Models {
    public class WClientContext : DbContext {
        public DbSet<WClient> WClients { get; set; }

        public DbSet<MobileClient> MobileClients { get; set; }

        public WClientContext():base("name=ZabronimAspDb") {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}