using control_asistencia_savin.Frm;
using control_asistencia_savin.Models;
using control_asistencia_savin.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class Main : Form
    {
        // Para actualizar los modelos de la BD
        // Scaffold-DbContext "Data Source=store.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models

        private readonly ApiService.ApiService _apiService = new ApiService.ApiService();
        private ApiService.FunctionsDataBase _functionsDataBase = new ApiService.FunctionsDataBase();
        private MetodosAsistencia _m = new MetodosAsistencia();

        private string _hora = "";
        private string _fecha = "";
        private System.Timers.Timer timer;
        private System.Timers.Timer delayTimer;
        public Main()
        {
            InitializeComponent();
            tmrTime.Start();
            //this.FormBorderStyle = FormBorderStyle.None; // Remueve los bordes de la ventana
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana
     
            loadSystem();
            AbrirForm(new frmAsistencia());


            // Pidiendo datos de la tienda por dirección MAC
            //MessageBox.Show("BIENVENIDOS");
            _m.NotificationMessage("BIENVENIDOS", "welcome");
            //notificicacionesUsuario.ShowWarningNotification("BIENVENIDOS");
            lblPunto.Text = "Punto: " + _functionsDataBase.GetNombreTienda();

            //CENTRANDO TÍTULOS
            lblSisAsis.AutoSize = true;
            lblPunto.AutoSize = true;
            int x1 = (this.pnlInfoStore.Width - lblSisAsis.Width) / 2;
            int x2 = (this.pnlInfoStore.Width - lblPunto.Width) / 2;
            int y1 = lblSisAsis.Location.Y;
            int y2 = lblPunto.Location.Y;

            lblSisAsis.Location = new System.Drawing.Point(x1, y1);
            lblPunto.Location = new System.Drawing.Point(x2, y2);


            //--------------------------------------------------------------
            // Inicializa el temporizador
            timer = new System.Timers.Timer();

            // Establece el intervalo de tiempo (en milisegundos) antes de que se dispare el evento
            // En este ejemplo, configuramos el temporizador para que se ejecute cada día a las 7:00 pM
            TimeSpan timeUntilNextRun = CalculateTimeUntilNextRun();
            timer.Interval = timeUntilNextRun.TotalMilliseconds;

            // Manejador de evento para el temporizador
            timer.Elapsed += Timer_Elapsed;

            // Inicia el temporizador
            timer.Start();
        }
        private void loadSystem()
        {

            // MessageBox.Show(_functionsDataBase.verifyConection().ToString());
            if (_functionsDataBase.verifyConection())
            {

                //MessageBox.Show("estado: " + _functionsDataBase.correctConection);

                _functionsDataBase.LimpiarDB();

                //this.Load += async (sender, e) => await _functionsDataBase.loadDataBase();
                _functionsDataBase.loadDataBase();
                int deleteBackupsMonth = int.Parse(DateTime.Now.ToString("MM")) - 2;
                int deleteBackups = deleteBackupsMonth == 0 ? 12 : deleteBackupsMonth;
                //MessageBox.Show("date: " + DateTime.Now.ToString("MM") +"\nInt: "+ deleteBackups.ToString());
                _functionsDataBase.DeleteBackupFiles(deleteBackups);
            }
            else
            {
                MessageBox.Show("Tu dirección MAC no está registrada.\nDir mac: " + _apiService._dirMac + "\nCerrando la aplicación.");
                Environment.Exit(0);
                //this.Close();
            }
        }
        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            int x1 = (this.pnlHora.Width - lblTime.Width) / 2;
            int y1 = lblTime.Location.Y;

            int x2 = (this.pnlHora.Width - lblFecha.Width) / 2;
            int y2 = lblFecha.Location.Y;

            lblTime.Location = new System.Drawing.Point(x1, y1);
            lblFecha.Location = new System.Drawing.Point(x2, y2);
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
            if (this.lnkInicio.Visible)
            {
                if (_apiService._esProduction)
                {
                    this.LinksProduction(false);
                }
                else
                {
                    this.LinksDevelopment(false);
                }
            }
            else
            {
                if (_apiService._esProduction)
                {
                    this.LinksProduction(true);
                }
                else
                {
                    this.LinksDevelopment(true);
                }
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
        private void lnkFakeRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmAsistenciaSimulation());
        }
        // FUNCIONES PARA CERRAR LA APLICACIÓN CON EL LOGOUT   
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
        // BACKUP
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
                _functionsDataBase.BackUpDB(_fecha.Replace("/", "_") + "_" + _hora.Replace(":", "_"));
            }
        }
        private void LinksDevelopment(bool actionLink)
        {
            this.lnkInicio.Visible = actionLink;
            this.lnkMarcarCodigo.Visible = actionLink;
            this.lnkVerAtrasos.Visible = actionLink;
            this.lnkRegistrar.Visible = actionLink;
            this.lnkApiTest.Visible = actionLink;
            this.lnkFakeRegister.Visible = actionLink;
        }
        private void LinksProduction(bool actionLink)
        {
            this.lnkInicio.Visible = actionLink;
            //this.lnkMarcarCodigo.Visible = actionLink;
            //this.lnkVerAtrasos.Visible = actionLink;
            this.lnkRegistrar.Visible = actionLink;
            //this.lnkApiTest.Visible = actionLink;
            //this.lnkFakeRegister.Visible = actionLink;
        }
        // -------------------------------------------------------------------
        // REGISTRAR FALTAS
        // -------------------------------------------------------------------
        private TimeSpan CalculateTimeUntilNextRun()
        {
            // Obtenemos la hora actual
            DateTime now = DateTime.Now;

            // Establecemos la hora específica en la que deseamos que se ejecute la tarea
            DateTime scheduledTime;

            if (_m.EsSabado())
            {
                scheduledTime = new DateTime(now.Year, now.Month, now.Day, 13, 00, 01); // 1:00 PM
            }
            else
            {
                scheduledTime = new DateTime(now.Year, now.Month, now.Day, 19, 00, 01); // 7:00 PM
            }

            // Si ya pasó la hora programada de hoy, programamos la tarea para mañana a la misma hora
            if (now > scheduledTime)
            {
                // aquí iniiamos el procedimiento si ya pasó la hora y el programa estaba cerrado
                _m.RegistrarFaltasDelDiaAfterClose();
                scheduledTime = scheduledTime.AddDays(1);
            }

            // Calculamos el tiempo hasta la próxima ejecución
            TimeSpan timeUntilNextRun = scheduledTime - now;

            return timeUntilNextRun;
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Detenemos el temporizador para que no vuelva a ejecutarse automáticamente
            timer.Stop();

            _m.RegistrarFaltasDelDiaAfterClose();
            // Reiniciamos el temporizador para el próximo día
            timer.Interval = 24 * 60 * 60 * 1000; // 24 horas en milisegundos
            timer.Start();
        }


    }
}
