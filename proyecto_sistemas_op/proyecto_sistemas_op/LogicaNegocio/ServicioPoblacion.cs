using proyecto_sistemas_op.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_sistemas_op.LogicaNegocio
{
    public class ServicioPoblacion
    {
        public Dictionary<string, DatosEscolaridad> CalcularDistribucionPorEdad(List<DatosPoblacion> datos)
        {
            // Inicializar el diccionario para cada grupo etario
            var distribucionPorEdad = new Dictionary<string, DatosEscolaridad>
            {
                { "0-4", new DatosEscolaridad { GrupoEtario = "0-4", Cantidad = 0, NivelEducacion = "Distribución general" } },
                { "5-9", new DatosEscolaridad { GrupoEtario = "5-9", Cantidad = 0, NivelEducacion = "Distribución general" } },
                { "10-14", new DatosEscolaridad { GrupoEtario = "10-14", Cantidad = 0, NivelEducacion = "Distribución general" } },
                { "15-19", new DatosEscolaridad { GrupoEtario = "15-19", Cantidad = 0, NivelEducacion = "Distribución general" } },
                { "20-49", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Distribución general" } },
                { "50-59", new DatosEscolaridad { GrupoEtario = "50-59", Cantidad = 0, NivelEducacion = "Distribución general" } },
                { "60+", new DatosEscolaridad { GrupoEtario = "60+", Cantidad = 0, NivelEducacion = "Distribución general" } }
            };

            // Procesa cada dato de población y se asigna al grupo etario correspondiente
            foreach (var dato in datos)
            {
                string grupo;

                if (dato.Edad >= 0 && dato.Edad <= 4)
                    grupo = "0-4";
                else if (dato.Edad >= 5 && dato.Edad <= 9)
                    grupo = "5-9";
                else if (dato.Edad >= 10 && dato.Edad <= 14)
                    grupo = "10-14";
                else if (dato.Edad >= 15 && dato.Edad <= 19)
                    grupo = "15-19";
                else if (dato.Edad >= 20 && dato.Edad <= 49)
                    grupo = "20-49";
                else if (dato.Edad >= 50 && dato.Edad <= 59)
                    grupo = "50-59";
                else
                    grupo = "60+";

                // Suma el total de población de hombres y mujeres para el grupo correspondiente
                distribucionPorEdad[grupo].Cantidad += dato.CantidadHombres + dato.CantidadMujeres;
            }

            return distribucionPorEdad;
        }
    }

}
