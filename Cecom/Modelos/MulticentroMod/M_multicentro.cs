using Cecom.Modelos.MulticentroMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Modelos
{
    public class M_multicentro : M_AlarmaM
    {
        public int? id_multicentro {  get; set; }  // Cambiado a int? puede ser null
        public int cuenta {  get; set; }
        public string nombre {  get; set; }
        public string departamento {  get; set; }
        
    }
}
