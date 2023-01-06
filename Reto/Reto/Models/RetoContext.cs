using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Reto.Models;

public partial class RetoContext : DbContext
{
    public RetoContext()
    {
    }

    public RetoContext(DbContextOptions<RetoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Motivo> Motivos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=SEBASTIAN\\WINCC; Database=Reto; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.CiudadId).HasName("PK__Ciudad__82E38B09D68073EF");

            entity.ToTable("Ciudad");

            entity.Property(e => e.CiudadId)
                .ValueGeneratedNever()
                .HasColumnName("Ciudad_ID");
            entity.Property(e => e.DepartamentoId).HasColumnName("Departamento_ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Departamento).WithMany(p => p.Ciudads)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Departamento");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Cliente__EB683FB471B3F118");

            entity.ToTable("Cliente");

            entity.Property(e => e.ClienteId)
                .ValueGeneratedNever()
                .HasColumnName("Cliente_ID");
            entity.Property(e => e.CiudadId).HasColumnName("Ciudad_ID");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DepartamentoId).HasColumnName("Departamento_ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Teléfono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CiudadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Ciudadd");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Departamentoo");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepartamentoId).HasName("PK__Departam__A22977F75B22808F");

            entity.ToTable("Departamento");

            entity.Property(e => e.DepartamentoId)
                .ValueGeneratedNever()
                .HasColumnName("Departamento_ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Motivo>(entity =>
        {
            entity.HasKey(e => e.MotivoId).HasName("PK__Motivo__66AB55D83B5EBB77");

            entity.ToTable("Motivo");

            entity.Property(e => e.MotivoId)
                .ValueGeneratedNever()
                .HasColumnName("Motivo_ID");
            entity.Property(e => e.Tipo).HasColumnType("text");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ReservaId).HasName("PK__Reserva__5F33CA4E8435E691");

            entity.ToTable("Reserva");

            entity.Property(e => e.ReservaId).HasColumnName("Reserva_ID");
            entity.Property(e => e.ClienteId).HasColumnName("Cliente_ID");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.MotivoId).HasColumnName("Motivo_ID");
            entity.Property(e => e.Observaciones).HasColumnType("text");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cliente");

            entity.HasOne(d => d.Motivo).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.MotivoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Motivo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
