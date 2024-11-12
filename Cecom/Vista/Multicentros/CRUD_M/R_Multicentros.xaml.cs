using Cecom.Controladores;
using Cecom.Vista.Multicentros.CRUD_M;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Cecom.Multicentros
{
    /// <summary>
    /// Lógica de interacción para R_Multicentros.xaml
    /// </summary>
    public partial class R_Multicentros : Page
    {
        public R_Multicentros()
        {
            InitializeComponent();
            l_multicentro();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            C_multicentro multicentro = new C_multicentro(1,null);
            bool? resp=multicentro.ShowDialog();
            /*C_Multicentro c_Multicentro = new C_Multicentro(1,null);
            c_Multicentro.ShowDialog();
            bool resp = c_Multicentro.confirmacion();*/
            if (resp==true)
            {
                l_multicentro();

            }

        }

        private void l_multicentro()
        {
            DataTable lista = new DataTable();
            Crt_multicentro crt_Multicentro = new Crt_multicentro();
            crt_Multicentro.Lista_multicentro(lista);
            dgv_multicentro.SelectedValuePath = "id_multicentro";
            dgv_multicentro.ItemsSource = lista.DefaultView;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgv_multicentro.SelectedItem != null)
            {
                DataRowView fila = (DataRowView)dgv_multicentro.SelectedItem;
                C_multicentro c_Multicentro = new C_multicentro(2, fila);

                bool? resp = c_Multicentro.ShowDialog();
                if (resp == true)
                {
                    l_multicentro();

                }

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(dgv_multicentro.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Estas seguro de eliminar", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                Crt_multicentro multicentro = new Crt_multicentro();
                if (result == MessageBoxResult.OK)
                {
                    multicentro.delete_multicentro(dgv_multicentro.SelectedValue.ToString());
                    //usuario.eliminar_usuario(dgv_usuarios.SelectedValue.ToString());
                    l_multicentro();
                }
            }
        }
    }
}
