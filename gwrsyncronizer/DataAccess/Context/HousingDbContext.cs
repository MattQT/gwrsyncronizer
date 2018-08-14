using gwrsyncronizer.DataAccess.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwrsyncronizer.DataAccess.Context
{
    public class HousingDbContext : DbContext
    {
        public DbSet<Egids> Egids { get; set; }
        public DbSet<Edids> Edids { get; set; }
        public DbSet<Ewids> Ewids { get; set; }
        public HousingDbContext() : base(nameOrConnectionString: "Default")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("housing");
            base.OnModelCreating(modelBuilder);
        }
    }
}
