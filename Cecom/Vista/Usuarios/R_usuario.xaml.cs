using Cecom.Controladores;
using Cecom.Modelos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using iTextSharp.tool.xml;
using System.Text;

namespace Cecom.Vista.Usuarios
{
    /// <summary>
    /// Lógica de interacción para R_usuario.xaml
    /// </summary>
    public partial class R_usuario : Page
    {
        public R_usuario()
        {
            InitializeComponent();
            lista_u();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int num = 1;
            
            C_Usuario usuario= new C_Usuario(num, null);
            bool? resp=usuario.ShowDialog();
            if (resp == true)
            {
                lista_u();
            }
            /*C_usuario usuario = new C_usuario(num, null);
            //bool resp = usuario.confirmacion();
            System.Windows.Forms.DialogResult respuesta = usuario.ShowDialog();
            if (respuesta== System.Windows.Forms.DialogResult.OK)// cambiar a herencia de c_usuarios Windows
            {
                lista_u();
            }*/


        }

        private void lista_u()
        {
            DataTable lista = new DataTable();
            Crt_usuario _usuario = new Crt_usuario();
            _usuario.ListaUsuarios(lista);
            dgv_usuarios.ItemsSource = null;
            dgv_usuarios.SelectedValuePath = "id_usuario";
            dgv_usuarios.ItemsSource = lista.DefaultView;

        }

        private void dgv_usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if(dgv_usuarios.SelectedItem !=null)
            {
                MessageBox.Show(dgv_usuarios.SelectedValue.ToString());
            }*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataRowView fila = (DataRowView)dgv_usuarios.SelectedItem;
            if (dgv_usuarios.SelectedItem != null)
            {
                int num = 2;
                /*C_usuario usuario = new C_usuario(num, fila);
                usuario.ShowDialog();

                lista_u();*/

                C_Usuario usuario = new C_Usuario(num, fila);
                bool? resp = usuario.ShowDialog();
                if (resp == true)
                {
                    lista_u();
                }


            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgv_usuarios.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Estas seguro de eliminar", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                Crt_usuario usuario = new Crt_usuario();
                if (result == MessageBoxResult.OK)
                {
                    usuario.eliminar_usuario(dgv_usuarios.SelectedValue.ToString());
                    lista_u();
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GenerarReporte();
        }

        public void GenerarReporte()
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog
                {
                    FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf",
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    Title = "Guardar Reporte PDF"
                };
                string paginahtml = Properties.Resources.reporte_usuarios.ToString();
                string filas = string.Empty;
                StringBuilder tabla = new StringBuilder();
                foreach (var dat in dgv_usuarios.Items)
                {
                    // Cast al tipo adecuado; ajusta según el tipo real de los datos en Items
                    var rowView = dat as DataRowView; // Cambia 'MiTipoDeObjeto' al tipo real

                    if (rowView != null)
                    {
                        var row = rowView.Row;

                        filas += "<tr>";
                        filas += $"<td>{row["name"]}</td>"; // Ajusta el nombre de las columnas
                        filas += $"<td>{row["email"]}</td>";
                        filas += $"<td>{row["rol"]}</td>";
                        filas += "</tr>";
                    }

                }
                /*foreach (var dat in dgv_usuarios.Items)
                {
                    filas += "<tr>";
                    //filas += $"<td>{tabla.AppendLine(dat.ToString())}</td>";
                    filas += $"<td>{}</td>";
                    filas += $"<td>hola2</td>";
                    filas += $"<td>hola3</td>";
                    filas += "</tr>";


                }*/
                paginahtml = paginahtml.Replace("@FILAS", filas);
                bool? result = save.ShowDialog();
                if (result == true)
                {
                    using (FileStream stream = new FileStream(save.FileName, FileMode.Create, FileAccess.Write))
                    {
                        using (Document pagina = new Document(PageSize.A4, 25, 25, 25, 25))
                        {

                            PdfWriter doc = PdfWriter.GetInstance(pagina, stream);

                            pagina.Open();
                            using (StringReader reader = new StringReader(paginahtml))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(doc, pagina, reader);
                            }
                            //pagina.Add(new Phrase("hola que ashe"));

                            //pagina.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
