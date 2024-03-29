using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos.internal_database.context_class;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

namespace Datos.internal_database.context;

public class login_internal_context : DbContext
{
    public DbSet<Customer_internal_db> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        try
        {
            base.OnConfiguring(optionsBuilder);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error al configurar el contexto de la base de datos.", ex);
        }
        optionsBuilder.UseMySql("server=localhost;port=3306;database=internal_database;user=syncro_mx;password=AdelV=1043,Maxi=1043;", new MySqlServerVersion(new Version(7, 0, 0)));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer_internal_db>(entity =>
        {
            entity.ToTable("customer_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.AccountUser).HasColumnName("account_user");
            entity.Property(e => e.AccountPass).HasColumnName("account_password");
            entity.Property(e => e.Active).HasColumnName("active");
            // Otras configuraciones para la entidad Customer
        });
    }

}
