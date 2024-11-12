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
using System.Windows.Shapes;

namespace Cecom.Vista.RadioBases.PersonalTecnico
{
    /// <summary>
    /// Lógica de interacción para C_PersonalTecnico.xaml
    /// </summary>
    public partial class C_PersonalTecnico : Window
    {
        public C_PersonalTecnico()
        {
            InitializeComponent();
            ListaEmpresa();
        }

        private List<M_Empresa> ListaEmpresa()
        {
            Ctr_Empresa empresa= new Ctr_Empresa();
            List<M_Empresa> lista = empresa.getEmpresa();
            pcb_empresa.ItemsSource = null;
            pcb_empresa.DisplayMemberPath = "em_nombre";//muestra lista de empresas con el nombre
            pcb_empresa.SelectedValuePath = "id_empresa";// se optiene el id de la empresa
            pcb_empresa.ItemsSource = lista;// carga la lista de las empresas 
            return lista;
        }

        private void pbtn_guardar_Click(object sender, RoutedEventArgs e)
        {
            M_Personal personal = new M_Personal
            {
                pt_nombre = ptxt_nombre.Text.Trim(),
                pt_ci = ptxt_ci.Text.Trim(),
                pt_telefono =Int32.Parse(ptxt_telefono.Text.Trim()),
                id_empresa = (int)pcb_empresa.SelectedValue,
            };
            Ctr_Personal ctr_Personal = new Ctr_Personal();
            bool resp= ctr_Personal.savePersonal(personal);
            if (resp)
            {
                MessageBox.Show("La accion se realizó correctamente");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("La accion se realizó correctamente");
            }
        }
        
    }
}
