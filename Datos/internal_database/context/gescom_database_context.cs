using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.internal_database.context_class;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pomelo.EntityFrameworkCore;

namespace Datos.internal_database.context;

public class gescom_database_context : DbContext
{
    public DbSet<Customer_internal_db> Customers { get; set; }
    public DbSet<GescomDatabaseCredential_internal_db> GescomDatabaseCredentials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;port=3306;database=internal_database;user=syncro_mx;password=AdelV=1043,Maxi=1043;", new MySqlServerVersion(new Version(7, 0, 0)));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer_internal_db>(entity =>
        {
            entity.ToTable("customer_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            // Otras configuraciones para la entidad Customer
            entity.HasOne(usr => usr.GescomDatabaseCredentials).WithOne(p=>p.Customer);
            entity.Ignore(usr => usr.PrestashopDatabaseCredentials);
        });
        modelBuilder.Entity<GescomDatabaseCredential_internal_db>(entity =>
        {
            entity.ToTable("gescom_database_credentials");
            entity.Property(e => e.CredentialGescomId).HasColumnName("credential_gescom_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Host).HasColumnName("host");
            entity.Property(e => e.User).HasColumnName("user");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.DatabaseName).HasColumnName("database_name");
            entity.Property(e => e.Port).HasColumnName("port");
        });
    }

}
