using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Modelos.MulticentroMod
{
    public class M_EventoM : M_multicentro
    {
        public int id_evento {  get; set; }
        public string hora_e { get; set; }
        public string fecha_e { get; set; }
        public string tipo { get; set; }
        public string observaciones { get; set; }
        
    }
}
