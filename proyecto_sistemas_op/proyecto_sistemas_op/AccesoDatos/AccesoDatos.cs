using proyecto_sistemas_op.Modelos;
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
        /// <returns>
        /// Una lista de diccionarios (<see cref="List{Dictionary{string, string}}"/>), donde cada diccionario contiene pares clave-valor que representan diferentes categorías.
        /// Las claves incluyen: "edad", "cantidad_hombres", "cantidad_mujeres", "primaria_incompleta", "primaria_completa", "secundaria_completa", 
        /// "secundaria_incompleta", "universitaria_completa", "universitaria_incompleta", y "sin_estudios".
        /// </returns>
        public List<Dictionary<string, string>> parser_data()
        {
            string[] data = Lector_archivos();
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
        /// <returns>
        /// Un array de cadenas (<see cref="string[]"/>) que contiene cada línea del archivo.
        /// Si el archivo no existe o ocurre un error, devuelve un array vacío.
        /// </returns>
        /// 

        public string FindProjectRoot(string startPath, string targetFolderName)
        {
            DirectoryInfo directory = new DirectoryInfo(startPath);
            while (directory != null)
            {
                if (directory.FullName.EndsWith(targetFolderName))
                {
                    return directory.FullName;
                }
                directory = directory.Parent;
            }
            return null;
        }
        public string[] Lector_archivos() {
            // Obtener el directorio base de la aplicación
            string baseDirectory = AppContext.BaseDirectory;

            // Buscar el directorio raíz `sistemas_operativos\proyecto_sistemas_op`
            string projectRoot = FindProjectRoot(baseDirectory, "sistemas_operativos\\proyecto_sistemas_op");

            if (projectRoot == null)
            {
                Console.WriteLine("El directorio raíz del proyecto no se encontró.");
                
            }

            // Construir la ruta completa al archivo en "datos_externos"
            string rutaRelativa = Path.Combine(projectRoot, "datos_externos", "Proyeccion_2025.txt");



            try
            {
                if (File.Exists(rutaRelativa)){
                    
                    string[] contenido = File.ReadAllLines(rutaRelativa);
                    return contenido;
                }
                else
                {
                    Console.WriteLine("El archivo " + rutaRelativa + " No existe");
                    return Array.Empty<string>();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un error inesperado: " + e.Message);
                return Array.Empty<string>();
            }
            
        }
       
        public List<DatosPoblacion> ParsearDatosAObjectos()
        {
            string[] data = Lector_archivos();
            List<DatosPoblacion> resultado = new List<DatosPoblacion>();

            foreach (var linea in data) 
            {
                var dato = new DatosPoblacion
                {
                    Edad = int.Parse(linea.Substring(0, 2)),
                    CantidadHombres = int.Parse(linea.Substring(2, 7).Trim()),
                    CantidadMujeres = int.Parse(linea.Substring(9, 7).Trim()),
                    PrimariaIncompleta = int.Parse(linea.Substring(16, 6).Trim()),
                    PrimariaCompleta = int.Parse(linea.Substring(22, 6).Trim()),
                    SecundariaCompleta = int.Parse(linea.Substring(28, 6).Trim()),
                    SecundariaIncompleta = int.Parse(linea.Substring(34, 6).Trim()),
                    UniversitariaCompleta = int.Parse(linea.Substring(40, 6).Trim()),
                    UniversitariaIncompleta = int.Parse(linea.Substring(46, 6).Trim()),
                    SinEstudios = int.Parse(linea.Substring(52, 6).Trim())
                };

                resultado.Add(dato);
            }
            return resultado;
        }

    }
}
