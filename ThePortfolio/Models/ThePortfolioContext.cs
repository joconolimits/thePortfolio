using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThePortfolio.Models
{
    public class ThePortfolioContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ThePortfolioContext() : base("name=ThePortfolioContext")
        {
        }

        public System.Data.Entity.DbSet<ThePortfolio.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<ThePortfolio.Models.Tag> Tags { get; set; }

        public System.Data.Entity.DbSet<ThePortfolio.Models.Skill> Skills { get; set; }

        public System.Data.Entity.DbSet<ThePortfolio.Models.Service> Services { get; set; }

        public System.Data.Entity.DbSet<ThePortfolio.Models.PortfolioItem> PortfolioItems { get; set; }

        public System.Data.Entity.DbSet<ThePortfolio.Models.JournalItem> JournalItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioItem>()
                .HasMany(up => up.Tags)
                .WithMany(tag => tag.PortfolioItems)
                .Map(mc =>
                {
                    mc.ToTable("TagPortfolioItems");
                    mc.MapLeftKey("PortfolioItem_ID");
                    mc.MapRightKey("Tag_ID");
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<ThePortfolio.ViewModels.PortfolioItemViewModel> PortfolioItemViewModels { get; set; }
    }
}
