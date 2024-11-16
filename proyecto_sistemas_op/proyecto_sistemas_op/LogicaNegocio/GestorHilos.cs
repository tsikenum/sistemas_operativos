using proyecto_sistemas_op.Data;
using proyecto_sistemas_op.LogicaNegocio;
using proyecto_sistemas_op.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace proyecto_sistemas_op.Gestor
{
    public class GestorHilos
    {
        private readonly AccesoDatos _accesoDatos;
        private readonly ServicioAgrupacion _servicioAgrupacion;
        private readonly ServicioPoblacion _servicioPoblacion;

        public GestorHilos()
        {
            _accesoDatos = new AccesoDatos();
            _servicioAgrupacion = new ServicioAgrupacion();
            _servicioPoblacion = new ServicioPoblacion();
        }

        public void Ejecutar()
        {
            // Utilizamos una tarea para leer y procesar datos
            Task<List<DatosPoblacion>> leerDatosTask = Task.Run(() =>
            {
                return _accesoDatos.ParsearDatosAObjectos();
            });

            leerDatosTask.ContinueWith((antecedent) =>
            {
                if (antecedent.IsFaulted)
                {
                    Console.WriteLine("Error al leer los datos: " + antecedent.Exception.InnerException.Message);
                    return;
                }

                List<DatosPoblacion> datosPoblacion = antecedent.Result;

                // Calcular distribución por educación
                var distribucionPorEducacion = _servicioAgrupacion.CalcularDistribucionPorEducacion(datosPoblacion);
                MostrarResultados("Distribución por educación", distribucionPorEducacion);

                // Calcular distribución por edad
                var distribucionPorEdad = _servicioPoblacion.CalcularDistribucionPorEdad(datosPoblacion);
                MostrarResultados("Distribución por edad", distribucionPorEdad);
            });
        }

        private void MostrarResultados(string titulo, Dictionary<string, DatosEscolaridad> resultados)
        {
            Console.WriteLine(titulo);
            foreach (var resultado in resultados)
            {
                Console.WriteLine($"{resultado.Key}: {resultado.Value.Cantidad}");
            }
            Console.WriteLine();
        }
    }
}