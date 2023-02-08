using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IOTMonitoreoPozos
{
    static class ArchivosTexto
    {
        /// <summary>
        /// Método para leer archivos delimitados
        /// </summary>
        /// <param name="nomArchivo">ruta del archivo partiendo tres carpetas 
        /// superiores al archivo ejecutable. Por ejemplo, para una estructura de carpetas donde
        /// el ejecutable está en Solucion\Proyecto\bin\Debug\ejecutable.exe, si el parámetro es
        /// "archivo.csv", lo buscará en la carpeta "Solucion".
        /// </param>
        /// <param name="separador">caracter separador de campos, por ejemplo ";"</param>
        /// <param name="retorno">Una colección List donde cada elemento es un array de strings
        /// donde cada elemento de la colección List representa un renglón y 
        /// cada string que contiene es un campo de dicho renglón.
        /// </param>
        public static void LeerCSV(string nomArchivo, string separador, List<string[]> retorno)
        {
            //referencia: https://www.codeproject.com/Questions/1084390/Import-from-csv-in-csharp
            //referencia: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=netframework-4.8
            //la ruta comienza con ..\..\..\ para subir de la ruta del assembly (bin\debug)
            string ruta = @"..\..\..\";
            string path = ruta + nomArchivo;
            string[] campos;
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s;

                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    campos = s.Split(separador.ToCharArray()[0]);
                    retorno.Add(campos);
                }
            }
        }

        /// <summary>
        /// Metodo para escribir en el archivo.
        /// </summary>
        /// <param name="nomArchivo"></param>
        /// ruta del archivo partiendo tres carpetas 
        /// superiores al archivo ejecutable. Por ejemplo, para una estructura de carpetas donde
        /// el ejecutable está en Solucion\Proyecto\bin\Debug\ejecutable.exe, si el parámetro es
        /// "archivo.csv", lo buscará en la carpeta "Solucion".
        /// <param name="contenido">texto a escribir en el archivo, sobrescribiendo lo anterior</param>
        public static void Escribir(string nomArchivo, string contenido)
        {
            //referencia: https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=netframework-4.8
            //la ruta comienza con ..\..\..\ para subir de la ruta del assembly (bin\debug)
            string ruta = @"..\..\..\";
            string path = ruta + nomArchivo;
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(contenido);
            }
        }

        /// <summary>
        /// Método para agregar información al archivo
        /// </summary>
        /// <param name="nomArchivo"></param>
        /// ruta del archivo partiendo tres carpetas 
        /// superiores al archivo ejecutable. Por ejemplo, para una estructura de carpetas donde
        /// el ejecutable está en Solucion\Proyecto\bin\Debug\ejecutable.exe, si el parámetro es
        /// "archivo.csv", lo buscará en la carpeta "Solucion".
        /// <param name="contenido">texto a escribir en el archivo, agregando a lo anterior</param>
        public static void Agregar(string nomArchivo, string contenido)
        {
            //referencia: https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=netframework-4.8
            //la ruta comienza con ..\..\..\ para subir de la ruta del assembly (bin\debug)
            string ruta = @"..\..\..\";
            string path = ruta + nomArchivo;
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write(contenido);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomArchivo"></param>
        /// ruta del archivo partiendo tres carpetas 
        /// superiores al archivo ejecutable. Por ejemplo, para una estructura de carpetas donde
        /// el ejecutable está en Solucion\Proyecto\bin\Debug\ejecutable.exe, si el parámetro es
        /// "archivo.csv", lo buscará en la carpeta "Solucion".
        /// <returns>verdadero si existe el archivo, falso si no existe</returns>
        public static bool ExisteArchivo(string nomArchivo)
        {
            //referencia: https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=netframework-4.8
            //la ruta comienza con ..\..\..\ para subir de la ruta del assembly (bin\debug)
            string ruta = @"..\..\..\";
            string path = ruta + nomArchivo;

            return File.Exists(path);
        }
    }
}
