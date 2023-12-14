using control_asistencia_savin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_asistencia_savin
{
    internal class StoreContext : DbContext
    {
        public DbSet<RrhhTurno> RrhhTurnos { get; set; }
        public DbSet<RrhhFeriado> RrhhFeriados { get; set; }
        public DbSet<GenCiudad> GenCiudades { get; set; }
        public DbSet<RrhhPersonal> RrhhPersonals { get; set; }
        public DbSet<InvSucursal> InvSucursales { get; set; }
        public DbSet<RrhhAsistencia> RrhhAsistencias { get; set; }
        public DbSet<RrhhPuntoAsistencia> RrhhPuntoAsistencias { get; set; }
        public DbSet<RrhhTurnoAsignado> RrhhTurnoAsignados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=store.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RrhhPersonal>().ToTable("rrhh_personal");


            // Configuración de la relación uno a muchos entre GenCiudad y RrhhPersonal
            modelBuilder.Entity<RrhhPersonal>()
                .HasOne(p => p.Ciudad)
                .WithMany(c => c.Personal)
                .HasForeignKey(p => p.IdCiudad);

            // Configuración de la relación uno a muchos entre GenCiudad y RrhhFeriado
            modelBuilder.Entity<RrhhFeriado>()
                .HasOne(f => f.Ciudad)
                .WithMany(c => c.Feriados)
                .HasForeignKey(f => f.IdCiudad);

            // Configuración de la relación uno a muchos entre GenCiudad y InvSucursal
            modelBuilder.Entity<InvSucursal>()
                .HasOne(s => s.Ciudad)
                .WithMany(c => c.Sucursales)
                .HasForeignKey(s => s.IdCiudad);

            // Configuración de las relaciones de RrhhAsistencia
            modelBuilder.Entity<RrhhAsistencia>()
                .HasOne(a => a.Turno)
                .WithMany(t => t.Asistencias)
                .HasForeignKey(a => a.IdTurno);
            modelBuilder.Entity<RrhhAsistencia>()
                .HasOne(a => a.Personal)
                .WithMany(p => p.Asistencias)
                .HasForeignKey(a => a.IdPersonal);

            // Configuración de la relación uno a muchos entre InvSucursal y RrhhPuntoAsistencia
            modelBuilder.Entity<RrhhPuntoAsistencia>()
                .HasOne(pa => pa.Sucursal)
                .WithMany(s => s.PuntosAsistencia)
                .HasForeignKey(pa => pa.IdSucursal);

            // Configuración de la relación uno a muchos entre RrhhTurno y RrhhTurnoAsignado
            modelBuilder.Entity<RrhhTurnoAsignado>()
                .HasOne(ta => ta.Turno)
                .WithMany(t => t.TurnosAsignados)
                .HasForeignKey(ta => ta.IdTurno);

            // Configuración de la relación uno a muchos entre RrhhPersonal y RrhhTurnoAsignado
            modelBuilder.Entity<RrhhTurnoAsignado>()
                .HasOne(ta => ta.Personal)
                .WithMany(p => p.TurnosAsignados)
                .HasForeignKey(ta => ta.IdPersonal);

            // Configuración de la relación uno a muchos entre RrhhPuntoAsistencia y RrhhTurnoAsignado
            modelBuilder.Entity<RrhhTurnoAsignado>()
                .HasOne(ta => ta.PuntoAsistencia)
                .WithMany(pa => pa.TurnosAsignados)
                .HasForeignKey(ta => ta.IdPuntoAsistencia);
        }

        public void TestConnection()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    // Intenta obtener el primer registro de la tabla rrhh_personal
                    var personal = context.RrhhPersonals.FirstOrDefault();
                    MessageBox.Show(personal != null ? "Conexión exitosa y datos encontrados." : "Conexión exitosa pero no se encontraron datos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }


    }
}
