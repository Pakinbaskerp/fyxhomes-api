using API.Data.Models;
using API.Models;
using Entities.Util;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data
{
    /// <summary>
    /// Class <c>AppDbContext</c> manages the communication with the database.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSet properties for each of your models here.
        public DbSet<User> User { get; set; }
        public DbSet<UserLock> UserLock { get; set; }
        public DbSet<Asset> Asset { get; set;}
        public DbSet<Category> Category { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<PriceCatalogue> PriceCatalogue { get; set; }
        


        /// <summary>
        /// This method is invoked before creating tables from the model during migrations.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entities (e.g., using snake_case or any other naming convention)
            modelBuilder.HasDefaultSchema("public"); // Set default schema

            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ConvertToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ConvertToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ConvertToSnakeCase());
                }

                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    foreignKey.SetConstraintName(foreignKey.GetConstraintName().ConvertToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ConvertToSnakeCase());
                }
            }
        }

        /// <summary>
        /// This method is used to override the model attributes before saving.
        /// </summary>
        public void OnBeforeSaving(Guid UserId)
        {
            System.Collections.Generic.IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> entries = ChangeTracker.Entries();
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry in entries)
            {
                if (entry.Entity is Base trackable)
                {
                    DateTime now = DateTime.UtcNow;
                    Guid user = UserId;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.DateUpdated = now;
                            trackable.UpdatedBy = user;
                            break;
                        case EntityState.Added:
                            trackable.DateCreated = now;
                            trackable.CreatedBy = user;
                            trackable.DateUpdated = now;
                            trackable.UpdatedBy = user;
                            trackable.IsActive = true;
                            break;
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Deleted:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
