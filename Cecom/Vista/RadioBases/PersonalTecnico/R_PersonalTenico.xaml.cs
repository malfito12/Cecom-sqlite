using Cecom.Controladores.RadioBaseCtr;
using Cecom.Modelos.RadioBaseMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cecom.Vista.RadioBases.PersonalTecnico
{
    /// <summary>
    /// Lógica de interacción para R_PersonalTenico.xaml
    /// </summary>
    public partial class R_PersonalTenico : Page
    {
        public R_PersonalTenico()
        {
            InitializeComponent();
            l_personal();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            C_PersonalTecnico personal=new C_PersonalTecnico();
            bool? resp=personal.ShowDialog();
            if (resp == true)
            {
                l_personal();
            }
        }

        private void l_personal()
        {
            Ctr_Personal personal=new Ctr_Personal();
            List<M_Personal> lista = personal.getPersonal();
            dgv_listaPersonal.ItemsSource = null;
            dgv_listaPersonal.SelectedValuePath = "id_personal";
            dgv_listaPersonal.ItemsSource = lista;
        }
    }
}
