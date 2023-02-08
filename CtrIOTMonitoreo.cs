using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class CtrIOTMonitoreo
    {
        private List<Equipo> equipos = new List<Equipo>();
        private List<Inmueble> inmuebles = new List<Inmueble>();
        private List<IBienListable> bienesListables = new List<IBienListable>();
        private List<Sensor> sensores = new List<Sensor>();

        private IBienListable GetBienXNro(int nro)
        {
            return bienesListables.Find
                (delegate (IBienListable bien)
                   {
                       return bien.GetNumero() == nro;
                   }
                );
        }

        public string CrearInmueble(Inmueble nvoInmueble)
        {
            if (nvoInmueble.GetNumero() == 0)
            {
                return "Inmueble no válido. Revise los datos";
            }
            else
            {
                if (GetBienXNro(nvoInmueble.GetNumero()) != null)
                {
                    return "Ya existe un bien con el número " + nvoInmueble.GetNumero();
                }
                else
                {
                    inmuebles.Add(nvoInmueble);
                    bienesListables.Add(nvoInmueble);
                    return "";
                }
            }
        }

        public string CrearEquipo(Equipo nvoEquipo)
        {
            if (nvoEquipo.GetNumero() == 0)
            {
                return "Equipo no válido. Revise los datos";
            } else
            {
                if (GetBienXNro(nvoEquipo.GetNumero())!= null)
                {
                    return "Ya existe un bien con el número " + nvoEquipo.GetNumero();
                } else
                {
                    equipos.Add(nvoEquipo);
                    bienesListables.Add(nvoEquipo);
                    return "";
                }
            }
        }

        public string SetUbicacionDeclarada(int nroEquipo, double latitud, double longitud)
        {
            IBienListable bien;
            Equipo equipo;
            string retorno = "";

            bien = GetBienXNro(nroEquipo);
            if (bien == null)
            {
                retorno = "No existen bienes con el número " + nroEquipo;
            }
            else
            {
                equipo = bien as Equipo;
                if (equipo == null)
                {
                    retorno = "El bien " + nroEquipo + " no permite coordenadas";
                }
                else
                {
                    retorno = equipo.SetUbicacionDeclarada(latitud, longitud);
                    if (retorno == "")
                    {
                        return "";
                    }
                }
            }
            return retorno;
        }

        public string ListaEquipos()
        {
            string lista = "";
            foreach (Equipo eq in equipos)
            {
                lista = lista + eq.Detallar();
            }
            return lista;
        }

        public string ListaBienes()
        {
            string lista = "";
            foreach (IBienListable bi in bienesListables)
            {
                lista = lista + bi.Detallar();
            }
            return lista;
        }

        private Sensor GetSensorXNro(int nro)
        {
            return sensores.Find
                (
                    delegate (Sensor sen)
                    {
                        return sen.Numero == nro;
                    }
                );
        }

        public string CrearSensor(Sensor nvoSensor)
        {
            if (nvoSensor.Numero == 0)
            {
                return "Sensor no válido.";
            }
            else
            {
                if (GetSensorXNro(nvoSensor.Numero) != null)
                {
                    return "Ya existe un sensor con el número " + nvoSensor.Numero;
                }
                else
                {
                    sensores.Add(nvoSensor);
                    return "";
                }
            }
        }

        private IMonitoreable UbicarSensor(int nroSensor)
        {
            IMonitoreable monitoreableAct;

            foreach (IBienListable bien in bienesListables)
            {
                monitoreableAct = bien as IMonitoreable;
                if (monitoreableAct != null)
                {
                    if (monitoreableAct.GetSensorInstXNro(nroSensor) != null)
                    {
                        return monitoreableAct;
                    }
                }
            }
            return null;
        }

        public string AsignarSensor(int nroMonitoreable, int nroSensor)
        {
            string retorno = "";
            IBienListable bien;
            IMonitoreable monitoreable;
            Sensor sensor;
            IMonitoreable monitoreablePrevio;

            sensor = GetSensorXNro(nroSensor);
            if (sensor == null)
            {
                retorno = "No existen sensores con el número " + nroSensor;
            }
            else
            {
                bien = GetBienXNro(nroMonitoreable);
                if (bien == null)
                {
                    retorno = "No existen bienes con el número " + nroMonitoreable;
                }
                else
                {
                    monitoreable = bien as IMonitoreable;
                    if (monitoreable == null)
                    {
                        retorno = "El bien " + nroMonitoreable + " no es monitoreable";
                    }
                    else
                    {
                        monitoreablePrevio = UbicarSensor(nroSensor);
                        if (monitoreablePrevio != null)
                        {
                            retorno = "El sensor " + nroSensor + " ya está instalado en el "
                                + "elemento monitoreable nro: "
                                + monitoreablePrevio.GetNumero();
                        }
                        else
                        {
                            retorno = monitoreable.AgregarSensor(sensor);
                            if (retorno != "")
                            {
                                retorno = "Error al asignar sensor: " + retorno;
                            }
                            else
                            {
                                retorno = "";
                            }
                        }
                    }
                }
            }
            return retorno;
        }

        public string ActualizarMedicion(int nroSensor, string [] valores)
        {
            Sensor sensor;
            string retorno = "";

            if (nroSensor < Sensor.NroSensorMin || nroSensor > Sensor.NroSensorMax)
            {
                retorno = "Número de sensor inválido " + nroSensor;
            } else
            {
                sensor = GetSensorXNro(nroSensor);
                if (sensor == null)
                {
                    retorno = "No existen sensores con el número: " + nroSensor;
                } else
                {
                    retorno = sensor.ActualizarMedicion(valores);
                }
            }

            return retorno;
        }

        public string MonitoreoBienes()
        {
            string lista = "";
            IMonitoreable monitoreable;
            foreach (IBienListable bi in bienesListables)
            {
                monitoreable = bi as IMonitoreable;
                if (monitoreable != null)
                {
                    lista = lista + monitoreable.ResumirConMonitoreo();
                }

            }
            return lista;
        }
    }
}
