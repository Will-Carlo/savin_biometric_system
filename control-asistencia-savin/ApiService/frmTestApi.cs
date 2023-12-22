using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using DPFP;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmTestApi : Form
    {
        private readonly ApiService.ApiService _apiService;
        public frmTestApi()
        {
            InitializeComponent();
            _apiService = new ApiService.ApiService();
        }

        private async void btnVerificarApi_Click(object sender, EventArgs e)
        {
            try
            {
                var data = await _apiService.GetDataAsync();

                if (data != null)
                {
                    MessageBox.Show("Conexión exitosa.", "Test de conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnCargarDB_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("haciendo la consulta");
                var data = await _apiService.GetDataAsync();

                if (data != null)
                {
                    GuardarDatosEnBaseDeDatos(data);
                }
                MessageBox.Show("Datos guardados con éxito");

            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void GuardarDatosEnBaseDeDatos(ModelJson data)
        {
            using (var context = new StoreContext())
            {
                // guardar datos de RrhhTurnoAsignado
                //foreach (var turno in data.RrhhTurnoAsignado)
                //{
                //    var existente = context.RrhhTurnoAsignados.Find(turno.Id);
                //    if (existente == null)
                //    {
                //        context.RrhhTurnoAsignados.Add(turno);
                //    }
                //}

                // Utilizar el método genérico para cada tipo de entidad
                GuardarEntidades(context, data.RrhhTurno);
                GuardarEntidades(context, data.GenCiudad);
                GuardarEntidades(context, data.InvAlmacen);

                GuardarEntidades(context, data.RrhhPersonal);
                GuardarEntidades(context, data.InvSucursal);
                GuardarEntidades(context, data.RrhhFeriado);


                GuardarEntidades(context, data.RrhhPuntoAsistencia);
                GuardarEntidades(context, data.RrhhTurnoAsignado);

                GuardarEntidades(context, data.RrhhAsistencia);

                // Confirmar todos los cambios en la base de datos
                context.SaveChanges();
            }
        }

        // Método genérico para guardar entidades
        private void GuardarEntidades<TEntity>(StoreContext context, List<TEntity> entidades) where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            try
            {
                foreach (var entidad in entidades)
                {
                    // Aquí asumo que tienes una propiedad 'Id' en todas tus entidades,
                    // y que es de tipo int. Esto puede necesitar ajustes si tu situación es diferente.
                    var idProperty = entidad.GetType().GetProperty("Id");
                    if (idProperty != null)
                    {
                        var idValue = (int)idProperty.GetValue(entidad);
                        var existente = dbSet.Find(idValue);
                        if (existente == null)
                        {
                            dbSet.Add(entidad);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en la bd", ex.Message);
            }
        }



        public void LimpiarDB()
        {
            try
            {
                using (var context = new StoreContext())
                {
                    // Borrar todas las tablas.
                    BorrarDatosDeTabla(context.GenCiudads);
                    BorrarDatosDeTabla(context.InvAlmacens);
                    BorrarDatosDeTabla(context.InvSucursals);
                    BorrarDatosDeTabla(context.RrhhAsistencia);
                    BorrarDatosDeTabla(context.RrhhFeriados);
                    BorrarDatosDeTabla(context.RrhhPersonals);
                    BorrarDatosDeTabla(context.RrhhPuntoAsistencia);
                    BorrarDatosDeTabla(context.RrhhTurnos);
                    BorrarDatosDeTabla(context.RrhhTurnoAsignados);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar base de daros" + ex.Message, "Error");
            }
        }

        private void BorrarDatosDeTabla<T>(DbSet<T> dbSet) where T : class
        {
            foreach (var entidad in dbSet)
            {
                dbSet.Remove(entidad);
            }
        }

        public void BackUpDB(string dateBackUp)
        {
            var rutaBaseDeDatos = "store.db";
            var rutaCopiaDeSeguridad = "backup/store_backup_" + dateBackUp + ".db";

            // Asegurarse de que la base de datos no está siendo utilizada
            GC.Collect();
            GC.WaitForPendingFinalizers();

            try
            {
                // Copiar el archivo de la base de datos a la ruta de copia de seguridad
                File.Copy(rutaBaseDeDatos, rutaCopiaDeSeguridad, overwrite: true);

                Console.WriteLine("La copia de seguridad se ha creado con éxito.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error al crear la copia de seguridad: " + ex.Message);
            }
        }

        private void btnDeleteBD_Click(object sender, EventArgs e)
        {
            LimpiarDB();
        }

        private void btnMakeBackUp_Click(object sender, EventArgs e)
        {
            BackUpDB("2023/12/22 18:50:00");
        }
    }
}
