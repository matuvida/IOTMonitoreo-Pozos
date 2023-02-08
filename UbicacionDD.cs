using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class UbicacionDD
    {
        public const double MinLatitud = -90;
        public const double MaxLatitud = 90;
        public const double MinLongitud = -180;
        public const double MaxLongitud = 180;

        private const double ValorNoVal = -9999;

        public double Latitud { get; private set; }
        public double Longitud { get; private set; }

        public UbicacionDD ()
        {
            Latitud = ValorNoVal;
            Longitud = ValorNoVal;
        }

        public UbicacionDD(double laLatitud, double laLongitud)
        {
            if(laLatitud<MinLatitud || laLatitud > MaxLatitud)
            {
                throw new ArgumentException("Valor fuera de rango: laLatitud");
            } else
            {
                if (laLongitud < MinLongitud || laLongitud > MaxLongitud)
                {
                    throw new ArgumentException("Valor fuera de rango: laLongitud");
                }
                else
                {
                    Latitud = laLatitud;
                    Longitud = laLongitud;
                }
            }
                
        }

        public string Resumir()
        {
            if (Latitud == ValorNoVal)
            {
                return "No informada";
            } else
            {
                return Latitud  +", " + Longitud;
            }
            
        }
    }
}
