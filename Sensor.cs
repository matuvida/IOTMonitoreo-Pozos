using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    abstract class Sensor
    {
        public const int NroSensorMin = 100001;
        public const int NroSensorMax = 999999;

        public int Numero { get; private set; }
        public string Clase { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string NroSerie { get; private set; }
        public string UnidadMedida { get; private set; }

        public Sensor(int elNumero, string laClase, string laMarca
            , string elModelo, string elNroSerie, string laUnidadMedida)
        {
            if (elNumero < NroSensorMin || elNumero > NroSensorMax)
            {
                throw new ArgumentException("Valor fuera de rango: elNumero");
            } else
            {
                if (string.IsNullOrEmpty(laUnidadMedida))
                {
                    throw new ArgumentException("Argumento obligatorio: laUnidadMedida");
                }
                else
                {
                    Numero = elNumero;
                    Clase = laClase;
                    Marca = laMarca;
                    Modelo = elModelo;
                    NroSerie = elNroSerie;
                    UnidadMedida = laUnidadMedida;
                }

            }
        }

        public virtual string Detallar()
        {
            return "\nSensor nro: " + Numero + " Clase: " + Clase
                + "\n  Marca: " + Marca + " Modelo: " + Modelo
                + "\n  Nro. serie: " + NroSerie + " Unidad de Medida: "
                + UnidadMedida
                ;
        }


        public virtual string Resumir()
        {
            return "\n  Sensor nro: " + Numero + " Clase: " + Clase
                + " Unidad de Medida: " + UnidadMedida
                ;
        }

        public abstract string ResumirMedicion();

        public abstract string ActualizarMedicion(string [] valores);
    }
}
