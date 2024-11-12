using Cecom.Vista.RadioBases.Empresas;
using Cecom.Vista.RadioBases.Llaves;
using Cecom.Vista.RadioBases.PersonalTecnico;
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

namespace Cecom.RadioBases
{
    /// <summary>
    /// Lógica de interacción para MenuRadio.xaml
    /// </summary>
    public partial class MenuRadio : Page
    {
        public MenuRadio()
        {
            InitializeComponent();
            R_Llave llave = new R_Llave();
            FrameRadioBases.Navigate(llave);

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            R_Empresa empresa = new R_Empresa();
            FrameRadioBases.Navigate(empresa);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            R_PersonalTenico personal = new R_PersonalTenico();
            FrameRadioBases.Navigate(personal);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            R_Llave llave= new R_Llave();
            FrameRadioBases.Navigate(llave);
        }
    }
}
