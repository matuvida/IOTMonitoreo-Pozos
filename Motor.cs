using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class Motor : Equipo
    {
        public const int PotenciaMin = 1;
        public const int PotenciaMax = 100000;

        public string TipoMotor { get; private set; }
        public int PotenciaHP { get; private set; }

        public Motor(int elNroEquipo, string laMarca
            , string elModelo, string elNroSerie
            , bool estaActivo) : base( elNroEquipo, "Motor", laMarca
            , elModelo, elNroSerie
            , estaActivo)
        { }

        public Motor(int elNroEquipo, string laMarca
            , string elModelo, string elNroSerie
            , bool estaActivo, string elTipoMotor
            , int laPotenciaHP) : this(elNroEquipo, laMarca
            , elModelo, elNroSerie
            , estaActivo)
        {
            TipoMotor = elTipoMotor;
            if( laPotenciaHP < PotenciaMin || laPotenciaHP > PotenciaMax)
            {
                throw new ArgumentException("Valor fuera de rango: laPotenciaHP");
                //PotenciaHP = 0;
            } else
            {
                PotenciaHP = laPotenciaHP;
            }
        }

        public override string Detallar()
        {
            return base.Detallar() + "\n  Tipo y Potencia Motor: " + TipoMotor
                +" de " + PotenciaHP + " HP";
        }
    }
}
