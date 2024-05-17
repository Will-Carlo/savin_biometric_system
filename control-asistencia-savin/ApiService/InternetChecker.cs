using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.NetworkInformation;
using System.Timers;
using control_asistencia_savin.Frm;
using control_asistencia_savin.Notifications;
using Microsoft.Extensions.Logging;

namespace control_asistencia_savin.ApiService
{

    public class InternetChecker
    {

        private readonly Microsoft.Extensions.Logging.ILogger _logger = LoggingManager.GetLogger<FunctionsDataBase>();
        private FunctionsDataBase _functionsDataBase = new FunctionsDataBase();
        private MetodosAsistencia _m = new MetodosAsistencia();
        public InternetChecker()
        {
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
        }

        private void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                _logger.LogInformation("\n-> TAREA PROGRAMADA: \nSe ha recuperado la conexión a internet.");
                reLoad();
            }
        }

        private void reLoad()
        {

            if (_functionsDataBase.verifyConection())
            {
                _m.registrarAsistenciasTemporales();

                //System.Threading.Thread.Sleep(10000);

                _functionsDataBase.LimpiarDB();
                _functionsDataBase.loadDataBase();
                //MessageBox.Show("INTERNET CHECKER: se ejecutó la tarea con éxito.");
            }
            else {
                _logger.LogDebug("Aún no se obtiene conexión a internet.");
            }

        }

    }
}
