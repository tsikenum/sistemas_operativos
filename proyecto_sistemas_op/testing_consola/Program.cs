using proyecto_sistemas_op.Data;
using proyecto_sistemas_op.LogicaNegocio;
using proyecto_sistemas_op.Modelos;



// See https://aka.ms/new-console-template for more information

AccesoDatos lector = new AccesoDatos();
List<Dictionary<string, string>> datos = new List<Dictionary<string, string>>();
List<DatosPoblacion> datosPoblacions = new List<DatosPoblacion>();
Dictionary<string, DatosEscolaridad> datosEscolaridad;


datosPoblacions = lector.ParsearDatosAObjectos();

ServicioAgrupacion servicioAgrupacion = new ServicioAgrupacion();
datosEscolaridad = servicioAgrupacion.CalcularDistribucionPorEducacion(datosPoblacions);

Console.WriteLine(datosPoblacions);

foreach (var dt in datosEscolaridad)
{
    Console.WriteLine();

}
