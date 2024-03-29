using System;
using Datos.prestashop_database.context_class;
using Microsoft.EntityFrameworkCore;

namespace Datos.prestashop_database.context;

public class prestashop_order_context : DbContext
{
    
    public DbSet<ps_order> ps_order { get; set; }
    public DbSet<ps_order_detail> ps_order_detail { get; set; }
    public DbSet<ps_messege> ps_messege { get; set; }
    public DbSet<ps_product> ps_Products { get; set; }
    public DbSet<ps_address> ps_Addresses { get; set; }
    public DbSet<ps_customer_group> ps_Customer_Groups { get; set; }

    public prestashop_order_context(DbContextOptions<prestashop_order_context> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ps_order_detail>()
            .HasOne(p => p.Ps_Order)
            .WithMany(p => p.ps_order_details)
            .HasForeignKey(p => p.id_order);

        modelBuilder.Entity<ps_messege>()
            .HasOne(p => p.Ps_Order)
            .WithMany(p => p.ps_messeges)
            .HasForeignKey(p => p.id_order);

        modelBuilder.Entity<ps_order>(entity =>
        {
            entity.HasKey(e => e.id_order);
            entity.ToTable("ps_orders");
            entity.Property(e => e.id_order).HasColumnName("id_order");
            entity.Property(e => e.reference).HasColumnName("reference");
            entity.Property(e => e.id_address_delivery).HasColumnName("id_address_delivery");
            entity.Property(e => e.current_state).HasColumnName("current_state");
            entity.Property(e => e.total_paid).HasColumnName("total_paid");
            entity.Property(e => e.total_shiping).HasColumnName("total_shipping");
            entity.Property(e => e.id_carrier).HasColumnName("id_carrier");
            entity.Property(e => e.id_customer).HasColumnName("id_customer");
            // Otras configuraciones para la entidad Customer
        });
        modelBuilder.Entity<ps_order_detail>(entity =>
        {
            entity.HasKey(e => e.id_order_detail);
            entity.ToTable("ps_order_detail");
            entity.Property(e => e.id_order_detail).HasColumnName("id_order_detail");
            entity.Property(e => e.id_order).HasColumnName("id_order");
            entity.Property(e => e.product_id).HasColumnName("product_id");
            entity.Property(e => e.product_quantity).HasColumnName("product_quantity");
            entity.Property(e => e.product_price).HasColumnName("product_price");
            entity.Property(e => e.reduction_percent).HasColumnName("reduction_percent");
            entity.Property(e => e.reduction_amount).HasColumnName("reduction_amount");
        });
        modelBuilder.Entity<ps_messege>(entity =>
        {
            entity.HasKey(e => e.id_message);
            entity.ToTable("ps_message");
            entity.Property(e => e.id_message).HasColumnName("id_message");
            entity.Property(e => e.id_order).HasColumnName("id_order");
            entity.Property(e => e.message).HasColumnName("message");
            entity.Property(e => e.private_message).HasColumnName("private");
        });

        modelBuilder.Entity<ps_address>(entity =>
        {
            entity.HasKey(e => e.id_address);
            entity.ToTable("ps_address");
            entity.Property(e => e.id_address).HasColumnName("id_address");
            entity.Property(e => e.id_customer).HasColumnName("id_customer");
            entity.Property(e => e.company).HasColumnName("company");
            // Otras configuraciones para PrestashopDatabaseCredential
        });
        modelBuilder.Entity<ps_product>(entity =>
        {
            entity.HasKey(e => e.id_product);
            entity.ToTable("ps_product");
            entity.Property(e => e.id_product).HasColumnName("id_product");
            entity.Property(e => e.reference).HasColumnName("reference");
            // Otras configuraciones para PrestashopDatabaseCredential
        });
        modelBuilder.Entity<ps_customer_group>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("ps_customer_group");
            entity.Property(e => e.IdCustomer).HasColumnName("id_customer");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            // Otras configuraciones para PrestashopDatabaseCredential
        });
    }

}
