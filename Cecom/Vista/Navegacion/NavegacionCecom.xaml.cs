using Cecom.Multicentros;
using Cecom.RadioBases;
using Cecom.Vista.Usuarios;
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
using System.Windows.Shapes;

namespace Cecom.Navegacion
{
    /// <summary>
    /// Lógica de interacción para NavegacionCecom.xaml
    /// </summary>
    public partial class NavegacionCecom : Window
    {
        public NavegacionCecom()
        {
            InitializeComponent();
            MenuMulticentros menuMulticentros = new MenuMulticentros();
            MainFrame.Navigate(menuMulticentros);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuMulticentros menuMulticentros = new MenuMulticentros();
            MainFrame.Navigate(menuMulticentros);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MenuRadio menuRadio = new MenuRadio();
            MainFrame.Navigate(menuRadio);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            R_usuario usuario = new R_usuario();
            MainFrame.Navigate(usuario);
        }
    }
}
