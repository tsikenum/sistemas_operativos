using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyecto_sistemas_op.Data;

namespace proyecto_sistemas_op.Formularios
{
    public partial class FormularioPrincipal : Form
    {
        string[] fileData;
        DataTable ageSexTable, groupTable, educationTable;

 

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            AccesoDatos lector = new AccesoDatos();
            List<Dictionary<string, string>> datos = new List<Dictionary<string, string>>();


            datos = lector.P();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public FormularioPrincipal()
        {
            InitializeComponent();
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
