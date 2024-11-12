using Cecom.Controladores;
using Cecom.Modelos;
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
using System.Windows.Shapes;

namespace Cecom.Vista.Multicentros.CRUD_M
{
    /// <summary>
    /// Lógica de interacción para C_multicentro.xaml
    /// </summary>
    public partial class C_multicentro : Window
    {
        int numero;
        DataRowView dataFila;
        public C_multicentro(int num, DataRowView fila)
        {
            InitializeComponent();
            this.numero = num;
            this.dataFila = fila;
            llenadoForm();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (numero == 1)
                {
                    M_multicentro data = new M_multicentro();
                    data.nombre = mtxt_nombre.Text.Trim();
                    data.cuenta = Convert.ToInt32(mtxt_cuenta.Text.Trim());
                    data.departamento = mcb_departamento.Text.Trim();
                    Crt_multicentro crt_Multicentro = new Crt_multicentro();
                    bool resp = crt_Multicentro.Save_multicentro(data);
                    if (resp)
                    {

                        MessageBox.Show("La acción se realizó correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                        this.Close();
                    }

                    else MessageBox.Show("Error, No se pudo realizar la accion");
                }
                else
                {
                    M_multicentro data = new M_multicentro();
                    data.id_multicentro = Convert.ToInt32(dataFila["id_multicentro"]);
                    data.nombre = mtxt_nombre.Text.Trim();
                    data.cuenta = Convert.ToInt32(mtxt_cuenta.Text.Trim());
                    data.departamento = mcb_departamento.Text.Trim();
                    Crt_multicentro crt_multicentro = new Crt_multicentro();
                    bool resp = crt_multicentro.Update_multicentro(data);
                    if (resp)
                    {
                        MessageBox.Show("La acción se realizó correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                        this.Close();
                    }
                    else MessageBox.Show("Error, No se pudo realizar la accion");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); MessageBox.Show("Coloque Correctamente los datos"); }
        }
        private void llenadoForm()
        {
            if (dataFila != null)
            {
                mtxt_nombre.Text = dataFila["nombre"].ToString();
                mtxt_cuenta.Text = dataFila["cuenta"].ToString();
                mcb_departamento.Text = dataFila["departamento"].ToString();
            }
        }


    }
}
