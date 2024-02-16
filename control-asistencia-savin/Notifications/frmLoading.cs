using CustomControls.RJControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin.Notifications
{
    public partial class frmLoading : Form
    {
        private const int MAX_PROGRESS = 100; // Máximo valor de la barra de progreso
        private int currentProgress = 0;
        public frmLoading()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            pgbSavinLoad.Maximum = MAX_PROGRESS;
            pgbSavinLoad.Value = 0;

            // Configuración del temporizador
            int totalIncrements = 100; // La barra de progreso va de 0 a 100
            int totalTimeInSeconds = 7; // Queremos que la animación dure 7 segundos

            // Calcula el intervalo del temporizador en milisegundos
            int interval = (totalTimeInSeconds * 1000) / totalIncrements;

            // Establece el intervalo del temporizador
            timer1.Interval = interval;


            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentProgress < MAX_PROGRESS)
            {
                currentProgress++;
                pgbSavinLoad.Value = currentProgress;
            }
            else
            {
                // Cuando la barra de progreso alcanza su máximo, cerramos el formulario de carga y mostramos el formulario principal
                timer1.Stop(); // Detenemos el temporizador
                Close(); // Cerramos el formulario de carga

            }
        }
    }
}
