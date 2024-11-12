using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Modelos.MulticentroMod
{
    public class M_AlarmaM : M_usuario
    {
        public int id_alarma {  get; set; }
        public string evento { get; set; }
        public string fecha { get; set; }
        public int estado { get; set; }

        /*public void idAlarma(int id)
        {
            id_alarma = id;
        }*/
    }
}
