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
    public partial class frmNotification : Form
    {
        public frmNotification(string message, string typeMessage)
        {
            InitializeComponent();
            InitializeFormProperties();



            switch (typeMessage)
            {
                case "welcome":
                    lblMessage.Text = message;
                    this.Text = "Mensaje de bienvenida"; // Cambiar el texto del título del formulario
                    this.BackColor = Color.Yellow;
                    this.ForeColor = Color.Blue;
                    this.Icon = SystemIcons.Information; // Icono de información
                    break;
                case "alert":
                    lblMessage.Text = message;
                    this.Text = "Alerta"; // Cambiar el texto del título del formulario
                    this.BackColor = Color.Red;
                    this.ForeColor = Color.White;
                    this.Icon = SystemIcons.Warning; // Icono de advertencia
                    break;
                default:
                    lblMessage.Text = message;
                    this.Text = "Error"; // Cambiar el texto del título del formulario
                    this.BackColor = Color.Red;
                    this.ForeColor = Color.White;
                    this.Icon = SystemIcons.Error; // Icono de error
                    break;
            }


            CentrarMensaje();
            btnOk.ForeColor = Color.Black;  
        }
        private void InitializeFormProperties()
        {
            // Configurar propiedades del formulario
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // No se puede cambiar el tamaño
            this.MaximizeBox = false; // Deshabilitar el botón de maximizar
            this.MinimizeBox = false; // Deshabilitar el botón de minimizar
            this.StartPosition = FormStartPosition.CenterScreen; // Centrar en la pantalla
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario cuando se hace clic en "Aceptar"
        }

        private void CentrarMensaje()
        {
            lblMessage.AutoSize = true;
            int x1 = (this.pnlNotification.Width - lblMessage.Width) / 2;
            int y1 = lblMessage.Location.Y;

            lblMessage.Location = new System.Drawing.Point(x1, y1);
        }
    }
}
