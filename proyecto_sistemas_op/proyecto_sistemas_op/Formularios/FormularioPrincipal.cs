using proyecto_sistemas_op.Data;
using proyecto_sistemas_op.Modelos;
using proyecto_sistemas_op.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proyecto_sistemas_op.Formularios
{
    public partial class FormularioPrincipal : Form
    {
        private string[] fileData;
        private DataTable ageSexTable, groupTable, educationTable;
        private ServicioPoblacion servicioPoblacion;
        private ServicioAgrupacion servicioAgrupacion;
        private Dictionary<string, DatosEscolaridad> distribucionEdadActual;
        private Dictionary<string, DatosEscolaridad> distribucionEducacionActual;
        private Thread hiloCargarDatos;
        private Thread hiloProcesarEdad;
        private Thread hiloProcesarEducacion;
        private List<DatosPoblacion> datosPoblacion;

        public FormularioPrincipal()
        {
            InitializeComponent();
            servicioPoblacion = new ServicioPoblacion();
            servicioAgrupacion = new ServicioAgrupacion();
            CheckForIllegalCrossThreadCalls = false; // Permite actualizar UI desde otros hilos
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                btnCargarArchivo.Enabled = false;
                MostrarProgreso("Cargando datos...");

                // Crear y iniciar el hilo principal de carga
                hiloCargarDatos = new Thread(CargarDatos);
                hiloCargarDatos.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCargarArchivo.Enabled = true;
            }
        }

        private void CargarDatos()
        {
            try
            {
                var accesoDatos = new AccesoDatos();
                datosPoblacion = accesoDatos.ParsearDatosAObjectos();

                // Crear tabla de edad y sexo
                ageSexTable = new DataTable();
                ageSexTable.Columns.Add("Edad");
                ageSexTable.Columns.Add("Hombres");
                ageSexTable.Columns.Add("Mujeres");

                foreach (var dato in datosPoblacion)
                {
                    ageSexTable.Rows.Add(dato.Edad, dato.CantidadHombres, dato.CantidadMujeres);
                }

                // Actualizar la primera tabla
                dt_lista_edades_sexo.DataSource = ageSexTable;

                // Iniciar hilos para procesar distribuciones
                hiloProcesarEdad = new Thread(ProcesarDistribucionEdad);
                hiloProcesarEducacion = new Thread(ProcesarDistribucionEducacion);

                hiloProcesarEdad.Start();
                hiloProcesarEducacion.Start();

                // Esperar a que ambos hilos terminen
                hiloProcesarEdad.Join();
                hiloProcesarEducacion.Join();

                // Actualizar UI cuando todo esté listo
                OcultarProgreso();
                ActualizarTablaDistribucion();
                ActualizarTablaEducacion();
                btnCargarArchivo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el procesamiento: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCargarArchivo.Enabled = true;
                OcultarProgreso();
            }
        }

        private void ProcesarDistribucionEdad()
        {
            distribucionEdadActual = servicioPoblacion.CalcularDistribucionPorEdad(datosPoblacion);
        }

        private void ProcesarDistribucionEducacion()
        {
            distribucionEducacionActual = servicioAgrupacion.CalcularDistribucionPorEducacion(datosPoblacion);
        }

        private void MostrarProgreso(string mensaje)
        {
            lblProgreso.Text = mensaje;
            lblProgreso.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void OcultarProgreso()
        {
            lblProgreso.Visible = false;
            progressBar1.Visible = false;
        }

        private void ActualizarTablaDistribucion()
        {
            if (distribucionEdadActual == null)
                return;

            var distribucionTable = new DataTable();
            distribucionTable.Columns.Add("Grupo Etario");
            distribucionTable.Columns.Add("Cantidad");
            distribucionTable.Columns.Add("Nivel Educativo");

            foreach (var grupo in distribucionEdadActual)
            {
                distribucionTable.Rows.Add(
                    grupo.Value.GrupoEtario,
                    grupo.Value.Cantidad,
                    grupo.Value.NivelEducacion
                );
            }

            dataGridView2.DataSource = distribucionTable;
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ActualizarTablaEducacion()
        {
            if (distribucionEducacionActual == null)
                return;

            var educacionTable = new DataTable();
            educacionTable.Columns.Add("Grupo Etario");
            educacionTable.Columns.Add("Nivel Educativo");
            educacionTable.Columns.Add("Cantidad");

            foreach (var grupo in distribucionEducacionActual)
            {
                educacionTable.Rows.Add(
                    grupo.Value.GrupoEtario,
                    grupo.Value.NivelEducacion,
                    grupo.Value.Cantidad
                );
            }

            dataGridView3.DataSource = educacionTable;
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dataGridView3.Sort(dataGridView3.Columns["Grupo Etario"], ListSortDirection.Ascending);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Asegurarse de que los hilos se detengan al cerrar el formulario
            if (hiloCargarDatos != null && hiloCargarDatos.IsAlive)
                hiloCargarDatos.Abort();

            if (hiloProcesarEdad != null && hiloProcesarEdad.IsAlive)
                hiloProcesarEdad.Abort();

            if (hiloProcesarEducacion != null && hiloProcesarEducacion.IsAlive)
                hiloProcesarEducacion.Abort();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e)
        {
        }
    }
}
/*
using proyecto_sistemas_op.Data;
using proyecto_sistemas_op.Modelos;
using proyecto_sistemas_op.LogicaNegocio;
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
        private ServicioPoblacion servicioPoblacion;
        private ServicioAgrupacion servicioAgrupacion;
        private Dictionary<string, DatosEscolaridad> distribucionEdadActual;
        private Dictionary<string, DatosEscolaridad> distribucionEducacionActual;

        public FormularioPrincipal()
        {
            InitializeComponent();
            servicioPoblacion = new ServicioPoblacion();
            servicioAgrupacion = new ServicioAgrupacion();
        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                var accesoDatos = new AccesoDatos();
                var datosPoblacion = accesoDatos.ParsearDatosAObjectos();

                // Tabla para datos por edad y sexo
                ageSexTable = new DataTable();
                ageSexTable.Columns.Add("Edad");
                ageSexTable.Columns.Add("Hombres");
                ageSexTable.Columns.Add("Mujeres");
                foreach (var dato in datosPoblacion)
                {
                    ageSexTable.Rows.Add(dato.Edad, dato.CantidadHombres, dato.CantidadMujeres);
                }
                dt_lista_edades_sexo.DataSource = ageSexTable;

                // Calcular las distribuciones
                distribucionEdadActual = servicioPoblacion.CalcularDistribucionPorEdad(datosPoblacion);
                distribucionEducacionActual = servicioAgrupacion.CalcularDistribucionPorEducacion(datosPoblacion);

                // Actualizar ambas tablas
                ActualizarTablaDistribucion();
                ActualizarTablaEducacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarTablaDistribucion()
        {
            if (distribucionEdadActual == null)
                return;

            var distribucionTable = new DataTable();
            distribucionTable.Columns.Add("Grupo Etario");
            distribucionTable.Columns.Add("Cantidad");
            distribucionTable.Columns.Add("Nivel Educativo");

            foreach (var grupo in distribucionEdadActual)
            {
                distribucionTable.Rows.Add(
                    grupo.Value.GrupoEtario,
                    grupo.Value.Cantidad,
                    grupo.Value.NivelEducacion
                );
            }

            dataGridView2.DataSource = distribucionTable;

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ActualizarTablaEducacion()
        {
            if (distribucionEducacionActual == null)
                return;

            var educacionTable = new DataTable();
            educacionTable.Columns.Add("Grupo Etario");
            educacionTable.Columns.Add("Nivel Educativo");
            educacionTable.Columns.Add("Cantidad");

            foreach (var grupo in distribucionEducacionActual)
            {
                educacionTable.Rows.Add(
                    grupo.Value.GrupoEtario,
                    grupo.Value.NivelEducacion,
                    grupo.Value.Cantidad
                );
            }

            // Asignar al DataGridView3
            dataGridView3.DataSource = educacionTable;

            // Dar formato a las columnas
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // Opcional: Ordenar por Grupo Etario y Nivel Educativo
            dataGridView3.Sort(dataGridView3.Columns["Grupo Etario"], ListSortDirection.Ascending);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormularioPrincipal_Load(object sender, EventArgs e)
        {
        }
    }
}*/