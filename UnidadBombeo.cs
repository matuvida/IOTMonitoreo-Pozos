using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class UnidadBombeo : Equipo
    {
        public const int CargaMxMin = 1;
        public const int CargaMxMax = 200000;

        public int CargaMaxLbs { get; private set; }

        public UnidadBombeo(int elNroEquipo, string laMarca
            , string elModelo, string elNroSerie
            , bool estaActivo) : base(elNroEquipo, "Unidad Bombeo", laMarca
            , elModelo, elNroSerie
            , estaActivo)
        { }

        public UnidadBombeo(int elNroEquipo, string laMarca
            , string elModelo, string elNroSerie
            , bool estaActivo
            , int laCargaMaxLbs) : this(elNroEquipo, laMarca
            , elModelo, elNroSerie
            , estaActivo)
        {

            if (laCargaMaxLbs < CargaMxMin || laCargaMaxLbs > CargaMxMax)
            {
                throw new ArgumentException("Valor fuera de rango: laCargaMaxLbs");
                //CargaMaxLbs = 0;
            }
            else
            {
                CargaMaxLbs = laCargaMaxLbs;
            }
        }

        public override string Detallar()
        {
            return base.Detallar() + "\n  Carga Máxima en Lbs: " + CargaMaxLbs;
        }

    }
}
