using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    class AppIOTMonitoreo
    {
        private CtrIOTMonitoreo ctrIOT = new CtrIOTMonitoreo();

        public void Iniciar()
        {
            const string OpcLisEq = "E";
            const string OpcLisBi = "B";
            const string OpcMonit = "M";
            const string OpcSalir = "S";
            const string OpcMonNo = "N";

            string opcion = "";
            string continuarMon = "";
            CargaMonitoreo miCarga = new CargaMonitoreo(ctrIOT,"monitoreo_",".csv");

            CargaInicial();

            do
            {
                opcion = ServValidac.PedirStrNoVac("Menú Principal - Ingrese opción"
                    + "\n" + OpcLisEq + "-Listar Equipos"
                    + "\n" + OpcLisBi + "-Listar Bienes"
                    + "\n" + OpcMonit + "-Monitoreo"
                    + "\n" + OpcSalir + "-Salir"
                    );

                switch (opcion)
                {
                    case OpcLisEq:
                        Console.WriteLine("\n\nListando Equipos");
                        ListarEquipos();
                        break;
                    case OpcLisBi:
                        Console.WriteLine("\n\nListando Bienes");
                        ListarBienes();
                        break;
                    case OpcMonit:
                        do
                        {
                            Console.WriteLine(miCarga.Ejecutar());
                            Console.WriteLine("\n\nMonitoreando");
                            MonitorearBienes();
                            continuarMon = ServValidac.PedirSoN("¿Desea repetir el monitoreo? S/N");
                        } while (continuarMon != OpcMonNo);
                        break;
                    case OpcSalir:
                        break;
                    default:
                        Console.WriteLine("\n\nOpción Inválida");
                        break;
                }

            } while (opcion != OpcSalir);
        }

        private void ListarEquipos()
        {
            Console.WriteLine(ctrIOT.ListaEquipos());
        }


        private void ListarBienes()
        {
            Console.WriteLine(ctrIOT.ListaBienes());
        }


        private void MonitorearBienes()
        {
            Console.WriteLine(ctrIOT.MonitoreoBienes());
        }

        private void MostrarNoVacio(string texto)
        {
            if (texto != "") {
                Console.WriteLine(texto);
            }
        }

        private void CargaInicial()
        {
            CargaMaestros cargaMae = new CargaMaestros();
            MostrarNoVacio(cargaMae.CargarMaestros(ctrIOT,"maestros.csv",";"));
        }
    }
}
