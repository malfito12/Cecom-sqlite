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

namespace Cecom.Vista.RadioBases.Llaves
{
    /// <summary>
    /// Lógica de interacción para R_Llave.xaml
    /// </summary>
    public partial class R_Llave : Page
    {
        int _idPersonal;
        int _idEmpresa;
        int primero = 10001;
        int operacion;

        private CollectionViewSource viewSource;
        public R_Llave()
        {
            InitializeComponent();
            viewSource = new CollectionViewSource();
            this.operacion = 0;
            codigo();
            limpiar();
            getLlaves();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.operacion = 1;
            limpiar();
            codigo();
            llformulario.IsEnabled = true;

        }

        private void getLlaves()
        {
            Ctr_Llave llave = new Ctr_Llave();
            List<M_Llave> lista = llave.getLLave();
            dgv_ListaLLaves.ItemsSource = null;
            dgv_ListaLLaves.SelectedValuePath = "id_llave";
            viewSource.Source = lista; // Asigna la lista al CollectionViewSource
            dgv_ListaLLaves.ItemsSource = viewSource.View;
        }

        private void codigo()
        {
            Ctr_Llave llave = new Ctr_Llave();
            int cod = llave.codigo();
            string aux;
            if (cod != 0)
            {
                primero = cod + 1;
                aux = "ING-" + primero.ToString();
                lbcodigo.Content = aux;
            }
            else
            {
                aux = "ING-" + primero.ToString();
                lbcodigo.Content = aux;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool resp;
            M_Llave data = new M_Llave
            {
                l_codigo = primero,
                l_sitio = lltxt_sitio.Text.Trim(),
                l_departamento = llcb_departamento.Text.Trim(),
                id_personal = _idPersonal,
                id_empresa = _idEmpresa,
                l_responsable = lltxt_responsable.Text.Trim(),
                l_motivo = lltxt_observaciones.Text.Trim(),

            };
            if (this.operacion == 1)
            {
                Ctr_Llave llave = new Ctr_Llave();
                resp = llave.saveLlave(data);

            }
            else
            {
                Ctr_Llave llave = new Ctr_Llave();
                resp = llave.updateLlave(data, (int)dgv_ListaLLaves.SelectedValue);
            }
            if (resp)
            {
                MessageBox.Show("La operacion se realizó correctamente");
                getLlaves();
                limpiar();
            }

            else MessageBox.Show("Error. no se realizó la operación");
        }

        private void lltxt_ci_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Ctr_Personal personal = new Ctr_Personal();
                M_Personal lista = personal.getPersonalId(lltxt_ci.Text.Trim());
                if (lista != null)
                {
                    lltxt_nombre.Text = lista.pt_nombre;
                    lltxt_empresa.Text = lista.em_nombre;
                    _idPersonal = Convert.ToInt32(lista.id_personal);
                    _idEmpresa = Convert.ToInt32(lista.id_empresa);
                }
            }

        }

        private void txtsource_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewSource.Filter -= filtrarTabla; // Evita duplicados al agregar
            viewSource.Filter += filtrarTabla; // Agrega el método de filtrado
            viewSource.View.Refresh();// Actualiza la vista
        }

        private void filtrarTabla(object sender, FilterEventArgs e)
        {
            var filter = txtsource.Text.ToLower();// Obtiene el texto ingresado
            var llave = e.Item as M_Llave;
            if (llave != null)
            {
                e.Accepted =
                    llave.l_codigo.ToString().Contains(filter) ||
                    llave.l_sitio.ToLower().Contains(filter);
            }

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var llave = button.CommandParameter as M_Llave; // Obtiene el objeto correspondiente a la fila
                if (llave != null)
                {

                    // Lógica para eliminar la llave

                    // Por ejemplo, llamar a un método en el controlador para eliminarlo
                    //Ctr_Llave controlador = new Ctr_Llave();
                    //controlador.EliminarLlave(llave.id_llave); // Asegúrate de tener este método en tu controlador
                    CerrarLlave cerrar = new CerrarLlave(llave);
                    bool? resp=cerrar.ShowDialog();
                    if (resp==true)
                    {
                        getLlaves();
                    }
                    //MessageBox.Show(llave.l_codigo.ToString());
                    // Refrescar el DataGrid
                    //getLlaves();
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dgv_ListaLLaves.SelectedItem != null)
            {
                this.operacion = 2;
                llformulario.IsEnabled = true;
                M_Llave lista = (M_Llave)dgv_ListaLLaves.SelectedItem;
                lltxt_sitio.Text = lista.l_sitio;
                llcb_departamento.Text = lista.l_departamento.ToString();
                lltxt_ci.Text = lista.pt_ci;
                lltxt_nombre.Text = lista.pt_nombre;
                lltxt_empresa.Text = lista.em_nombre;
                lltxt_responsable.Text = lista.l_responsable;
                lltxt_observaciones.Text = lista.l_motivo;
                _idPersonal = lista.id_personal;
                _idEmpresa = lista.id_empresa;

            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            limpiar();

        }

        public void limpiar()
        {
            lltxt_sitio.Text = "";
            llcb_departamento.Text = "";
            lltxt_ci.Text = "";
            lltxt_nombre.Text = "";
            lltxt_empresa.Text = "";
            lltxt_responsable.Text = "";
            lltxt_observaciones.Text = "";
            llformulario.IsEnabled = false;
            dgv_ListaLLaves.SelectedItem = null;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (dgv_ListaLLaves.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Estas seguro de eliminar", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                Ctr_Llave llave = new Ctr_Llave();
                if (result == MessageBoxResult.OK)
                {
                    llave.deleteLlave((int)dgv_ListaLLaves.SelectedValue);
                    //usuario.eliminar_usuario(dgv_usuarios.SelectedValue.ToString());
                    getLlaves();
                    limpiar();

                }
            }
        }
    }
}
