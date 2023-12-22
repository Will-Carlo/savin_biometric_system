using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models2;
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
                var data = await _apiService.GetDataAsync("http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-estructura-biometrico");

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
                var data = await _apiService.GetDataAsync("http://200.105.183.173:8080/savin-rest/ws/biometrico/listar-estructura-biometrico");

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
                // guardar datos de RrhhTurno
                foreach (var turno in data.RrhhTurnoAsignado)
                {
                    var existente = context.RrhhTurnoAsignados.Find(turno.Id);
                    if (existente == null)
                    {
                        context.RrhhTurnoAsignados.Add(turno);
                    }
                }

                // Repetir para las otras listas en ModelJson
                // ...

                context.SaveChanges();
            }
        }
    }
}
