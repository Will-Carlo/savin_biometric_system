using control_asistencia_savin.ApiService;
using control_asistencia_savin.Models;
using DPFP;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace control_asistencia_savin
{
    public partial class frmTestApi : Form
    {
        private FunctionsDataBase _functionsDataBase;

        public frmTestApi()
        {
            InitializeComponent();
            _functionsDataBase = new FunctionsDataBase();
        }

        private void btnVerificarApi_Click(object sender, EventArgs e)
        {
            _functionsDataBase.verifyConection();
        }
        private void btnCargarDB_Click(object sender, EventArgs e)
        {
            _functionsDataBase.loadDataBase();
        }
        private void btnDeleteBD_Click(object sender, EventArgs e)
        {
            _functionsDataBase.LimpiarDB();
        }
        private void btnMakeBackUp_Click(object sender, EventArgs e)
        {
            _functionsDataBase.BackUpDB("fechadata");
        }

    }
}
