using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using control_asistencia_savin.ApiService;

namespace control_asistencia_savin
{
    public partial class VerifyForm : Form
    {
        private ApiService.Credenciales _cd = new ApiService.Credenciales();
        public VerifyForm()
        {
            InitializeComponent();
            this.lblVersion.Text = _cd._versionApp;
        }

    }
}
