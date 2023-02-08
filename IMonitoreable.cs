using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTMonitoreoPozos
{
    interface IMonitoreable
    {
        int GetNumero();
        string AgregarSensor(Sensor nvoSensor);
        string ResumirMonitoreo();
        string ResumirConMonitoreo();
        Sensor GetSensorInstXNro(int nro);
    }
}
