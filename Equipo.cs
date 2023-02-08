using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    abstract class Equipo:IBienListable, IMonitoreable
    {
        public const int NroEquipMin = CBienListable.NroMin;
        public const int NroEquipMax = CBienListable.NroMax;

        public int NroEquipo { get; private set; }
        public string Clase { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string NroSerie { get; private set; }
        public bool Activo { get; private set; }
        public UbicacionDD UbicDeclarada { get; private set; } = new UbicacionDD();
        public List<Sensor> Sensores { get; private set; } = new List<Sensor>();

        public Equipo (int elNumEquipo, string laClase, string laMarca
            , string elModelo, string elNroSerie, bool estaActivo) 
        {
            if (elNumEquipo < NroEquipMin || elNumEquipo > NroEquipMax)
            {
                throw new ArgumentException("Valor fuera de rango: elNumEquipo");
            }
            else
            {
                NroEquipo = elNumEquipo;
                Activo = estaActivo;
                Clase = laClase;
                Marca = laMarca;
                Modelo = elModelo;
                NroSerie = elNroSerie;
            }
        }

        public string SetUbicacionDeclarada (double laLatitud, double laLongitud)
        {
            UbicacionDD nvaUbic = new UbicacionDD(laLatitud, laLongitud);
            if (nvaUbic.Latitud != laLatitud)
            {
                return "No se modificó la ubicación. Revise las coordenadas.";
            } else
            {
                this.UbicDeclarada = nvaUbic;
                return "";
            }
        } 

        public virtual string Detallar()
        {
            string eqActivo = "No";
            if (Activo) { eqActivo = "Sí";}
            return "\nEquipo nro: " + NroEquipo + " Clase: " + Clase
                + "\n  Marca: " + Marca + " Modelo: " + Modelo
                + "\n  Nro. serie: " + NroSerie + " Activo: " + eqActivo
                + "\n  Ubicación Declarada: " + UbicDeclarada.Resumir();
        }

        public int GetNumero()
        {
            return NroEquipo;
        }

        public virtual string Resumir()
        {
            string eqActivo = "No";
            if (Activo) { eqActivo = "Sí"; }
            return "\nEquipo nro: " + NroEquipo + " Clase: " + Clase
                + "\n Activo: " + eqActivo +  " Ubicación Declarada: " + UbicDeclarada.Resumir();
        }

        public Sensor GetSensorInstXNro(int nro)
        {
            return Sensores.Find
                (
                    delegate (Sensor sen)
                    {
                        return sen.Numero == nro;
                    }
                );
        }

        public string AgregarSensor(Sensor nvoSensor)
        {
            if(nvoSensor.Numero == 0)
            {
                return "Sensor no válido";
            } else
            {
                if (GetSensorInstXNro(nvoSensor.Numero) != null)
                {
                    return "Sensor ya instalado en el equipo";
                } else
                {
                    Sensores.Add(nvoSensor); //agrega el sensor a la colección del equipo
                    return "";
                }
            }
        }

        public string ResumirMonitoreo()
        {
            string retorno = "";

            foreach (Sensor sen in Sensores)
            {
                retorno = retorno + sen.ResumirMedicion();
            }

            if (retorno == "")
            {
                return "\n  Sin monitoreo";
            } else
            {
                return retorno;
            }
        }

        public string ResumirConMonitoreo()
        {
            return this.Resumir() + this.ResumirMonitoreo();
        }
    }
}
