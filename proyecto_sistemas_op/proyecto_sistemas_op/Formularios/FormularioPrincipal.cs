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
using proyecto_sistemas_op.Modelos;
using proyecto_sistemas_op.LogicaNegocio;

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
            List<DatosPoblacion> datosPoblacions = new List<DatosPoblacion>();
            Dictionary<string, DatosEscolaridad> datosEscolaridad;


            datosPoblacions = lector.ParsearDatosAObjectos();

            ServicioAgrupacion servicioAgrupacion = new ServicioAgrupacion();
            /*datosEdadSexo = servicioAgrupacion.CalcularDistribucionPorEdadSexo(datosPoblacions);
            datosSexoGrupoEtario = servicioAgrupacion.CalcularDistribucionPorSexoGrupoEtario(datosPoblacions);*/
            datosEscolaridad = servicioAgrupacion.CalcularDistribucionPorEducacion(datosPoblacions);

            /*
            dataGridView1.Columns.Add("Edad", "Edad");
            dataGridView1.Columns.Add("Hombres", "Hombres");
            dataGridView1.Columns.Add("Mujeres", "Mujeres");


            foreach (var dt in datosEdadSexo)
            {
                dataGridView1.Rows.Add(dt.Value.GrupoEtario, dt.Value.NivelEducacion, dt.Value.Cantidad);
            }

            dataGridView2.Columns.Add("Edad", "Edad");
            dataGridView2.Columns.Add("Grado Escolaridad", "Grado Escolaridad");
            dataGridView2.Columns.Add("Total Poblacion", "Total Poblacion");


            foreach (var dt in datosSexoGrupoEtario)
            {
                dataGridView2.Rows.Add(dt.Value.GrupoEtario, dt.Value.NivelEducacion, dt.Value.Cantidad);
            }
            */
            dataGridView3.Columns.Add("Edad", "Edad");
            dataGridView3.Columns.Add("Grado Escolaridad", "Grado Escolaridad");
            dataGridView3.Columns.Add("Total Poblacion", "Total Poblacion");


            foreach (var dt in datosEscolaridad) 
            {
                dataGridView3.Rows.Add(dt.Value.GrupoEtario, dt.Value.NivelEducacion, dt.Value.Cantidad);
            }

        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
