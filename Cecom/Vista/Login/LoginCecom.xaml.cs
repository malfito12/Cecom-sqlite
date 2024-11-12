using Cecom.Controladores.login;
using Cecom.Modelos.login;
using Cecom.Navegacion;
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

namespace Cecom.Login
{
    /// <summary>
    /// Lógica de interacción para LoginCecom.xaml
    /// </summary>
    public partial class LoginCecom : Window
    {
        public LoginCecom()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool resp;
            string user= UsernameTextBox.Text;
            string pass = PasswordBox.Password;

            Crt_login login= new Crt_login();
            resp=login.Login_data(user, pass);

            if(resp)
            //if(user=="admin"&&pass=="admin")
            {
                //MessageBox.Show("Bienvenido "+M_login.name.ToString());
                NavegacionCecom navegacionCecom = new NavegacionCecom();
                navegacionCecom.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario Incorrecto");
            }

        }

        
    }
}
