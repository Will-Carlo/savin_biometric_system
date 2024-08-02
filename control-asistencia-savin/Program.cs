using control_asistencia_savin.ApiService;
using control_asistencia_savin.Frm;
using control_asistencia_savin.Notifications;
using control_asistencia_savin.WindowsSystemValidations;
using Serilog;

namespace control_asistencia_savin
{
    internal static class Program
    {

        // validación para evitar que el programa se ejecute más de una vez
        private static Mutex mutex = null;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ConfigureSerilog();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.


            //Application.SetCompatibleTextRenderingDefault(true);
            //Application.EnableVisualStyles();

            // Inicializar y mostrar el formulario de carga
            //frmLoading loadingForm = new frmLoading();
            //Application.Run(loadingForm);
            InternetChecker networkChangeExample = new InternetChecker();
            //Console.ReadLine();


            const string appName = "ControlAsistenciaSavin";
            bool createNew;


            mutex = new Mutex(true, appName, out createNew);

            if (createNew)
            {
                //MessageBox.Show("La aplicación ya está en ejecución", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //RestoreExistingInstance();
                //return;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ApplicationConfiguration.Initialize();
                Application.Run(new Main());

            }
            else
            {
                RestoreExistingInstance();
            }

            //ApplicationConfiguration.Initialize();
            //Application.Run(new Main());
        }
        //private static void ConfigureSerilog()
        //{
        //    // Configuración de Serilog
        //    Log.Logger = new LoggerConfiguration()
        //        .MinimumLevel.Debug()
        //        .WriteTo.Console()
        //        .WriteTo.File("logs/nombre2_savin.txt", rollingInterval: RollingInterval.Day)
        //        .CreateLogger();
        //}

        private static void RestoreExistingInstance()
        {
            NativeMethods.PostMessage(
                (IntPtr)NativeMethods.HWND_BROADCAST,
                NativeMethods.WM_SHOWME,
                IntPtr.Zero,
                IntPtr.Zero);
        }

    }
}

