using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Modelos.MulticentroMod
{
    internal class M_ReporteAM : M_usuario
    {
        public int id_reporte {  get; set; }
        public string data { get; set; }
        public string fecha { get; set; }
        public int id_alarma { get; set; }

    }
}
