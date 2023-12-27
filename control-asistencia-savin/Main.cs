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

        public Main()
        {
            InitializeComponent();
            tmrTime.Start();

            // Pidiendo datos de la tienda por dirección MAC
            _apiService = new ApiService.ApiService();
            lblPunto.Text = "Punto: " + _apiService.nomTienda;

            //this.FormBorderStyle = FormBorderStyle.None; // Remueve los bordes de la ventana
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana
            //this.TopMost = true;
            AbrirForm(new frmAsistencia());
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
                this.lnkVerAtrasosMes.Visible = false;
                this.lnkVerAtrasos.Visible = false;
                this.lnkRegistrar.Visible = false;
                this.lnkApiTest.Visible = false;
            }
            else
            {
                this.lnkInicio.Visible = true;
                this.lnkMarcarCodigo.Visible = true;
                this.lnkVerAtrasosMes.Visible = true;
                this.lnkVerAtrasos.Visible = true;
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
    }
}
