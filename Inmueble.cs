using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class Inmueble : IBienListable
    {
        public const int NroInmuebleMin = CBienListable.NroMin;
        public const int NroInmuebleMax = CBienListable.NroMax;

        public int Numero { get; private set; }
        public string Nombre { get; private set; }
        public string Direccion { get; private set; }
        
        public Inmueble (int elNumero, string elNombre, string laDireccion)
        {
            if (elNumero < NroInmuebleMin || elNumero > NroInmuebleMax)
            {
                throw new ArgumentException("Valor fuera de rango: elNumero");
            } else
            {
                if (string.IsNullOrEmpty(elNombre))
                {
                    throw new ArgumentException("Argumento obligatorio: elNombre");
                } else
                {
                    if (string.IsNullOrEmpty(laDireccion))
                    {
                        throw new ArgumentException("Argumento obligatorio: laDireccion");
                    } else
                    {
                        Numero = elNumero;
                        Nombre = elNombre;
                        Direccion = laDireccion;
                    }
                }
            }
        }

        public string Detallar()
        {
            return "\nNúmero: " + Numero + " Nombre: " + Nombre
                + "\n  Dirección: " + Direccion;
        }

        public int GetNumero()
        {
            return Numero;
        }
    }
}
