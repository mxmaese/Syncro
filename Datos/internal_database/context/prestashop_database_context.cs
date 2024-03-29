using Datos.internal_database.context_class;
using Microsoft.EntityFrameworkCore;

namespace Datos.internal_database.context;

public class prestashop_database_context : DbContext
{
    public DbSet<Customer_internal_db> Customers { get; set; }
    public DbSet<PrestashopDatabaseCredential_internal_db> PrestashopDatabaseCredentials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;port=3306;database=internal_database;user=syncro_mx;password=AdelV=1043,Maxi=1043;", new MySqlServerVersion(new Version(7, 0, 0)));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer_internal_db>().HasOne(p => p.PrestashopDatabaseCredentials).WithOne(p => p.Customer);

        modelBuilder.Entity<Customer_internal_db>(entity =>
        {
            entity.ToTable("customer_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            // Otras configuraciones para la entidad Customer

            entity.Ignore(usr => usr.GescomDatabaseCredentials);
        });
        modelBuilder.Entity<PrestashopDatabaseCredential_internal_db>(entity =>
        {
            entity.ToTable("prestashop_database_credentials");
            entity.HasKey(e => e.CredentialPrestashopId);

            entity.Property(e => e.CredentialPrestashopId).HasColumnName("credential_prestashop_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Host).HasColumnName("host");
            entity.Property(e => e.User).HasColumnName("user");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.DatabaseName).HasColumnName("database_name");
            entity.Property(e => e.Port).HasColumnName("port");
            // Otras configuraciones para PrestashopDatabaseCredential
        });
    }

}