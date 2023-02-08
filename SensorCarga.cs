using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class SensorCarga : Sensor
    {
        public const int CargaMin = 0;
        public const int CargaMaxMax = 9999999;
        public int CargaActual { get; private set; }
        public int CargaMax { get; private set; }

        public SensorCarga(int elNumero, string laMarca
            , string elModelo, string elNroSerie, string laUnidadMedida
            , int laCargaMax)
            : base(elNumero, "CARGA", laMarca
            , elModelo, elNroSerie, laUnidadMedida)
        {
            CargaActual = 0;
            if (laCargaMax < CargaMin || laCargaMax > CargaMaxMax)
            {
                throw new ArgumentException("Valor fuera de rango: laCargaMax");
            }
            else
            {
                CargaMax = laCargaMax;
            }
        }

        public override string ResumirMedicion()
        {
            return base.Resumir() + " Lectura: " + CargaActual;
        }

        public override string ActualizarMedicion(string [] valores)
        {
            int valor = 0;

            if (valores.GetLength(0) == 0)
            {
                return "No se recibió ningún valor";
            }
            else
            {
                if (!int.TryParse(valores[0], out valor))
                {
                    return "Valor no numérico recibido: " + valores[0];
                }
                else
                {
                    if (valor < 0 || valor > CargaMaxMax)
                    {
                        return "Valor inválido recibido: " + valores[0];
                    }
                    else
                    {
                        CargaActual = valor;
                        return "";
                    }
                }
            }
        }
    }
}
