using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class CargaMonitoreo
    {
        private CtrIOTMonitoreo miCtrIOT;
        private string prefijoArchMon;
        private string sufijoArchMon;
        private int nroArch = 1;

        public CargaMonitoreo(CtrIOTMonitoreo elCtrMonitoreo
            , string elPrefijoArchMon
            , string elSufijoArchMon)
        {
            miCtrIOT = elCtrMonitoreo;
            prefijoArchMon = elPrefijoArchMon;
            sufijoArchMon = elSufijoArchMon;
        }

        public string Ejecutar()
        {
            const string inicioError = "Error en línea ";
            const string SeparadorCampos = ";";
            const string SeparadorValores = ",";
            const byte MedicionCampos = 2;
            const byte MedColSensor = 0;
            const byte MedColValores = 1;

            List<string[]> registros = new List<string[]>();
            string nomArchivo;
            string[] campos;
            int numLin = 0;
            int nroSensor;
            string errores = "";
            string retActMed;

            nomArchivo = prefijoArchMon + nroArch + sufijoArchMon;

            if (!ArchivosTexto.ExisteArchivo(nomArchivo))
            {
                nroArch = 1;
                nomArchivo = prefijoArchMon + nroArch + sufijoArchMon;
                if (!ArchivosTexto.ExisteArchivo(nomArchivo))
                {
                    errores = "No existen archivos con prefijo " + prefijoArchMon
                        + " número y sufijo " + sufijoArchMon; 
                }
            } 

            if (errores == "")
            {
                ArchivosTexto.LeerCSV(nomArchivo, SeparadorCampos, registros);

                foreach (string[] reg in registros)
                {
                    numLin++;
                    if (reg.GetLength(0) != MedicionCampos)
                    {
                        errores = errores + "\n" + inicioError + numLin
                            + ": Cantidad de campos inválida (" + reg.GetLength(0) + "de"
                            + MedicionCampos;
                    }
                    else
                    {
                        if (!int.TryParse(reg[MedColSensor], out nroSensor))
                        {
                            errores = errores + "\n" + inicioError + numLin
                                + ": Número de sensor no numérico (" + reg[0] + ")";
                        }
                        else
                        {
                            campos = reg[MedColValores].Split(SeparadorValores.ToCharArray()[0]);
                            retActMed = miCtrIOT.ActualizarMedicion(nroSensor, campos);
                            if (retActMed != "")
                            {
                                errores = errores + "\n" + retActMed;
                            }
                        }
                    }
                }
            }
            nroArch++;
            return errores;
        }

    }
}
