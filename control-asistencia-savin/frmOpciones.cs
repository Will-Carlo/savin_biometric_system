using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_asistencia_savin
{
    public partial class frmOpciones : Form
    {
        public frmOpciones()
        {
            InitializeComponent();
        }

        private void btnVerAtrasos_Click(object sender, EventArgs e)
        {
            frmAtrasos atrasos = new frmAtrasos();
            atrasos.Show();
        }
    }
}
