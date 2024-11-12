using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Modelos.RadioBaseMod
{
    public class M_Llave : M_Personal
    {
        public int id_llave {  get; set; }
        public int l_codigo { get; set; }
        public string l_fechaI { get; set; }
        public string l_hora { get; set; }
        public string l_sitio { get; set; }
        public string l_departamento { get; set; }
        public string l_responsable { get; set; }
        public string l_motivo { get; set; }
        public string l_fechaS { get; set; }
        public int l_estado { get; set; }

    }
}
