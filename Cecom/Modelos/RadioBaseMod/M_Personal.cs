using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Modelos.RadioBaseMod
{
    public class M_Personal: M_Empresa
    {
        public int id_personal {  get; set; }
        public string pt_nombre { get; set; }
        public string pt_ci { get; set; }
        public int pt_telefono { get; set; }
    }
}
