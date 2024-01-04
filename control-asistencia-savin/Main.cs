using control_asistencia_savin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class Main : Form
    {
        // Para actualizar los modelos de la BD
        // Scaffold-DbContext "Data Source=store.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models

        private readonly ApiService.ApiService _apiService;
        private ApiService.FunctionsDataBase _functionsDataBase;
        private string _hora;
        private string _fecha;


        public Main()
        {
            InitializeComponent();
            tmrTime.Start();

            //this.FormBorderStyle = FormBorderStyle.None; // Remueve los bordes de la ventana
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana
            //this.TopMost = true;
            
            _functionsDataBase = new ApiService.FunctionsDataBase();
            _apiService = new ApiService.ApiService();

            loadSystem();
            AbrirForm(new frmAsistencia());

            // Pidiendo datos de la tienda por dirección MAC
            lblPunto.Text = "Punto: " + _apiService.nomTienda;
        }
        private void loadSystem()
        {
            _functionsDataBase.verifyConection();

            if (_functionsDataBase.correctConection)
            {

                //MessageBox.Show("estado: " + _functionsDataBase.correctConection);

                _functionsDataBase.LimpiarDB();

                //this.Load += async (sender, e) => await _functionsDataBase.loadDataBase();
                _functionsDataBase.loadDataBase();
                int deleteBackups = int.Parse(DateTime.Now.ToString("MM")) - 2;
                _functionsDataBase.DeleteBackupFiles(deleteBackups);
            }
            else
            {
                MessageBox.Show("Tu dirección MAC no está registrada.\nDir mac:"+ _apiService.macAddress() + "\nCerrando la aplicación.");
                Environment.Exit(0);
                //this.Close();
            }
        }
        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        public void AbrirForm(object subForm)
        {
            if (this.pnlBase.Controls.Count > 0)
            {
                this.pnlBase.Controls.RemoveAt(0);
            }
            Form f = subForm as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.pnlBase.Controls.Add(f);
            this.pnlBase.Tag = f;
            f.Show();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.lnkVerAtrasos.Visible)
            {
                this.lnkInicio.Visible = false;
                this.lnkMarcarCodigo.Visible = false;
                this.lnkVerAtrasos.Visible = false;
                //this.lnkVerAtrasosMes.Visible = false;
                this.lnkRegistrar.Visible = false;
                this.lnkApiTest.Visible = false;
            }
            else
            {
                this.lnkInicio.Visible = true;
                this.lnkMarcarCodigo.Visible = true;
                this.lnkVerAtrasos.Visible = true;
                //this.lnkVerAtrasosMes.Visible = true;
                this.lnkRegistrar.Visible = true;
                this.lnkApiTest.Visible = true;
            }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmAtrasos());
        }
        private void lnkRegistrar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmRegistrar());
        }
        private void lnkInicio_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmAsistencia());
        }
        private void lnkMarcarCodigo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmCodigo());
        }
        private void lnkApiTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmTestApi());

        }
        private void lblLogOut_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            this.Close();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            this.Close();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("¿Estás seguro de que quieres cerrar la aplicación?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                _hora = DateTime.Now.ToString("HH:mm:ss");
                _fecha = DateTime.Now.ToString("dd/MM/yyyy");
                _functionsDataBase.BackUpDB(_fecha.Replace("/", "_")+"_"+_hora.Replace(":", "_"));
            }
        }
    }
}
