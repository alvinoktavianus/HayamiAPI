namespace HayamiAPI
{
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

        public System.Data.Entity.DbSet<HayamiAPI.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<HayamiAPI.Models.Model> Models { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

    }
}