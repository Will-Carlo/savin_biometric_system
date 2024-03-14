using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.NetworkInformation;
using System.Timers;
using control_asistencia_savin.Frm;

namespace control_asistencia_savin.ApiService
{

    public class InternetChecker
    {


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
                reLoad();
            }
        }

        private void reLoad()
        {
            if (_functionsDataBase.verifyConection())
            {
                _m.registrarAsistenciasTemporales();
                _functionsDataBase.LimpiarDB();
                _functionsDataBase.loadDataBase();
                //MessageBox.Show("INTERNET CHECKER: se ejecutó la tarea con éxito.");
            }
            {
                //MessageBox.Show("Aún no se obtiene conexión exitosa del servidor");
            }

        }

    }
}
