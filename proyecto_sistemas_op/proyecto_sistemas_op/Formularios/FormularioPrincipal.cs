using proyecto_sistemas_op.Data;
using proyecto_sistemas_op.Modelos;
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

namespace proyecto_sistemas_op.Formularios
{
    public partial class FormularioPrincipal : Form
    {
        string[] fileData;
        DataTable ageSexTable, groupTable, educationTable;

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            var accesoDatos = new AccesoDatos();
            var datosPoblacion = accesoDatos.ParsearDatosAObjectos();

            // Crear DataTable para el DataGridView principal
            ageSexTable = new DataTable();
            ageSexTable.Columns.Add("Edad");
            ageSexTable.Columns.Add("Hombres");
            ageSexTable.Columns.Add("Mujeres");

            foreach (var dato in datosPoblacion)
            {
                ageSexTable.Rows.Add(dato.Edad, dato.CantidadHombres, dato.CantidadMujeres);
            }

            dt_lista_edades_sexo.DataSource = ageSexTable; // Enlaza con un DataGridView en el formulario
        }

        private void ActualizarDataGrid(Dictionary<string, DatosEscolaridad> datos, DataGridView gridView)
        {
            // Crear un DataTable para los datos
            var table = new DataTable();
            table.Columns.Add("Grupo Etario");
            table.Columns.Add("Cantidad");
            table.Columns.Add("Nivel Educativo");

            // Llenar el DataTable con los datos del Dictionary
            foreach (var entry in datos)
            {
                table.Rows.Add(entry.Value.GrupoEtario, entry.Value.Cantidad, entry.Value.NivelEducacion);
            }

            // Enlazar el DataTable con el DataGridView
            gridView.DataSource = table;
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
