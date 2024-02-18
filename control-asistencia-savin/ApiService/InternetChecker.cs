using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.NetworkInformation;
using System.Timers;

namespace control_asistencia_savin.ApiService
{

    public class InternetChecker
    {
        public InternetChecker()
        {
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
        }

        private void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                MessageBox.Show("La conexión a Internet se ha restablecido.");
                // Llama a tu procedimiento aquí
            }
        }
    }
}
