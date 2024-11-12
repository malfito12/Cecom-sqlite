using Cecom.Controladores.RadioBaseCtr;
using Cecom.Modelos.RadioBaseMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Cecom.Vista.RadioBases.Empresas
{
    /// <summary>
    /// Lógica de interacción para C_Empresa.xaml
    /// </summary>
    public partial class C_Empresa : Window
    {
        int numero;
        M_Empresa data;
        public C_Empresa(int num, M_Empresa emp)
        {
            InitializeComponent();
            this.numero = num;
            this.data = emp;
            asignar();
        }


        private void asignar()
        {
            if (this.data != null)
            {
                etxt_nombre.Text = data.em_nombre;
                etxt_alias.Text = data.em_alias;
                etxt_direccion.Text = data.em_direccion;
                etxt_telefono.Text = data.em_telefono;

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool resp;
            M_Empresa empresa = new M_Empresa();
            empresa.em_nombre = etxt_nombre.Text.Trim();
            empresa.em_alias = etxt_alias.Text.Trim();
            empresa.em_direccion = etxt_direccion.Text.Trim();
            empresa.em_telefono = etxt_telefono.Text.Trim();
            Ctr_Empresa ctr_Empresa = new Ctr_Empresa();
            if (numero == 1) resp = ctr_Empresa.saveEmpresa(empresa);
            else resp = ctr_Empresa.updateEmpresa(empresa, data.id_empresa);


            if (resp)
            {
                MessageBox.Show("La accion se realizó correctamente");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error, No se realizó la accion");
            }
        }
    }
}
