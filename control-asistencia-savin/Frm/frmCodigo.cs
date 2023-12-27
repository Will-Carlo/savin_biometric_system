using control_asistencia_savin.ApiService;
using control_asistencia_savin.Frm;
using control_asistencia_savin.Models;
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
    public partial class frmCodigo : VerifyForm
    {
        private MetodosAsistencia m = new MetodosAsistencia();
        private readonly ApiService.ApiService _apiService;

        public frmCodigo()
        {
            InitializeComponent();
            btnVerificarHuellaCod.Text = "Leer Código";
            lblStatusProcess.Left = 850;
            txtCodigo.Enabled = true;
            //lblStatusProcess.Enabled = false;
            _apiService = new ApiService.ApiService();
        }

        private int BuscarIdPersonal(string codigo)
        {
            using (var context = new StoreContext())
            {
                var asignacion = context.RrhhTurnoAsignados
                    .Where(ta => ta.Codigo == codigo)
                    .Select(ta => ta.IdPersonal)
                    .FirstOrDefault(); // Sincronizado

                return asignacion; // Esto será null si no se encuentra ninguna coincidencia
            }
        }

        // Devuelve el nombre del personal según su ID
        private string PersonalName(int id)
        {
            using (var context = new StoreContext())
            {
                var personal = context.RrhhPersonals
                    .Where(p => p.Id == id)
                    .Select(p => new { p.Paterno, p.Materno, p.Nombres })
                    .FirstOrDefault(); // Usamos FirstOrDefault para obtener un solo registro o null

                if (personal != null)
                {
                    return $"{personal.Nombres} {personal.Paterno} {personal.Materno}";
                }
                else
                {
                    return null;
                }
            }
        }

        private void btnVerificarHuellaCod_Click(object sender, EventArgs e)
        {
            m.setCapturaHoraMarcado(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                // buscamos el id de la persona según su código
                int IdPersonal = BuscarIdPersonal(txtCodigo.Text);
                // guardamos el código en una variaable
                String txtCod = txtCodigo.Text;

                if (txtCod != "")
                {
                    if (PersonalName(IdPersonal) != null)
                    {
                        lblStatusProcess.Text = "VERIFICADO...";
                        lblStatusProcess.ForeColor = Color.Green;
                        lblStatusProcess.Visible = true;
                        // carga los datos del empleado en el label
                        lblNombre.Text = PersonalName(IdPersonal);
                        lblHora.Text = m.getHora();
                        //Muestra en pantalla los datos y hora
                        lblNombre.Visible = true;
                        lblHora.Visible = true;

                        RrhhAsistencia regisAsis = new RrhhAsistencia()
                        {
                            IdTurno = m.capturaIdTurno(),
                            IdPersonal = IdPersonal,
                            HoraMarcado = m.getCapturaHoraMarcado(),
                            MinutosAtraso = m.capturaMinAtraso(IdPersonal),
                            IndTipoMovimiento = m.capturaIndMov(IdPersonal)
                        };
                        m.AddAsistencia(regisAsis);

                        // Enviando datos al API REST
                        var response = _apiService.RegistrarAsistenciaAsync(regisAsis);
                        //if (response != null)
                        //{
                        //    MessageBox.Show("Asistencia enviada al servidor con éxito: " + response.Status);
                        //}
                    }
                    else
                    {
                        lblStatusProcess.Text = "RECHAZADO...";
                        lblStatusProcess.ForeColor = Color.Red;
                        lblStatusProcess.Visible = true;
                        MessageBox.Show("Código incorrecto.", "Error");
                    }

                }
                else
                {
                    lblStatusProcess.Text = "";
                    lblStatusProcess.Visible = false;
                    MessageBox.Show("Debes ingresar un código.", "Error");
                }

            }
            catch (DbUpdateException ex)
            {
                // Si hay una excepción interna, muestra su mensaje; de lo contrario, muestra el mensaje de la excepción principal.
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error al guardar datos");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                lblNombre.Visible = false;
                lblHora.Visible = false;
                MessageBox.Show("error: " + ex.Message, "Error");

            }
        }
    }
}
