using proyecto_sistemas_op.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_sistemas_op.LogicaNegocio
{
    public class ServicioAgrupacion
    {
        public Dictionary<string, DatosEscolaridad> CalcularDistribucionPorEducacion(List<DatosPoblacion> datos)
        {
            // Inicializar el diccionario para cada grupo etario y nivel de educacion
            var distribucionPorEducacion = new Dictionary<string, DatosEscolaridad>
            {
                { "0-4 - Sin Escolaridad", new DatosEscolaridad { GrupoEtario = "0-4", Cantidad = 0, NivelEducacion = "Sin Escolaridad" } },
                { "0-4 - Primaria Completa", new DatosEscolaridad { GrupoEtario = "0-4", Cantidad = 0, NivelEducacion = "Primaria Completa" } },
                { "0-4 - Secundaria Completa", new DatosEscolaridad { GrupoEtario = "0-4", Cantidad = 0, NivelEducacion = "Secundaria Completa" } },
                { "0-4 - Universidad Completa", new DatosEscolaridad { GrupoEtario = "0-4", Cantidad = 0, NivelEducacion = "Universidad Completa" } },
                { "5-9 - Sin Escolaridad", new DatosEscolaridad { GrupoEtario = "5-9", Cantidad = 0, NivelEducacion = "Sin Escolaridad" } },
                { "5-9 - Primaria Completa", new DatosEscolaridad { GrupoEtario = "5-9", Cantidad = 0, NivelEducacion = "Primaria Completa" } },
                { "5-9 - Secundaria Completa", new DatosEscolaridad { GrupoEtario = "5-9", Cantidad = 0, NivelEducacion = "Secundaria Completa" } },
                { "5-9 - Universidad Completa", new DatosEscolaridad { GrupoEtario = "5-9", Cantidad = 0, NivelEducacion = "Universidad Completa" } },
                { "10-14 - Sin Escolaridad", new DatosEscolaridad { GrupoEtario = "10-14", Cantidad = 0, NivelEducacion = "Sin Escolaridad" } },
                { "10-14 - Primaria Completa", new DatosEscolaridad { GrupoEtario = "10-14", Cantidad = 0, NivelEducacion = "Primaria Completa" } },
                { "10-14 - Secundaria Completa", new DatosEscolaridad { GrupoEtario = "10-14", Cantidad = 0, NivelEducacion = "Secundaria Completa" } },
                { "10-14 - Universidad Completa", new DatosEscolaridad { GrupoEtario = "10-14", Cantidad = 0, NivelEducacion = "Universidad Completa" } },
                { "15-19 - Sin Escolaridad", new DatosEscolaridad { GrupoEtario = "15-19", Cantidad = 0, NivelEducacion = "Sin Escolaridad" } },
                { "15-19 - Primaria Completa", new DatosEscolaridad { GrupoEtario = "15-19", Cantidad = 0, NivelEducacion = "Primaria Completa" } },
                { "15-19 - Secundaria Completa", new DatosEscolaridad { GrupoEtario = "15-19", Cantidad = 0, NivelEducacion = "Secundaria Completa" } },
                { "15-19 - Universidad Completa", new DatosEscolaridad { GrupoEtario = "15-19", Cantidad = 0, NivelEducacion = "Universidad Completa" } },
                { "20-49 - Sin Escolaridad", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Sin Escolaridad" } },
                { "20-49 - Primaria Completa", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Primaria Completa" } },
                { "20-49 - Secundaria Completa", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Secundaria Completa" } },
                { "20-49 - Universidad Completa", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Universidad Completa" } },
                { "50-59 - Sin Escolaridad", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Sin Escolaridad" } },
                { "50-59 - Primaria Completa", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Primaria Completa" } },
                { "50-59 - Secundaria Completa", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Secundaria Completa" } },
                { "50-59 - Universidad Completa", new DatosEscolaridad { GrupoEtario = "20-49", Cantidad = 0, NivelEducacion = "Universidad Completa" } },
                { "60+ - Sin Escolaridad", new DatosEscolaridad { GrupoEtario = "60+", Cantidad = 0, NivelEducacion = "Sin Escolaridad" } },
                { "60+ - Primaria Completa", new DatosEscolaridad { GrupoEtario = "60+", Cantidad = 0, NivelEducacion = "Primaria Completa" } },
                { "60+ - Secundaria Completa", new DatosEscolaridad { GrupoEtario = "60+", Cantidad = 0, NivelEducacion = "Secundaria Completa" } },
                { "60+ - Universidad Completa", new DatosEscolaridad { GrupoEtario = "60+", Cantidad = 0, NivelEducacion = "Universidad Completa" } },
            };

            // Procesa cada dato de población, se asigna al grupo etario y nivel de educación correspondiente
            foreach (var dato in datos)
            {
                string grupo;

                if (dato.Edad >= 0 && dato.Edad <= 4 && dato.PrimariaCompleta == 0 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "0-4 - Sin Escolaridad";
                else if (dato.Edad >= 0 && dato.Edad <= 4 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "0-4 - Primaria Completa";
                else if (dato.Edad >= 0 && dato.Edad <= 4 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 0)
                    grupo = "0-4 - Secundaria Completa";
                else if (dato.Edad >= 0 && dato.Edad <= 4 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 1)
                    grupo = "0-4 - Universidad Completa";
                else if (dato.Edad >= 5 && dato.Edad <= 9 && dato.PrimariaCompleta == 0 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "5-9 - Sin Escolaridad";
                else if (dato.Edad >= 5 && dato.Edad <= 9 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "5-9 - Primaria Completa";
                else if (dato.Edad >= 5 && dato.Edad <= 9 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 0)
                    grupo = "5-9 - Secundaria Completa";
                else if (dato.Edad >= 5 && dato.Edad <= 9 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 1)
                    grupo = "5-9 - Universidad Completa";
                else if (dato.Edad >= 10 && dato.Edad <= 14 && dato.PrimariaCompleta == 0 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "10-14 - Sin Escolaridad";
                else if (dato.Edad >= 10 && dato.Edad <= 14 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "10-14 - Primaria Completa";
                else if (dato.Edad >= 10 && dato.Edad <= 14 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 0)
                    grupo = "10-14 - Secundaria Completa";
                else if (dato.Edad >= 10 && dato.Edad <= 14 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 1)
                    grupo = "10-14 - Universidad Completa";
                else if (dato.Edad >= 15 && dato.Edad <= 19 && dato.PrimariaCompleta == 0 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "15-19 - Sin Escolaridad";
                else if (dato.Edad >= 15 && dato.Edad <= 19 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "15-19 - Primaria Completa";
                else if (dato.Edad >= 15 && dato.Edad <= 19 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 0)
                    grupo = "15-19 - Secundaria Completa";
                else if (dato.Edad >= 15 && dato.Edad <= 19 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 1)
                    grupo = "15-19 - Universidad Completa";
                else if (dato.Edad >= 20 && dato.Edad <= 49 && dato.PrimariaCompleta == 0 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "20-49 - Sin Escolaridad";
                else if (dato.Edad >= 20 && dato.Edad <= 49 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "20-49 - Primaria Completa";
                else if (dato.Edad >= 20 && dato.Edad <= 49 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 0)
                    grupo = "20-49 - Secundaria Completa";
                else if (dato.Edad >= 20 && dato.Edad <= 49 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 1)
                    grupo = "20-49 - Universidad Completa";
                else if (dato.Edad >= 50 && dato.Edad <= 59 && dato.PrimariaCompleta == 0 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "50-59 - Sin Escolaridad";
                else if (dato.Edad >= 50 && dato.Edad <= 59 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "50-59 - Primaria Completa";
                else if (dato.Edad >= 50 && dato.Edad <= 59 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 0)
                    grupo = "50-59 - Secundaria Completa";
                else if (dato.Edad >= 50 && dato.Edad <= 59 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 1)
                    grupo = "50-59 - Universidad Completa";
                else if (dato.Edad <= 60 && dato.PrimariaCompleta == 0 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "60+ - Sin Escolaridad";
                else if (dato.Edad <= 60 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 0 && dato.UniversitariaCompleta == 0)
                    grupo = "60+ - Primaria Completa";
                else if (dato.Edad <= 60 && dato.PrimariaCompleta == 1 && dato.SecundariaIncompleta == 1 && dato.UniversitariaCompleta == 0)
                    grupo = "60+ - Secundaria Completa";
                else
                    grupo = "60+ - Universidad Completa";

                // Suma el total de población de hombres y mujeres para el grupo correspondiente
                distribucionPorEducacion[grupo].Cantidad += dato.CantidadHombres + dato.CantidadMujeres;
            }

            return distribucionPorEducacion;}
    }
}
