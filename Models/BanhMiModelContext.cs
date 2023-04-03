using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebBanBanhMi.Models
{
    public partial class BanhMiModelContext : DbContext
    {
        public BanhMiModelContext()
            : base("name=BanhMiModelContext")
        {
        }

        public virtual DbSet<BanhMi> BanhMis { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BanhMi>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);
        }
    }
}
