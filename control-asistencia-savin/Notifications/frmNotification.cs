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
                case "alert":
                    lblMessage.Text = message;
                    this.BackColor = Color.Red; // Establece el color de fondo en rojo
                    this.ForeColor = Color.White;
                    break;
                default:
                    lblMessage.Text = message;
                    this.BackColor = Color.Red; // Establece el color de fondo en rojo
                    this.ForeColor = Color.White;
                    break;
            }


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
    }
}
