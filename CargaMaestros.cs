using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class CargaMaestros
    {
        private enum ValsUnidadBombeo
        {
            Tipo,Numero,Marca,Modelo,NroSerie,Activo,CargaMaxLbs
        }

        private enum ValsMotor
        {
            Tipo, Numero, Marca, Modelo, NroSerie, Activo, TipoMotor, PotenciaHP
        }

        private enum ValsSetUbicacionDeclarada
        {
            Tipo, Numero,Latitud,Longitud
        }

        private enum ValsSensorCarga
        {
            Tipo, Numero, Marca,Modelo,NroSerie,UnidadMedida,CargaMax
        }

        private enum ValsSensorGPSDD
        {
            Tipo, Numero, Marca, Modelo, NroSerie
        }

        private enum ValsAsignarSensor
        {
            Tipo, NroMonitoreable, NroSensor
        }

        private enum ValsInmueble
        {
            Tipo, Numero, Nombre,Direccion
        }

        public string CargarMaestros (CtrIOTMonitoreo capaCtrol, string nomArchivo, string separador)
        {
            string errorReg = "";
            string retorno = "";
            List<string[]> registros = new List<string[]>();
            int posicReg;
            int nroReg = 0;
            string tipoReg = "";

            string[] nombresTipoReg;
            string errorCantVals;
            int nroBien;
            int nroSensor;
            //propiedades particulares no string
            double latitud;
            double longitud;
            bool activo;
            int cargaMax;
            int potenciaHP;

            string errorValor;
            string errorCtrl;


            ArchivosTexto.LeerCSV(nomArchivo, separador, registros);

            foreach (string[] reg in registros)
            {
                nroReg++;
                errorReg = "Registro " + nroReg + " ";
                posicReg = registros.Count;
                if (posicReg<1)
                {
                    errorReg = errorReg + "Registro sin datos.";
                } else
                {
                    tipoReg = reg[0];
                    errorReg = errorReg + "Tipo Registro: " + tipoReg  +". ";
                    try
                    {
                        switch (tipoReg)
                        {
                            case "Motor":
                                nombresTipoReg = Enum.GetNames(typeof(ValsMotor));
                                errorCantVals = ValidarCantVal(reg, nombresTipoReg);
                                if (errorCantVals != "")
                                {
                                    errorReg = errorReg + errorCantVals;
                                }
                                else
                                {
                                    //conversiones específicas

                                    errorValor = ExtraerConv("Numero", nombresTipoReg, reg, out nroBien);
                                    if (errorValor != "")
                                    {
                                        errorReg = errorReg + errorValor;
                                    }
                                    else
                                    {
                                        errorValor = ExtraerConv("PotenciaHP", nombresTipoReg, reg, out potenciaHP);
                                        if (errorValor != "")
                                        {
                                            errorReg = errorReg + errorValor;
                                        }
                                        else
                                        {
                                            errorValor = ExtraerConv("Activo", nombresTipoReg, reg, out activo);
                                            if (errorValor != "")
                                            {
                                                errorReg = errorReg + errorValor;
                                            }
                                            else
                                            {
                                                errorCtrl = capaCtrol.CrearEquipo(
                                                    new Motor(
                                                        nroBien
                                                        , reg[(int)ValsMotor.Marca]
                                                        , reg[(int)ValsMotor.Modelo]
                                                        , reg[(int)ValsMotor.NroSerie]
                                                        , activo
                                                        , reg[(int)ValsMotor.TipoMotor]
                                                        , potenciaHP)
                                                    );
                                                if (errorCtrl == "")
                                                {
                                                    errorReg = "";
                                                }
                                                else
                                                {
                                                    errorReg = errorReg + " Falló la operación: " + errorCtrl;
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            case "UnidadBombeo":
                                nombresTipoReg = Enum.GetNames(typeof(ValsUnidadBombeo));
                                errorCantVals = ValidarCantVal(reg, nombresTipoReg);
                                if (errorCantVals !="")
                                {
                                    errorReg = errorReg + errorCantVals;
                                } else {
                                    //conversiones específicas

                                    errorValor = ExtraerConv("Numero", nombresTipoReg, reg, out nroBien);
                                    if (errorValor != "")
                                    {
                                        errorReg = errorReg + errorValor;
                                    } else
                                    {
                                        errorValor = ExtraerConv("CargaMaxLbs", nombresTipoReg, reg, out cargaMax);
                                        if (errorValor != "")
                                        {
                                            errorReg = errorReg + errorValor;
                                        }
                                        else
                                        {
                                            errorValor = ExtraerConv("Activo", nombresTipoReg, reg, out activo);
                                            if (errorValor != "")
                                            {
                                                errorReg = errorReg + errorValor;
                                            }
                                            else
                                            {
                                                errorCtrl = capaCtrol.CrearEquipo(
                                                    new UnidadBombeo(
                                                        nroBien
                                                        , reg[(int) ValsUnidadBombeo.Marca]
                                                        , reg[(int) ValsUnidadBombeo.Modelo]
                                                        , reg[(int) ValsUnidadBombeo.NroSerie]
                                                        , activo
                                                        , cargaMax)
                                                    );
                                                if (errorCtrl == "")
                                                {
                                                    errorReg = "";
                                                } else
                                                {
                                                    errorReg = errorReg + " Falló la operación: " + errorCtrl;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case "SetUbicacionDeclarada":
                                nombresTipoReg = Enum.GetNames(typeof(ValsSetUbicacionDeclarada));
                                errorCantVals = ValidarCantVal(reg, nombresTipoReg);
                                if (errorCantVals != "")
                                {
                                    errorReg = errorReg + errorCantVals;
                                }
                                else
                                {
                                    //conversiones específicas

                                    errorValor = ExtraerConv("Numero", nombresTipoReg, reg, out nroBien);
                                    if (errorValor != "")
                                    {
                                        errorReg = errorReg + errorValor;
                                    }
                                    else
                                    {
                                        errorValor = ExtraerConv("Latitud", nombresTipoReg, reg, out latitud);
                                        if (errorValor != "")
                                        {
                                            errorReg = errorReg + errorValor;
                                        }
                                        else
                                        {
                                            errorValor = ExtraerConv("Longitud", nombresTipoReg, reg, out longitud);
                                            if (errorValor != "")
                                            {
                                                errorReg = errorReg + errorValor;
                                            }
                                            else
                                            {
                                                errorCtrl = capaCtrol.SetUbicacionDeclarada(
                                                        nroBien
                                                        , latitud
                                                        , longitud
                                                    );
                                                if (errorCtrl == "")
                                                {
                                                    errorReg = "";
                                                }
                                                else
                                                {
                                                    errorReg = errorReg + " Falló la operación: " + errorCtrl;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case "SensorGPSDD":
                                nombresTipoReg = Enum.GetNames(typeof(ValsSensorGPSDD));
                                errorCantVals = ValidarCantVal(reg, nombresTipoReg);
                                if (errorCantVals != "")
                                {
                                    errorReg = errorReg + errorCantVals;
                                }
                                else
                                {
                                    //conversiones específicas
                                    errorValor = ExtraerConv("Numero", nombresTipoReg, reg, out nroSensor);
                                    if (errorValor != "")
                                    {
                                        errorReg = errorReg + errorValor;
                                    }
                                    else
                                    {
                                        errorCtrl = capaCtrol.CrearSensor(
                                            new SensorGPSDD(
                                                nroSensor
                                                , reg[(int)ValsSensorGPSDD.Marca]
                                                , reg[(int)ValsSensorGPSDD.Modelo]
                                                , reg[(int)ValsSensorGPSDD.NroSerie]
                                            ));
                                        if (errorCtrl == "")
                                        {
                                            errorReg = "";
                                        }
                                        else
                                        {
                                            errorReg = errorReg + " Falló la operación: " + errorCtrl;
                                        }
                                    }
                                }
                                break;
                            case "SensorCarga":
                                nombresTipoReg = Enum.GetNames(typeof(ValsSensorCarga));
                                errorCantVals = ValidarCantVal(reg, nombresTipoReg);
                                if (errorCantVals != "")
                                {
                                    errorReg = errorReg + errorCantVals;
                                }
                                else
                                {
                                    //conversiones específicas
                                    errorValor = ExtraerConv("Numero", nombresTipoReg, reg, out nroSensor);
                                    if (errorValor != "")
                                    {
                                        errorReg = errorReg + errorValor;
                                    }
                                    else
                                    {
                                        errorValor = ExtraerConv("CargaMax", nombresTipoReg, reg, out cargaMax);
                                        if (errorValor != "")
                                        {
                                            errorReg = errorReg + errorValor;
                                        }
                                        else
                                        {
                                            errorCtrl = capaCtrol.CrearSensor(
                                                new SensorCarga(
                                                    nroSensor
                                                    , reg[(int)ValsSensorCarga.Marca]
                                                    , reg[(int)ValsSensorCarga.Modelo]
                                                    , reg[(int)ValsSensorCarga.NroSerie]
                                                    , reg[(int)ValsSensorCarga.UnidadMedida]
                                                    , cargaMax)
                                                );
                                            if (errorCtrl == "")
                                            {
                                                errorReg = "";
                                            }
                                            else
                                            {
                                                errorReg = errorReg + " Falló la operación: " + errorCtrl;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "AsignarSensor":
                                nombresTipoReg = Enum.GetNames(typeof(ValsAsignarSensor));
                                errorCantVals = ValidarCantVal(reg, nombresTipoReg);
                                if (errorCantVals != "")
                                {
                                    errorReg = errorReg + errorCantVals;
                                }
                                else
                                {
                                    //conversiones específicas
                                    errorValor = ExtraerConv("NroMonitoreable", nombresTipoReg, reg, out nroBien);
                                    if (errorValor != "")
                                    {
                                        errorReg = errorReg + errorValor;
                                    }
                                    else
                                    {
                                        errorValor = ExtraerConv("NroSensor", nombresTipoReg, reg, out nroSensor);
                                        if (errorValor != "")
                                        {
                                            errorReg = errorReg + errorValor;
                                        }
                                        else
                                        {
                                            errorCtrl = capaCtrol.AsignarSensor(
                                                    nroBien
                                                    , nroSensor
                                                );
                                            if (errorCtrl == "")
                                            {
                                                errorReg = "";
                                            }
                                            else
                                            {
                                                errorReg = errorReg + " Falló la operación: " + errorCtrl;
                                            }
                                        }
                                    }
                                }
                                break;
                            case "Inmueble":
                                nombresTipoReg = Enum.GetNames(typeof(ValsInmueble));
                                errorCantVals = ValidarCantVal(reg, nombresTipoReg);
                                if (errorCantVals != "")
                                {
                                    errorReg = errorReg + errorCantVals;
                                }
                                else
                                {
                                    //conversiones específicas
                                    errorValor = ExtraerConv("Numero", nombresTipoReg, reg, out nroBien);
                                    if (errorValor != "")
                                    {
                                        errorReg = errorReg + errorValor;
                                    }
                                    else
                                    {
                                        errorCtrl = capaCtrol.CrearInmueble(
                                            new Inmueble(
                                                nroBien
                                                , reg[(int)ValsInmueble.Nombre]
                                                , reg[(int)ValsInmueble.Direccion]
                                            ));
                                        if (errorCtrl == "")
                                        {
                                            errorReg = "";
                                        }
                                        else
                                        {
                                            errorReg = errorReg + " Falló la operación: " + errorCtrl;
                                        }
                                    }
                                }
                                break;
                            default:
                                errorReg = errorReg + "Tipo de registro inválido.";
                                break;
                        }
                    }
                    catch (ArgumentException aex)
                    {
                        errorReg = errorReg + "Argumento inválido: " + aex.Message + "."; 
                    }
                    catch (Exception aex)
                    {
                        errorReg = errorReg + ": Error " + aex.Message;
                    }
                }
                if (errorReg!="")
                {
                    retorno = retorno + errorReg + "\n";
                }
            }

            return retorno;
        }

        private string ValidarCantVal (string[] registro, string[] nombresEnum)
        {
            if (registro.GetLength(0) != nombresEnum.GetLength(0))
            {
                return "Cantidad de valores incorrecto para el tipo de registro";
            } else
            {
                return "";
            }
        } 

        private string ExtraerConv(string nombreReq, string[] nombresEnum, string[] registro, out int valor)
        {
            int pos = BuscarNom(nombreReq, nombresEnum);

            if (pos == -1)
            {
                valor = 0;
                return "No se encuentra el nombre";
            } else
            {
                if (!int.TryParse(registro[pos],out valor))
                {
                    valor = 0;
                    return "Error no entero en el valor " + nombreReq
                        + " de la posición " + pos + ": " + registro[pos];
                } else
                {
                    return "";
                }
            }
        }

        private string ExtraerConv(string nombreReq, string[] nombresEnum, string[] registro, out double valor)
        {
            int pos = BuscarNom(nombreReq, nombresEnum);

            if (pos == -1)
            {
                valor = 0;
                return "No se encuentra el nombre";
            }
            else
            {
                if (!double.TryParse(registro[pos], out valor))
                {
                    valor = 0;
                    return "Error no numérico en el valor " + nombreReq
                        + " de la posición " + pos + ": " + registro[pos];
                }
                else
                {
                    return "";
                }
            }
        }

        private string ExtraerConv(string nombreReq, string[] nombresEnum, string[] registro, out bool valor)
        {
            int pos = BuscarNom(nombreReq, nombresEnum);

            if (pos == -1)
            {
                valor = false;
                return "No se encuentra el nombre";
            }
            else
            {
                if (!bool.TryParse(registro[pos], out valor))
                {
                    valor = false;
                    return "Error no booleano en el valor " + nombreReq
                        + " de la posición " + pos + ": " + registro[pos];
                }
                else
                {
                    return "";
                }
            }
        }

        private int BuscarNom(string nombreReq, string[]nombres)
        {
            int pos = 0;
            while (nombres[pos] != nombreReq && pos < nombres.GetUpperBound(0))
            {
                pos++;
            }

            if (nombres[pos] != nombreReq)
            {
                return -1;
            } else
            {
                return pos;
            }
        }
    }
}
