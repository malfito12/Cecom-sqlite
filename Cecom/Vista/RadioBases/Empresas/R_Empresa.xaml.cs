using Cecom.Controladores;
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

namespace Cecom.Vista.RadioBases.Empresas
{
    /// <summary>
    /// Lógica de interacción para R_Empresa.xaml
    /// </summary>
    public partial class R_Empresa : Page
    {
        public R_Empresa()
        {
            InitializeComponent();
            l_empresa();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            C_Empresa empresa = new C_Empresa(1, null);
            bool? resp = empresa.ShowDialog();
            if (resp == true)
            {
                l_empresa();
            }
        }

        private void l_empresa()
        {
            Ctr_Empresa empresa = new Ctr_Empresa();
            List<M_Empresa> lista = empresa.getEmpresa();
            dgv_listaEmpresa.ItemsSource = null;
            dgv_listaEmpresa.SelectedValuePath = "id_empresa";
            dgv_listaEmpresa.ItemsSource = lista;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgv_listaEmpresa.SelectedItem != null)
            {
                M_Empresa data = (M_Empresa)dgv_listaEmpresa.SelectedItem;

                C_Empresa empresa = new C_Empresa(2, data);
                bool? resp = empresa.ShowDialog();
                if (resp == true)
                {
                    l_empresa();

                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgv_listaEmpresa.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Estas seguro de eliminar", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                Ctr_Empresa empresa = new Ctr_Empresa();
                if (result == MessageBoxResult.OK)
                {
                    empresa.deleteEmpresa(Convert.ToInt32(dgv_listaEmpresa.SelectedValue));
                    //usuario.eliminar_usuario(dgv_usuarios.SelectedValue.ToString());
                    l_empresa();
                }
            }
        }
    }
}
