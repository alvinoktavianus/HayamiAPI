namespace HayamiAPI
{
    using HayamiAPI.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Context : DbContext
    {
        // Your context has been configured to use a 'Context' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HayamiAPI.Context' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Context' 
        // connection string in the application configuration file.
        public Context()
            : base("name=Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Type>().Property(t => t.TypePrice).HasPrecision(23, 6);
            modelBuilder.Entity<TransactionHd>().Property(t => t.TotalDiscount).HasPrecision(23, 6);
            modelBuilder.Entity<TransactionHd>().Property(t => t.TotalPrice).HasPrecision(23, 6);
            modelBuilder.Entity<TransactionDt>().Property(t => t.TotalPrice).HasPrecision(23, 6);
            modelBuilder.Entity<TransactionDt>().Property(t => t.AddDiscountValue).HasPrecision(23, 6);
        }

        public System.Data.Entity.DbSet<HayamiAPI.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.Type> Types { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.Model> Models { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.Counter> Counters { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.Storage> Storages { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.ProductHd> ProductHds { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.ProductDt> ProductDts { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.Discount> Discounts { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.TransactionHd> TransactionHds { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.TransactionDt> TransactionDts { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.TransactionReturHd> TransactionReturHds { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.TransactionReturDt> TransactionReturDts { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

    }
}