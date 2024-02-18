using control_asistencia_savin.ApiService;
using control_asistencia_savin.Notifications;

namespace control_asistencia_savin
{
    internal static class Program
    {

       
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.


            //Application.SetCompatibleTextRenderingDefault(true);
            //Application.EnableVisualStyles();

            // Inicializar y mostrar el formulario de carga
            //frmLoading loadingForm = new frmLoading();
            //Application.Run(loadingForm);
            InternetChecker networkChangeExample = new InternetChecker();
            Console.ReadLine();


            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }

       
    }
}

