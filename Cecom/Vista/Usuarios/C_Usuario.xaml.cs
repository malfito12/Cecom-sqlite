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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecom.Vista.Usuarios
{
    /// <summary>
    /// Lógica de interacción para C_Usuario.xaml
    /// </summary>
    public partial class C_Usuario : Window
    {
        int numero;
        DataRowView newData;
        public C_Usuario(int num, DataRowView data)
        {
            InitializeComponent();
            this.numero = num;
            newData = data;
            llenadofmr();
        }

        public void llenadofmr()
        {
            if (newData != null)
            {
                utxt_name.Text = newData["name"].ToString();
                utxt_pass.Password = newData["password"].ToString();
                utxt_email.Text = newData["email"].ToString();
                ucb_rol.Text = newData["rol"].ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (numero == 1)
            {
                Iusuario usuario = new M_usuario();
                usuario.name = utxt_name.Text.Trim();
                usuario.password = utxt_pass.Password.Trim();
                usuario.email = utxt_email.Text.Trim();
                usuario.rol = ucb_rol.Text.Trim();

                if (usuario.Validar())
                {
                    Crt_usuario crt_Usuario = new Crt_usuario();
                    bool resp = crt_Usuario.guardar_usuario(usuario);
                    if (resp)
                    {
                        MessageBox.Show("La acción se realizó correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error");

                    }
                }
                else
                {
                    MessageBox.Show("complete todos los campos");
                }

            }
            else
            {
                M_usuario usuario = new M_usuario();
                usuario.id_usuario = Convert.ToInt32(newData["id_usuario"]);
                usuario.name = utxt_name.Text.Trim();
                usuario.password = utxt_pass.Password.Trim();
                usuario.email = utxt_email.Text.Trim();
                usuario.rol = ucb_rol.Text.Trim();
                Crt_usuario crt_Usuario = new Crt_usuario();
                bool resp = crt_Usuario.editar_usuario(usuario);
                if (resp)
                {
                    MessageBox.Show("La acción se realizó correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        
    }
}
