using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GastosAPI.Models;

public partial class GastosDbContext : DbContext
{
    public GastosDbContext()
    {
    }

    public GastosDbContext(DbContextOptions<GastosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Balance> Balances { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public virtual DbSet<Lugar> Lugars { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<TasaCambio> TasaCambios { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=GastosDB;User Id=sa;Password=1234;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Balance>(entity =>
        {
            entity.HasKey(e => e.IdBalance);

            entity.ToTable("Balance");

            entity.Property(e => e.IdBalance).ValueGeneratedNever();
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("money");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Balances)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Balance_Usuario");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.Property(e => e.IdCategoria).ValueGeneratedNever();
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Icono).HasMaxLength(50);
            entity.Property(e => e.NombreCategoria).HasMaxLength(50);
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.IdIngreso);

            entity.Property(e => e.IdIngreso).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("money");

            entity.HasOne(d => d.IdBalanceNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.IdBalance)
                .HasConstraintName("FK_Ingresos_Balance");
        });

        modelBuilder.Entity<Lugar>(entity =>
        {
            entity.HasKey(e => e.IdLugar);

            entity.ToTable("Lugar");

            entity.Property(e => e.IdLugar).ValueGeneratedNever();
            entity.Property(e => e.NombreLugar).HasMaxLength(50);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago);

            entity.ToTable("MetodoPago");

            entity.Property(e => e.IdMetodoPago).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<TasaCambio>(entity =>
        {
            entity.HasKey(e => e.IdTasaCambio);

            entity.ToTable("TasaCambio");

            entity.Property(e => e.IdTasaCambio).ValueGeneratedNever();
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion);

            entity.ToTable("Transaccion");

            entity.Property(e => e.IdTransaccion).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("money");
            entity.Property(e => e.Producto).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Transaccion_Categoria");

            entity.HasOne(d => d.IdLugarNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdLugar)
                .HasConstraintName("FK_Transaccion_Lugar");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdMetodoPago)
                .HasConstraintName("FK_Transaccion_MetodoPago");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Transaccion_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.PrimerNombre).HasMaxLength(50);
            entity.Property(e => e.SegundoNombre).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
