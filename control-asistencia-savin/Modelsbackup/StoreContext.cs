using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace control_asistencia_savin.Models;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuxAsistencia> AuxAsistencia { get; set; }

    public virtual DbSet<GenCiudad> GenCiudads { get; set; }

    public virtual DbSet<InvAlmacen> InvAlmacens { get; set; }

    public virtual DbSet<InvSucursal> InvSucursals { get; set; }

    public virtual DbSet<RrhhAsistenciaTemporal> RrhhAsistenciaTemporals { get; set; }

    public virtual DbSet<RrhhAsistencia> RrhhAsistencia { get; set; }

    public virtual DbSet<RrhhFeriado> RrhhFeriados { get; set; }

    public virtual DbSet<RrhhPersonal> RrhhPersonals { get; set; }

    public virtual DbSet<RrhhPuntoAsistencia> RrhhPuntoAsistencia { get; set; }

    public virtual DbSet<RrhhTurno> RrhhTurnos { get; set; }

    public virtual DbSet<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=store.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuxAsistencia>(entity =>
        {
            entity.ToTable("aux_asistencia");

            entity.HasIndex(e => e.Id, "IX_aux_asistencia_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CapturaImagen).HasColumnName("capturaImagen");
            entity.Property(e => e.HoraMarcado).HasColumnName("horaMarcado");
            entity.Property(e => e.IdPersonal).HasColumnName("idPersonal");
            entity.Property(e => e.IdTurno).HasColumnName("idTurno");
            entity.Property(e => e.IndTipoMovimiento).HasColumnName("indTipoMovimiento");
            entity.Property(e => e.MinutosAtraso).HasColumnName("minutosAtraso");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.AuxAsistencia).HasForeignKey(d => d.IdPersonal);

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.AuxAsistencia).HasForeignKey(d => d.IdTurno);
        });

        modelBuilder.Entity<GenCiudad>(entity =>
        {
            entity.ToTable("gen_ciudad");

            entity.HasIndex(e => e.Id, "IX_gen_ciudad_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<InvAlmacen>(entity =>
        {
            entity.ToTable("inv_almacen");

            entity.HasIndex(e => e.Id, "IX_inv_almacen_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<InvSucursal>(entity =>
        {
            entity.ToTable("inv_sucursal");

            entity.HasIndex(e => e.Id, "IX_inv_sucursal_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.InvSucursals).HasForeignKey(d => d.IdCiudad);
        });

        modelBuilder.Entity<RrhhAsistenciaTemporal>(entity =>
        {
            entity.ToTable("rrhh_asistencia_temporal");

            entity.HasIndex(e => e.Id, "IX_rrhh_asistencia_temporal_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HoraMarcado).HasColumnName("hora_marcado");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.IdPuntoAsistencia).HasColumnName("id_punto_asistencia");
            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.IndTipoMovimiento).HasColumnName("ind_tipo_movimiento");
            entity.Property(e => e.MinutosAtraso).HasColumnName("minutos_atraso");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.RrhhAsistenciaTemporals)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdPuntoAsistenciaNavigation).WithMany(p => p.RrhhAsistenciaTemporals)
                .HasForeignKey(d => d.IdPuntoAsistencia)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.RrhhAsistenciaTemporals)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<RrhhAsistencia>(entity =>
        {
            entity.ToTable("rrhh_asistencia");

            entity.HasIndex(e => e.Id, "IX_rrhh_asistencia_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HoraMarcado).HasColumnName("hora_marcado");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.IdPuntoAsistencia).HasColumnName("id_punto_asistencia");
            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.IndTipoMovimiento).HasColumnName("ind_tipo_movimiento");
            entity.Property(e => e.MinutosAtraso).HasColumnName("minutos_atraso");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.RrhhAsistencia)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdPuntoAsistenciaNavigation).WithMany(p => p.RrhhAsistencia)
                .HasForeignKey(d => d.IdPuntoAsistencia)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.RrhhAsistencia)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<RrhhFeriado>(entity =>
        {
            entity.ToTable("rrhh_feriado");

            entity.HasIndex(e => e.Id, "IX_rrhh_feriado_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.IndTipoFeriado).HasColumnName("ind_tipo_feriado");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.RrhhFeriados).HasForeignKey(d => d.IdCiudad);
        });

        modelBuilder.Entity<RrhhPersonal>(entity =>
        {
            entity.ToTable("rrhh_personal");

            entity.HasIndex(e => e.Id, "IX_rrhh_personal_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.IndiceDerecho).HasColumnName("indice_derecho");
            entity.Property(e => e.IndiceIzquierdo).HasColumnName("indice_izquierdo");
            entity.Property(e => e.Materno).HasColumnName("materno");
            entity.Property(e => e.Nombres).HasColumnName("nombres");
            entity.Property(e => e.Paterno).HasColumnName("paterno");
            entity.Property(e => e.PulgarDerecho).HasColumnName("pulgar_derecho");
            entity.Property(e => e.PulgarIzquierdo).HasColumnName("pulgar_izquierdo");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.RrhhPersonals)
                .HasForeignKey(d => d.IdCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<RrhhPuntoAsistencia>(entity =>
        {
            entity.ToTable("rrhh_punto_asistencia");

            entity.HasIndex(e => e.Id, "IX_rrhh_punto_asistencia_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.DireccionMac).HasColumnName("direccion_mac");
            entity.Property(e => e.IdAlmacen).HasColumnName("id_almacen");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.MinutosTolerancia).HasColumnName("minutos_tolerancia");
            entity.Property(e => e.Nombre).HasColumnName("nombre");

            entity.HasOne(d => d.IdAlmacenNavigation).WithMany(p => p.RrhhPuntoAsistencia).HasForeignKey(d => d.IdAlmacen);

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.RrhhPuntoAsistencia).HasForeignKey(d => d.IdPersonal);

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.RrhhPuntoAsistencia).HasForeignKey(d => d.IdSucursal);
        });

        modelBuilder.Entity<RrhhTurno>(entity =>
        {
            entity.ToTable("rrhh_turno");

            entity.HasIndex(e => e.Id, "IX_rrhh_turno_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HoraIngreso).HasColumnName("hora_ingreso");
            entity.Property(e => e.HoraSalida).HasColumnName("hora_salida");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<RrhhTurnoAsignado>(entity =>
        {
            entity.ToTable("rrhh_turno_asignado");

            entity.HasIndex(e => e.Id, "IX_rrhh_turno_asignado_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.IdPersonal).HasColumnName("id_personal");
            entity.Property(e => e.IdPuntoAsistencia).HasColumnName("id_punto_asistencia");
            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.IndMarcadoFijoVariable).HasColumnName("ind_marcado_fijo_variable");
            entity.Property(e => e.IndTipoMarcado).HasColumnName("ind_tipo_marcado");

            entity.HasOne(d => d.IdPersonalNavigation).WithMany(p => p.RrhhTurnoAsignados)
                .HasForeignKey(d => d.IdPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdPuntoAsistenciaNavigation).WithMany(p => p.RrhhTurnoAsignados).HasForeignKey(d => d.IdPuntoAsistencia);

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.RrhhTurnoAsignados)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
