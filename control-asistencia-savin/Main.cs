﻿using control_asistencia_savin.Frm;
using control_asistencia_savin.Frm.admin_frm;
using control_asistencia_savin.Models;
using control_asistencia_savin.Notifications;
using control_asistencia_savin.WindowsSystemValidations;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
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
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private ApiService.Credenciales _cd = new ApiService.Credenciales();

        private string _hora = "";
        private string _fecha = "";
        private System.Timers.Timer timer;

        // ejecutar el programa siempre en segundo plano
        private NotifyIcon notifyIcon;

        // enviar logs a servidor
        private FileSystemWatcher _watcher;
        private string _logsDirectory;
        private string _macAddress;
        private string _serverUrl = "https://level-hill-gander.glitch.me/upload";
        // forzar el envio de logs
        private System.Timers.Timer _timer;
        private Dictionary<string, DateTime> _fileWriteTimes;

        public Main()
        {
            InitializeComponent();

            _logger = LoggingManager.GetLogger<Main>();
            _logger.LogInformation($"\n#################### INICIANDO LA APLICACIÓN {_cd._versionApp} ####################");
            string typeConexion = _apiService._esProduction ? "production" : "development";
            _logger.LogDebug($"-> Loading data for {typeConexion}...");
            this.Text = $"SAVIN {_cd._versionApp} - CONTROL BIOMÉTRICO";

            //InitializeDelayTimer();
            tmrTime.Start();
            //this.FormBorderStyle = FormBorderStyle.None; // Remueve los bordes de la ventana
            this.WindowState = FormWindowState.Maximized; // Maximiza la ventana

            loadSystem();
            AbrirForm(new frmAsistencia());

            _logger.LogDebug($"-> Punto: {_functionsDataBase.GetNombreTienda()}.");
            // Pidiendo datos de la tienda por dirección MAC
            //MessageBox.Show("BIENVENIDOS");
            //_m.NotificationMessage("BIENVENIDOS", "welcome");
            //notificicacionesUsuario.ShowWarningNotification("BIENVENIDOS");

            lblPunto.Text = "Punto: " + _functionsDataBase.GetNombreTienda();

            //pctWarning.Visible = false;
            //lnkSincronizarRegistros.Visible = false;

            //CENTRANDO TÍTULOS
            lblSisAsis.AutoSize = true;
            lblPunto.AutoSize = true;
            int x1 = (this.pnlInfoStore.Width - lblSisAsis.Width) / 2;
            int x2 = (this.pnlInfoStore.Width - lblPunto.Width) / 2;
            int y1 = lblSisAsis.Location.Y;
            int y2 = lblPunto.Location.Y;

            lblSisAsis.Location = new System.Drawing.Point(x1, y1);
            lblPunto.Location = new System.Drawing.Point(x2, y2);

            //---------------------------------------------------------------
            // verifica la conexión cada 20 minutos v2
            if (_apiService._esProduction)
            {
                _logger.LogDebug("Se ha activado la función de tarea programada.");
                _logger.LogDebug("Se ha activado la función de enviar logs a servidor.");
                SetupScheduledTask();
                inicializarVariablesEnvioLog();
            }
            else
            {
                _logger.LogDebug("Se ha desactivado la función de tarea programada.");
                _logger.LogDebug("Se ha desactivado la función de enviar logs a servidor.");
            }

            //---------------------------------------------------------------
            // Configuración del NotifyIcon
            notifyIcon = new NotifyIcon();

            //string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string iconPath = System.IO.Path.Combine("img", "savin_2.ico");

            if (System.IO.File.Exists(iconPath))
            {
                notifyIcon.Icon = new Icon(iconPath);
            }
            else
            {
                notifyIcon.Icon = SystemIcons.Application;
            }

            notifyIcon.Visible = true;
            notifyIcon.Text = "Control Asistencia Savin";
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            // Configuración del menú contextual del NotifyIcon
            var contextMenu = new ContextMenuStrip();
            var exitMenuItem = new ToolStripMenuItem("Salir");
            exitMenuItem.Click += ExitMenuItem_Click;
            contextMenu.Items.Add(exitMenuItem);
            notifyIcon.ContextMenuStrip = contextMenu;


            // ENVIAR LOGS _apiService._dirMac


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

                _logger.LogInformation("Sistema cargado correctamente.");

                frmLoading loadingForm = new frmLoading();
                Application.Run(loadingForm);
            }
            else
            {
                if (_functionsDataBase.existenRegistrosSinSincronizar())
                {
                    advertenciaDeSincronizacion(true);
                }
                _logger.LogError("Error al cargar el sistema.");

                // Environment.Exit(0);
                //this.Close();
            }
        }
        // -------------------------------------------------------------------
        // BACKUP
        // -------------------------------------------------------------------

        private void deleteOldsBackUps()
        {
            int deleteBackupsMonth = int.Parse(DateTime.Now.ToString("MM")) - 2;

            int deleteBackups = deleteBackupsMonth == 0 ? 12 : deleteBackupsMonth;
            //_logger.LogInformation($"Eliminando old backups. Mes: {deleteBackupsMonth}, Mes a enviar: {deleteBackups}");

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

        // -------------------------------------------------------------------
        // GAME OF LINKS
        // -------------------------------------------------------------------

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
            //this.lnkRegistrar.Visible = actionLink;
            //this.lnkApiTest.Visible = actionLink;
            //this.lnkFakeRegister.Visible = actionLink;
            //this.lnkMarcar2.Visible = actionLink;
            //this.lnkVerAsistencias.Visible = actionLink;

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

            string formName = f.GetType().Name;
            _logger.LogDebug($"Abriendo formulario: {formName}");

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

        // -------------------------------------------------------------------
        // FUNCIONES PARA CERRAR LA APLICACIÓN CON EL LOGOUT 
        // -------------------------------------------------------------------

        private void lblLogOut_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            _logger.LogInformation("Cerrando aplicación por logout");
            cerrarYCrearBackUp();

            //this.Close();

            ejecutarEnSegundoPlano();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            _logger.LogInformation("Cerrando aplicación por logout");
            cerrarYCrearBackUp();

            //this.Close();

            ejecutarEnSegundoPlano();
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("¿Estás seguro de que quieres cerrar la aplicación?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                _logger.LogWarning("Cerrando la aplicación. (false)");
            }
            else
            {
                e.Cancel = true;
                _logger.LogWarning("Cerrando la aplicación. (true)");
                cerrarYCrearBackUp();

                ejecutarEnSegundoPlano();
            }
        }
        private void cerrarYCrearBackUp()
        {
            _hora = DateTime.Now.ToString("HH:mm:ss");
            _fecha = DateTime.Now.ToString("dd/MM/yyyy");
            _functionsDataBase.BackUpDB(_fecha.Replace("/", "_") + "_" + _hora.Replace(":", "_"));
            _logger.LogInformation("\n#################### CERRANDO LA APLICACIÓN ####################");

            //LoggingManager.CloseAndFlush();
        }

        private void ejecutarEnSegundoPlano()
        {
            this.Hide();
            notifyIcon.ShowBalloonTip(1000, "Control Asistencia Savin", "La aplicación se está ejecutando en segundo plano.", ToolTipIcon.Info);
            _logger.LogInformation("\nLa aplicación se ha ocultado y sigue ejecutándose en segundo plano.");
        }

        // -------------------------------------------------------------------
        // REGISTRAR FALTAS
        // -------------------------------------------------------------------

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
                new TimeSpan(6, 01, 01), // 12:30 PM
                new TimeSpan(6, 11, 01), // 12:30 PM
                new TimeSpan(6, 21, 01), // 12:30 PM
                new TimeSpan(6, 31, 01), // 12:30 PM
                new TimeSpan(6, 41, 01), // 12:30 PM
                new TimeSpan(6, 51, 01), // 12:30 PM

                new TimeSpan(7, 01, 01), // 12:30 PM
                new TimeSpan(7, 11, 01), // 12:30 PM
                new TimeSpan(7, 21, 01), // 12:30 PM
                new TimeSpan(7, 31, 01), // 12:30 PM
                new TimeSpan(7, 41, 01), // 12:30 PM
                new TimeSpan(7, 51, 01), // 12:30 PM

                //MAÑANA
                new TimeSpan(8, 01, 01), // 12:30 PM
                new TimeSpan(8, 11, 01), // 12:30 PM
                new TimeSpan(8, 21, 01), // 12:30 PM
                new TimeSpan(8, 31, 01), // 12:30 PM
                new TimeSpan(8, 41, 01), // 12:30 PM
                new TimeSpan(8, 51, 01), // 12:30 PM

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
                new TimeSpan(10, 56, 01), // 12:30 PM
                
                new TimeSpan(10, 58, 01), // 12:30 PM

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

                new TimeSpan(19, 01, 01),  // 7:30 PM
                new TimeSpan(19, 11, 01),  // 7:30 PM
                new TimeSpan(19, 21, 01),  // 7:30 PM
                new TimeSpan(19, 31, 01),  // 7:30 PM
                new TimeSpan(19, 41, 01),  // 7:30 PM
                new TimeSpan(19, 51, 01),  // 7:30 PM

                new TimeSpan(20, 01, 01),  // 7:30 PM
                new TimeSpan(20, 11, 01),  // 7:30 PM
                new TimeSpan(20, 21, 01),  // 7:30 PM
                new TimeSpan(20, 31, 01),  // 7:30 PM
                new TimeSpan(20, 41, 01),  // 7:30 PM
                new TimeSpan(20, 51, 01),  // 7:30 PM
                
                new TimeSpan(21, 01, 01),  // 7:30 PM
                new TimeSpan(21, 11, 01),  // 7:30 PM
                new TimeSpan(21, 21, 01),  // 7:30 PM
                new TimeSpan(21, 31, 01),  // 7:30 PM
                new TimeSpan(21, 41, 01),  // 7:30 PM
                new TimeSpan(21, 51, 01),  // 7:30 PM
                                
                new TimeSpan(22, 01, 01),  // 7:30 PM
                new TimeSpan(22, 11, 01),  // 7:30 PM
                new TimeSpan(22, 21, 01),  // 7:30 PM
                new TimeSpan(22, 31, 01),  // 7:30 PM
                new TimeSpan(22, 41, 01),  // 7:30 PM
                new TimeSpan(22, 51, 01),  // 7:30 PM
                                                
                new TimeSpan(23, 01, 01),  // 7:30 PM
                new TimeSpan(23, 11, 01),  // 7:30 PM
                new TimeSpan(23, 21, 01),  // 7:30 PM
                new TimeSpan(23, 31, 01),  // 7:30 PM
                new TimeSpan(23, 41, 01),  // 7:30 PM
                new TimeSpan(23, 51, 01),  // 7:30 PM
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
            _logger.LogInformation("\n-> TAREA PROGRAMADA: \nIniciando la sincronización automática del sistema...");
            if (_functionsDataBase.verifyConection())
            //if (_apiService.IsInternetAvailable())
            {
                _m.registrarAsistenciasTemporales();
                _functionsDataBase.LimpiarDB();
                _functionsDataBase.loadDataBase();
            }else{
                _logger.LogDebug("No hay conexión.");
                if (_functionsDataBase.existenRegistrosSinSincronizar())
                {
                    advertenciaDeSincronizacion(true);
                }

            }
        }
        private void advertenciaDeSincronizacion(bool mostrarAdvertencia)
        {
            if (this.InvokeRequired)
            {
                // Si el llamado viene de un hilo diferente al hilo de la UI,
                // usar el método Invoke con una expresión lambda para ejecutar mostrarAdvertenciaInterfaz en el hilo de la UI.
                this.Invoke((System.Windows.Forms.MethodInvoker)delegate {
                    mostrarAdvertenciaInterfaz(mostrarAdvertencia);
                });
            }
            else
            {
                // Este código se ejecuta solo si estamos en el hilo de la UI.
                mostrarAdvertenciaInterfaz(mostrarAdvertencia);
            }
        }



        private void mostrarAdvertenciaInterfaz(bool mostrarAdvertencia)
        {
            pctWarning.Visible = mostrarAdvertencia;
            lnkSincronizarRegistros.Visible = mostrarAdvertencia;
        }

        private void lnkSincronizarRegistros_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _logger.LogInformation("--- INICIA EL PROCESO DE SINCRONIZACIÓN (clic) ---");
            if (_functionsDataBase.existenRegistrosSinSincronizar())
            {
                if (_functionsDataBase.verifyConection())
                //if (_apiService.IsInternetAvailable())
                {
                    _m.registrarAsistenciasTemporales();
                    _functionsDataBase.LimpiarDB();
                    _functionsDataBase.loadDataBase();
                    advertenciaDeSincronizacion(false);
                    MessageBox.Show("Se ha sincronizado con éxito.");
                }
                else
                {
                    _logger.LogDebug("No se pudo sincronizar el backup.");
                    MessageBox.Show("No se pudo conectar a la red. \nInténtalo más tarde", "Sincronizar datos");
                }
                
            }
            else
            {
                MessageBox.Show("Se ha sincronizado con éxito.");

                _logger.LogInformation("No existen datos para sincronizar.");
                advertenciaDeSincronizacion(false);
            }

            _logger.LogInformation("--- FINALIZA EL PROCESO DE SINCRONIZACIÓN ---");


        }

        // -------------------------------------------------------------------
        // EJECUTAR EL PROGRAMA SIEMPRE EN SEGUNDO PLANO
        // -------------------------------------------------------------------
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            // Mostrar el formulario cuando se haga doble clic en el icono de la bandeja del sistema
            //this.Show();
            //this.WindowState = FormWindowState.Normal;
            //this.BringToFront();
            //this.Activate();

            ShowMe();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            // Cerrar la aplicación cuando se seleccione "Salir" en el menú contextual del NotifyIcon
            notifyIcon.Visible = false;
            _logger.LogInformation("\n#################### LA APLICACIÓN SE HA CERRADO DEFINITIVAMENTE ####################");
            LoggingManager.CloseAndFlush(); // Cierra y vacía el registro
            Environment.Exit(0); // Cierra la aplicación

        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // Llamamos a BringToFront para asegurarnos de que la ventana está en el frente
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
            Show();
        }

        public static class NativeMethods
        {
            public const int WM_SHOWME = 0x8001; // Número personalizado para el mensaje
        }

        // -------------------------------------------------------------------
        // ENVIAR LOGS DEL SISTEMA
        // -------------------------------------------------------------------

        private void inicializarVariablesEnvioLog()
        {
            // Establecer el directorio de trabajo actual en el directorio raíz del proyecto
            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            // Configurar la ruta de la carpeta de logs en el directorio raíz del proyecto
            //_logsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            _logsDirectory = "logs";
            EnsureLogsDirectoryExists();
            _macAddress = _apiService._dirMac;

            InitializeFileSystemWatcher();
        }

        private void InitializeFileSystemWatcher()
        {
            _watcher = new FileSystemWatcher
            {
                Path = _logsDirectory,
                Filter = "*.log",
                //NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size,
                NotifyFilter = NotifyFilters.LastWrite
                    | NotifyFilters.FileName
                    | NotifyFilters.Size
                    | NotifyFilters.LastAccess
                    | NotifyFilters.CreationTime,
                EnableRaisingEvents = true
            };

            _watcher.Changed += OnLogFileChanged;
            _watcher.Created += OnLogFileChanged;
            _watcher.Renamed += OnLogFileChanged;
            //_watcher.Deleted += OnLogFileChanged;

            // Inicializar el temporizador
            _timer = new System.Timers.Timer(1000); // Verificar cada segundo
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();

            _fileWriteTimes = new Dictionary<string, DateTime>();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var logFiles = Directory.GetFiles(_logsDirectory, "*.log");
            foreach (var file in logFiles)
            {
                var lastWriteTime = File.GetLastWriteTime(file);
                if (_fileWriteTimes.ContainsKey(file))
                {
                    if (_fileWriteTimes[file] != lastWriteTime)
                    {
                        _fileWriteTimes[file] = lastWriteTime;
                        UploadLogFile(file).Wait(); // Forzar la carga del archivo si ha cambiado
                    }
                }
                else
                {
                    _fileWriteTimes[file] = lastWriteTime;
                }
            }
        }


        private async void OnLogFileChanged(object sender, FileSystemEventArgs e)
        {
            //_logger.LogDebug("Detected change in file: " + e.FullPath);
            await Task.Delay(500); // Pequeño retraso para asegurar que el archivo se haya escrito completamente
            await UploadLogFile(e.FullPath);
        }


        private async Task UploadLogFile(string filePath)
        {
            //_logger.LogDebug("_dirMac: " + _macAddress);
            //_logger.LogDebug("Uploading file: " + filePath);

            const int MaxAttempts = 5; // Número máximo de intentos
            const long MaxFileSize = 50000000; // Tamaño máximo del archivo en bytes (ejemplo: 50 MB)
            int attempts = 0;

            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > MaxFileSize)
            {
                _logger.LogError($"File {filePath} is too large to upload. Max size allowed: {MaxFileSize} bytes.");
                return;
            }

            while (true)
            {
                try
                {
                    //var handler = new HttpClientHandler
                    //{
                    //    MaxRequestContentBufferSize = 256000000 // Tamaño máximo del buffer de contenido de la solicitud en bytes (ejemplo: 256 MB)
                    //};

                    using (var client = new HttpClient())
                    using (var content = new MultipartFormDataContent())
                    {
                        // Intentar abrir el archivo con acceso compartido
                        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var fileContent = new StreamContent(fileStream))
                        {
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            content.Add(fileContent, "log", Path.GetFileName(filePath));

                            client.DefaultRequestHeaders.Add("x-mac-address", _macAddress);
                            client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.39.0");
                            client.DefaultRequestHeaders.Add("Accept", "*/*");
                            client.DefaultRequestHeaders.Add("Host", "level-hill-gander.glitch.me");
                            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

                            //_logger.LogDebug("Sending request to: " + _serverUrl);
                            //_logger.LogDebug("Request Headers: " + client.DefaultRequestHeaders.ToString());
                            //_logger.LogDebug("File content: " + fileContent.Headers.ToString());

                            var response = await client.PostAsync(_serverUrl, content);
                            //_logger.LogDebug("Response status code: " + response.StatusCode);
                            //_logger.LogDebug("Response reason phrase: " + response.ReasonPhrase);

                            if (response.IsSuccessStatusCode)
                            {
                                //_logger.LogDebug($"File {filePath} uploaded successfully.");
                            }
                            else
                            {
                                //_logger.LogDebug($"Error uploading file {filePath}: {response.ReasonPhrase}");
                            }
                        }
                    }
                    break; // Salir del bucle si todo va bien
                }
                catch (IOException ex)
                {
                    if (++attempts == MaxAttempts)
                    {
                        //_logger.LogError($"Failed to upload file {filePath} after {MaxAttempts} attempts. Exception: {ex.Message}");
                        break; // Salir del bucle después de varios intentos fallidos
                    }
                    await Task.Delay(500); // Esperar un poco antes de reintentar
                }
                catch (HttpRequestException ex)
                {
                    if (++attempts == MaxAttempts)
                    {
                        _logger.LogError($"Failed to upload file {filePath} after {MaxAttempts} attempts. Exception: {ex.Message}");
                        break; // Salir del bucle después de varios intentos fallidos
                    }
                    await Task.Delay(500); // Esperar un poco antes de reintentar
                }
            }
        }

        private void EnsureLogsDirectoryExists()
        {
            if (!Directory.Exists(_logsDirectory))
            {
                Directory.CreateDirectory(_logsDirectory);
            }
        }

    }
}
