using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class SensorGPSDD : Sensor
    {
        public UbicacionDD Ubicacion { get; private set; } = new UbicacionDD();

        public SensorGPSDD(int elNumero, string laMarca
        , string elModelo, string elNroSerie)
        : base(elNumero, "GPS", laMarca
        , elModelo, elNroSerie, "DD")
        {
        }

        public override string ResumirMedicion()
        {
            return base.Resumir() + " Lectura: " + Ubicacion.Resumir();
        }

        public override string ActualizarMedicion(string[] valores)
        {
            double latitud = 0;
            double longitud = 0;

            UbicacionDD nvaUbic;

            if (valores.GetLength(0) != 2)
            {
                return "No se recibieron 2 valores";
            }
            else
            {
                if (!double.TryParse(valores[0], out latitud))
                {
                    return "Latitud no numérica recibida: " + valores[0];
                }
                else
                {
                    if (!double.TryParse(valores[1], out longitud))
                    {
                        return "Longitud no numérica recibida: " + valores[0];
                    }
                    else
                    {
                        nvaUbic = new UbicacionDD(latitud, longitud);
                        if (nvaUbic.Latitud != latitud)
                        {
                            return "No se modificó la ubicación. Revise las coordenadas.";
                        }
                        else
                        {
                            this.Ubicacion = nvaUbic;
                            return "";
                        }
                    }
                }
            }
        }
    }
}
