using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_sistemas_op.Data
{
    public class AccesoDatos
    {


        /// <summary>
        ///  Parsea o modifica la informacion que extrae el metodo Lector_Archivos.
        /// </summary>
        /// <param name="data">
        /// Un array de cadenas (<see cref="string[]"/>) donde cada elemento representa una línea de los datos extraidos.
        /// </param>
        /// <returns>
        /// Una lista de diccionarios (<see cref="List{Dictionary{string, string}}"/>), donde cada diccionario contiene pares clave-valor que representan diferentes categorías.
        /// Las claves incluyen: "edad", "cantidad_hombres", "cantidad_mujeres", "primaria_incompleta", "primaria_completa", "secundaria_completa", 
        /// "secundaria_incompleta", "universitaria_completa", "universitaria_incompleta", y "sin_estudios".
        /// </returns>
        public List<Dictionary<string, string>> parser_data(string[] data)
        {
 
            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();


            foreach (var linea in data)
            {
                Dictionary<string, string> temp_dic = new Dictionary<string, string>();
                temp_dic.Add("edad", linea.Substring(0, 2));
                temp_dic.Add("cantidad_hombres", linea.Substring(2,7));
                temp_dic.Add("cantidad_mujeres", linea.Substring(9,7));
                temp_dic.Add("primaria_incompleta", linea.Substring(16, 6));
                temp_dic.Add("primaria_completa", linea.Substring(22, 6));
                temp_dic.Add("secundaria_completa", linea.Substring(28, 6));
                temp_dic.Add("secundaria_incompleta", linea.Substring(34, 6));
                temp_dic.Add("universitaria_completa", linea.Substring(40, 6));
                temp_dic.Add("universitaria_incompleta", linea.Substring(46, 6));
                temp_dic.Add("sin_estudios", linea.Substring(52, 6));

                resultado.Add(temp_dic);
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que lee un archivo, desde una ruta especifica y guardar el resultado en un array de strings.
        /// </summary>
        /// <param name="ruta_archivo"></param>
        /// <returns>
        /// Un array de cadenas (<see cref="string[]"/>) que contiene cada línea del archivo.
        /// Si el archivo no existe o ocurre un error, devuelve un array vacío.
        /// </returns>
        public string[] Lector_archivos(string ruta_archivo) {
            

            try
            {
                if (File.Exists(ruta_archivo)){
                    
                    string[] contenido = File.ReadAllLines(ruta_archivo);
                    return contenido;
                }
                else
                {
                    Console.WriteLine("El archivo " + ruta_archivo + " No existe");
                    return Array.Empty<string>();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + e.Message);
                return Array.Empty<string>();
            }
            
        }
       


    }
}
