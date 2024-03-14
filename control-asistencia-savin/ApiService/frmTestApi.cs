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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace control_asistencia_savin
{
    public partial class frmTestApi : Form
    {
        private FunctionsDataBase _functionsDataBase;
        private int _IdTienda;
        private string _NombreTienda;
        private Dictionary<string, int> nombresNumeros = new Dictionary<string, int>
            {
                { "ALMACÉN CENTRAL", 1 },
                { "GOITIA", 2 },
                { "OFICINA LOAYZA", 3 },
                { "TIENDA LOAYZA", 4 },
                { "TIENDA SATÉLITE", 5 },
                { "TIENDA CEIBO", 6 },
                { "TIENDA COCHA", 7 },
                { "SANTA CRUZ", 8 },
                { "TARIJA", 9 },
                { "ORURO", 10 },
                { "POTOSÍ", 11 },
                { "SUCRE", 12 },
                { "TIENDA OBRAJES", 13 }
            };
        public frmTestApi()
        {
            InitializeComponent();
            _functionsDataBase = new FunctionsDataBase();


            cbxStore.DropDownStyle = ComboBoxStyle.DropDownList;

            foreach (string nombre in nombresNumeros.Keys)
            {
                cbxStore.Items.Add(nombre);
            }
            cbxStore.SelectedIndex = 0;

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
            _functionsDataBase.BackUpDB(this.txtDateBackup.Text);
        }
        private void btnDeleteBackups_Click(object sender, EventArgs e)
        {
            _functionsDataBase.DeleteBackupFiles(10);
        }

        private void btnCargarRegistros_Click(object sender, EventArgs e)
        {
            string messageDB = _functionsDataBase.LoadRegisters(_IdTienda, "Has seleccionado: " + this._NombreTienda);
            //MessageBox.Show("Has seleccionado: " + this._NombreTienda);
            this.lblPuntoAsistencia.Text = messageDB;
            //_functionsDataBase.LimpiarDB();
            //_functionsDataBase.loadDataBase();
        }

        private void cbxStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreSeleccionado = cbxStore.SelectedItem.ToString();
            this._NombreTienda = nombreSeleccionado;

            // Verificar si el nombre seleccionado está en el diccionario
            if (nombresNumeros.ContainsKey(nombreSeleccionado))
            {
                this._IdTienda = nombresNumeros[nombreSeleccionado];
            }
        }
    }
}
