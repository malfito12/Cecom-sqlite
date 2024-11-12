using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Cecom.Modelos
{
    public class M_usuario : Iusuario
    {
        public int id_usuario { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string rol { get; set; }
        public bool Validar()   
        {
            return !string.IsNullOrWhiteSpace(name) &&
                   !string.IsNullOrWhiteSpace(password) &&
                   !string.IsNullOrWhiteSpace(rol) &&
                   !string.IsNullOrWhiteSpace(email);
        }
    }
    public interface Iusuario
    {
        string name { get; set; }
        string password { get; set; }
        string email { get; set; }
        string rol { get; set; }
        bool Validar();
    }

}
