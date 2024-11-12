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
using System.Windows.Shapes;

namespace Cecom.Vista.RadioBases.Llaves
{
    /// <summary>
    /// Lógica de interacción para CerrarLlave.xaml
    /// </summary>
    public partial class CerrarLlave : Window
    {
        M_Llave newData;
        public CerrarLlave(M_Llave data)
        {
            InitializeComponent();
            this.newData = data;
            setData();
        }

        private void setData()
        {
            
            lbcodigo.Content="Codigo: "+ newData.l_codigo.ToString();
            lbhora.Content= "Hora: " + newData.l_hora.ToString();
            lbfecha.Content= "Fecha: " + newData.l_fechaI.ToString();
            lbsitio.Content= "Sitio: " + newData.l_sitio.ToString();
            lbdepartamento.Content= "Departamento: " + newData.l_departamento.ToString();
            lbempresa.Content= "Empresa: " + newData.em_nombre.ToString();
            lbnombretec.Content= "Nombre Tecnico: " + newData.pt_nombre.ToString();
            lbtelefono.Content= "Telefono: " + newData.pt_telefono.ToString();
            lbci.Content= "Cedula de Identidad: " + newData.pt_ci.ToString();
            lbresponsable.Content= "Responsable: " + newData.l_responsable.ToString();
            lbmotivo.Content= "Motivo de Ingreso: " + newData.l_motivo.ToString();
            lboperador.Content= "Operador: " + newData.name.ToString();

            Button btn = new Button
            {
                Content = "Cerrar"
            };
            btn.Click += (sender, e) =>
            {
                MessageBoxResult result = MessageBox.Show("Estas seguro de cerrar el codigo?", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                Ctr_Llave llave = new Ctr_Llave();
                if (result == MessageBoxResult.OK)
                {
                    llave.CerrarLlave(2, (int)newData.id_llave);
                    //usuario.eliminar_usuario(dgv_usuarios.SelectedValue.ToString());
                    this.DialogResult = true;   
                    this.Close();
                }
            };
            Label texto = new Label { Content = "CODIGO CERRADO" , FontSize=13, FontWeight= FontWeights.Bold };
            if (newData.l_estado == 1)
            {
                stackLlave.Children.Add(btn);
            }
            else
            {
                stackLlave.Children.Add(texto);
            }
        }
    }
}
