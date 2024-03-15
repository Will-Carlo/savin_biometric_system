using control_asistencia_savin.Frm;
using control_asistencia_savin.Frm.admin_frm;
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
        public Main()
        {
            InitializeComponent();
            //InitializeDelayTimer();
            tmrTime.Start();
            //this.FormBorderStyle = FormBorderStyle.None; // Remueve los bordes de la ventana
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana

            loadSystem();
            AbrirForm(new frmAsistencia());


            // Pidiendo datos de la tienda por dirección MAC
            //MessageBox.Show("BIENVENIDOS");
            //_m.NotificationMessage("BIENVENIDOS", "welcome");
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
            // temporizador para registro de asistencias temporales y de tipo variable
            //timer = new System.Timers.Timer();

            //// Establece el intervalo de tiempo (en milisegundos) antes de que se dispare el evento
            //// En este ejemplo, configuramos el temporizador para que se ejecute cada día a las
            //TimeSpan timeUntilNextRun = CalculateTimeUntilNextRun();
            //timer.Interval = timeUntilNextRun.TotalMilliseconds;

            //// Manejador de evento para el temporizador
            //timer.Elapsed += Timer_Elapsed;

            //// Inicia el temporizador
            //timer.Start();


            //---------------------------------------------------------------
            // verifica la conexión cada 20 minutos v2
            SetupScheduledTask();

        }
        private void loadSystem()
        {
            //MessageBox.Show(_functionsDataBase.verifyConection().ToString());
            if (_functionsDataBase.verifyConection())
            //if (_apiService._serverConexion)
            {

                _m.registrarAsistenciasTemporales();

                //MessageBox.Show("estado: " + _functionsDataBase.correctConection);
                _functionsDataBase.LimpiarDB();

                //this.Load += async (sender, e) => await _functionsDataBase.loadDataBase();
                _functionsDataBase.loadDataBase();

                this.deleteOldsBackUps();



                frmLoading loadingForm = new frmLoading();
                Application.Run(loadingForm);
            }
            else
            {
                // MessageBox.Show("Error de conexión en el servidor exitosa");

                // Environment.Exit(0);
                //this.Close();
            }
        }

        private void deleteOldsBackUps()
        {
            int deleteBackupsMonth = int.Parse(DateTime.Now.ToString("MM")) - 2;
            int deleteBackups = deleteBackupsMonth == 0 ? 12 : deleteBackupsMonth;
            //MessageBox.Show("date: " + DateTime.Now.ToString("MM") + "\nInt: " + deleteBackups.ToString());
            _functionsDataBase.DeleteBackupFiles(deleteBackups);
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

        private void lnkMarcar2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmVerificarNew());

        }

        private void lnkVerAsistencias_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AbrirForm(new frmAsistenciasPersonal());

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
            this.lnkMarcar2.Visible = actionLink;
            this.lnkVerAsistencias.Visible = actionLink;

        }
        private void LinksProduction(bool actionLink)
        {
            this.lnkInicio.Visible = actionLink;
            this.lnkMarcarCodigo.Visible = actionLink;
            //this.lnkVerAtrasos.Visible = actionLink;
            this.lnkRegistrar.Visible = actionLink;
            //this.lnkApiTest.Visible = actionLink;
            //this.lnkFakeRegister.Visible = actionLink;
            //this.lnkMarcar2.Visible = actionLink;
            //this.lnkVerAsistencias.Visible = actionLink;

        }
        // -------------------------------------------------------------------
        // REGISTRAR FALTAS
        // -------------------------------------------------------------------
        //private TimeSpan CalculateTimeUntilNextRun()
        //{
        //    // Obtenemos la hora actual
        //    DateTime now = DateTime.Now;

        //    // Establecemos la hora específica en la que deseamos que se ejecute la tarea
        //    DateTime scheduledTime;

        //    if (_m.EsSabado())
        //    {
        //        scheduledTime = new DateTime(now.Year, now.Month, now.Day, 13, 00, 01); // 1:00 PM
        //    }
        //    else
        //    {
        //        scheduledTime = new DateTime(now.Year, now.Month, now.Day, 19, 00, 01); // 7:00 PM
        //    }

        //    // Si ya pasó la hora programada de hoy, programamos la tarea para mañana a la misma hora
        //    if (now > scheduledTime)
        //    {
        //        // aquí iniiamos el procedimiento si ya pasó la hora y el programa estaba cerrado
        //        _m.RegistrarFaltasDelDiaAfterClose();
        //        scheduledTime = scheduledTime.AddDays(1);
        //    }

        //    // Calculamos el tiempo hasta la próxima ejecución
        //    TimeSpan timeUntilNextRun = scheduledTime - now;

        //    return timeUntilNextRun;
        //}
        //private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    // Detenemos el temporizador para que no vuelva a ejecutarse automáticamente
        //    timer.Stop();

        //    _m.RegistrarFaltasDelDiaAfterClose();
        //    // Reiniciamos el temporizador para el próximo día
        //    timer.Interval = 24 * 60 * 60 * 1000; // 24 horas en milisegundos
        //    timer.Start();
        //}


        // -------------------------------------------------------------------
        // INICIAR ENTRE 5 A 10
        // -------------------------------------------------------------------

        // -------------------------------------------------------------------
        // REGISTRAR ASISTENCIAS TEMPORALES Y RECARGAR PARA ASISTENCIAS DE PERSONAL DE TIPO VARIABLE
        // -------------------------------------------------------------------
        //private TimeSpan CalculateTimeUntilNextRun()
        //{
        //    // Obtenemos la hora actual
        //    DateTime now = DateTime.Now;

        //    // Establecemos la hora específica en la que deseamos que se ejecute la tarea
        //    DateTime scheduledTime;

        //    if (_m.EsSabado())
        //    {
        //        scheduledTime = new DateTime(now.Year, now.Month, now.Day, 12, 30, 01); // 12:30 PM
        //    }   
        //    else
        //    {
        //        if (now.TimeOfDay > new TimeSpan(11, 20, 0)) // Si es después de las 12:00 PM
        //        {
        //            scheduledTime = new DateTime(now.Year, now.Month, now.Day, 11, 54, 1); // 12:00 PM
        //        }
        //        else if (now.TimeOfDay > new TimeSpan(11, 50, 0)) // Si es después de las 2:00 PM
        //        {
        //            //scheduledTime = new DateTime(now.Year, now.Month, now.Day, 9, 47, 1); // 12:00 PM
        //            scheduledTime = new DateTime(now.Year, now.Month, now.Day, 11, 56, 1); // 2:00 PM
        //        }
        //        else if (now.TimeOfDay > new TimeSpan(18, 30, 0)) // Si es después de las 2:00 PM
        //        {
        //            //scheduledTime = new DateTime(now.Year, now.Month, now.Day, 9, 47, 1); // 12:00 PM
        //            scheduledTime = new DateTime(now.Year, now.Month, now.Day, 18, 30, 1); // 6:30 PM
        //        }
        //        else
        //        {
        //            scheduledTime = new DateTime(now.Year, now.Month, now.Day, 18, 50, 1); // caso por defecto
        //        }
        //    }

        //    // Si ya pasó la hora programada de hoy, programamos la tarea para mañana a la misma hora
        //    if (now > scheduledTime && _apiService.IsInternetAvailable())
        //    {
        //        // aquí iniiamos el procedimiento si ya pasó la hora y el programa estaba cerrado
        //        this.reLoad();
        //        scheduledTime = scheduledTime.AddDays(1);
        //    }

        //    // Calculamos el tiempo hasta la próxima ejecución
        //    TimeSpan timeUntilNextRun = scheduledTime - now;

        //    return timeUntilNextRun;
        //}
        //private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    // Detenemos el temporizador para que no vuelva a ejecutarse automáticamente
        //    timer.Stop();
        //    this.reLoad();
        //    // Reiniciamos el temporizador para el próximo día
        //    timer.Interval = 24 * 60 * 60 * 1000; // 24 horas en milisegundos
        //    timer.Start();
        //}
        //private void reLoad()
        //{
        //    if (_apiService.IsInternetAvailable())
        //    {
        //        _m.registrarAsistenciasTemporales();
        //        _functionsDataBase.LimpiarDB();
        //        _functionsDataBase.loadDataBase();
        //    }
        //}

        // -------------------------------------------------------------------
        // REGISTRAR ASISTENCIAS TEMPORALES Y RECARGAR PARA ASISTENCIAS DE PERSONAL DE TIPO VARIABLE V2
        // -------------------------------------------------------------------

        private void SetupScheduledTask()
        {
            // Crear un temporizador
            System.Timers.Timer timer = new System.Timers.Timer();

            // Establecer el intervalo del temporizador a 1 minuto (60000 milisegundos)
            timer.Interval = 60000;

            // Asociar un controlador de eventos al evento Elapsed del temporizador
            timer.Elapsed += ScheduledTaskHandler;

            // Iniciar el temporizador
            timer.Start();
        }
        private void ScheduledTaskHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Obtener la hora actual
            DateTime now = DateTime.Now;

            // Verificar si la hora actual está dentro del rango de ejecución deseado
            if (IsScheduledTime(now))
            {
                // Ejecutar la tarea programada
                ExecuteScheduledTask();
            }
        }
        private bool IsScheduledTime(DateTime currentTime)
        {
            // Definir los horarios deseados para ejecutar la tarea automáticamente
            TimeSpan[] scheduledTimes = new TimeSpan[]
            {
                //MAÑANA
                new TimeSpan(9, 01, 01), // 12:30 PM
                new TimeSpan(9, 11, 01), // 12:30 PM
                new TimeSpan(9, 21, 01), // 12:30 PM
                new TimeSpan(9, 31, 01), // 12:30 PM
                new TimeSpan(9, 41, 01), // 12:30 PM
                new TimeSpan(9, 51, 01), // 12:30 PM

                new TimeSpan(10, 01, 01), // 12:30 PM
                new TimeSpan(10, 11, 01), // 12:30 PM
                new TimeSpan(10, 21, 01), // 12:30 PM
                new TimeSpan(10, 31, 01), // 12:30 PM
                new TimeSpan(10, 41, 01), // 12:30 PM
                new TimeSpan(10, 51, 01), // 12:30 PM

                new TimeSpan(11, 01, 01), // 12:30 PM
                new TimeSpan(11, 11, 01), // 12:30 PM
                new TimeSpan(11, 21, 01), // 12:30 PM
                new TimeSpan(11, 31, 01), // 12:30 PM
                new TimeSpan(11, 41, 01), // 12:30 PM
                new TimeSpan(11, 51, 01), // 12:30 PM

                new TimeSpan(12, 01, 01), // 12:30 PM
                new TimeSpan(12, 11, 01), // 12:30 PM
                new TimeSpan(12, 21, 01), // 12:30 PM
                new TimeSpan(12, 31, 01), // 12:30 PM
                new TimeSpan(12, 41, 01), // 12:30 PM
                new TimeSpan(12, 51, 01), // 12:30 PM
                //TARDE
                new TimeSpan(14, 01, 01),  // 2:00 PM
                new TimeSpan(14, 11, 01),  // 2:00 PM
                new TimeSpan(14, 21, 01),  // 2:00 PM
                new TimeSpan(14, 31, 01),  // 2:00 PM
                new TimeSpan(14, 41, 01),  // 2:00 PM
                new TimeSpan(14, 51, 01),  // 2:00 PM

                new TimeSpan(15, 01, 01),  // 2:00 PM
                new TimeSpan(15, 11, 01),  // 2:00 PM
                new TimeSpan(15, 21, 01),  // 2:00 PM
                new TimeSpan(15, 31, 01),  // 2:00 PM
                new TimeSpan(15, 41, 01),  // 2:00 PM
                new TimeSpan(15, 51, 01),  // 2:00 PM

                new TimeSpan(16, 01, 01),  // 2:00 PM
                new TimeSpan(16, 11, 01),  // 2:00 PM
                new TimeSpan(16, 21, 01),  // 2:00 PM
                new TimeSpan(16, 31, 01),  // 2:00 PM
                new TimeSpan(16, 41, 01),  // 2:00 PM
                new TimeSpan(16, 51, 01),  // 2:00 PM

                new TimeSpan(17, 01, 01),  // 2:00 PM
                new TimeSpan(17, 11, 01),  // 2:00 PM
                new TimeSpan(17, 21, 01),  // 2:00 PM
                new TimeSpan(17, 31, 01),  // 2:00 PM
                new TimeSpan(17, 41, 01),  // 2:00 PM
                new TimeSpan(17, 51, 01),  // 2:00 PM

                new TimeSpan(18, 01, 01),  // 6:30 PM
                new TimeSpan(18, 11, 01),  // 6:30 PM
                new TimeSpan(18, 21, 01),  // 6:30 PM
                new TimeSpan(18, 31, 01),  // 6:30 PM
                new TimeSpan(18, 41, 01),  // 6:30 PM
                new TimeSpan(18, 51, 01),  // 6:30 PM

                new TimeSpan(19, 01, 01),  // 6:30 PM
                new TimeSpan(19, 11, 01),  // 6:30 PM
                new TimeSpan(19, 21, 01),  // 6:30 PM
                new TimeSpan(19, 31, 01),  // 6:30 PM
                new TimeSpan(19, 41, 01),  // 6:30 PM
                //new TimeSpan(12, 04, 0),  // PRUEBA
                //new TimeSpan(12, 05, 0),  // PRUEBA
                //new TimeSpan(12, 06, 0),  // PRUEBA
                //new TimeSpan(12, 07, 0)  // PRUEBA

            };

            // Verificar si la hora actual coincide con alguno de los horarios programados
            foreach (TimeSpan scheduledTime in scheduledTimes)
            {
                if (currentTime.TimeOfDay.Hours == scheduledTime.Hours &&
                    currentTime.TimeOfDay.Minutes == scheduledTime.Minutes)
                {
                    return true;
                }
            }

            return false;
        }
        private void ExecuteScheduledTask()
        {
            reLoad();
        }
        private void reLoad()
        {
            if (_functionsDataBase.verifyConection())
            //if (_apiService.IsInternetAvailable())
            {
                _m.registrarAsistenciasTemporales();
                _functionsDataBase.LimpiarDB();
                _functionsDataBase.loadDataBase();
            }
        }

       
    }
}
