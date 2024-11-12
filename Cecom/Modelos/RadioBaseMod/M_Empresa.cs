using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Modelos.RadioBaseMod
{
    public class M_Empresa : M_usuario
    {
        public int id_empresa {  get; set; }
        public string em_nombre { get; set;}
        public string em_alias { get; set;}
        public string em_direccion { get; set;}
        public string em_telefono { get; set;}
    }
}
