using Cecom.Controladores;
using Cecom.Controladores.MulticentroCtr;
using Cecom.Modelos;
using Cecom.Modelos.login;
using Cecom.Modelos.MulticentroMod;
using Cecom.Multicentros;
using Cecom.Vista.Multicentros.CRUD_M;
using Cecom.Vista.Multicentros.ReportesM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;


namespace Cecom.Vista.Multicentros.EventosM
{
    /// <summary>
    /// Lógica de interacción para R_Eventos.xaml
    /// </summary>
    public partial class R_Eventos : Page
    {
        int? id_m = null;
        int _id;
        int? valor;
        private MenuMulticentros menuMulticentros;
        public R_Eventos(M_AlarmaM data, MenuMulticentros menu)
        {
            InitializeComponent();
            menuMulticentros = menu;
            this._id = data.id_alarma;
            this.valor = null;
            l_eventos();
        }

        private void limpiar()
        {
            etxt_departamento.Text = "";
            etxt_nombre.Text = "";
            id_m = null;
            ecb_tipo.Text = "";
            etxt_observaciones.Text = "";

        }
        private void Label_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Ctr_EventoM eventoDB = new Ctr_EventoM();

                M_multicentro resp = eventoDB.search_m(etxt_cuenta.Text.ToString());
                if (resp != null)
                {
                    etxt_departamento.Text = resp.departamento.ToString();
                    etxt_nombre.Text = resp.nombre.ToString();
                    id_m = resp.id_multicentro;
                }
                else
                {
                    limpiar();
                }




                //etxt_departamento.Text=resp.departamento.ToString();
                //MessageBox.Show(resp.nombre.ToString());


                //MessageBox.Show(etxt_cuenta.Text.ToString(),"message");
                //etxt_cuenta.Clear();
                //e.Handled = true;//evistar en sonido
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.valor == 1)
            {
                Ctr_EventoM eventoDB = new Ctr_EventoM();
                DateTime time = DateTime.Now;
                M_EventoM data = new M_EventoM
                {
                    hora_e = time.ToString("HH:mm:ss"),
                    fecha_e = time.ToString("yyyy-MM-dd"),
                    tipo = ecb_tipo.Text.ToString(),
                    observaciones = etxt_observaciones.Text.ToString(),
                    id_multicentro = id_m,
                    id_alarma = Convert.ToInt32(idEvento.Content)
                };
                //MessageBox.Show($"{data.id_multicentro}");
                bool resp = eventoDB.save_eventoM(data);
                if (resp)
                {
                    MessageBox.Show("Registrado");
                    l_eventos();
                    limpiar();
                    etxt_cuenta.Text = "";
                }
            }
            else if (this.valor == 2)
            {
                Ctr_EventoM eventoDB = new Ctr_EventoM();
                //DateTime time = DateTime.Now;
                M_EventoM data = new M_EventoM
                {
                    //hora_e = time.ToString("HH:mm:ss"),
                    //fecha_e = time.ToString("yyyy-MM-dd"),

                    tipo = ecb_tipo.Text.ToString(),
                    observaciones = etxt_observaciones.Text.ToString(),
                    id_evento = (int)dgv_leventos.SelectedValue,
                    id_multicentro = id_m,
                    id_alarma = Convert.ToInt32(idEvento.Content)
                };
                //MessageBox.Show($"{data.id_multicentro}");
                bool resp = eventoDB.update_eventoM(data);
                if (resp)
                {
                    MessageBox.Show("Actualizado");
                    l_eventos();
                    limpiar();
                    etxt_cuenta.Text = "";
                    eformulario.IsEnabled = false;
                }
            }
        }

        private void l_eventos()
        {
            //string id = idEvento.Content.ToString();
            Ctr_EventoM evento = new Ctr_EventoM();
            List<M_EventoM> lista = evento.lista_eventosM(this._id);
            //MessageBox.Show(lista.ToString());
            //int cont = lista.Count;
            dgv_leventos.ItemsSource = null;
            dgv_leventos.SelectedValuePath = "id_evento";
            dgv_leventos.ItemsSource = lista;
            /*if (lista != null && lista.Count > 0)
            {
                dgv_leventos.ItemsSource = null; // Limpia la fuente de datos
                dgv_leventos.ItemsSource = lista; // Asigna la lista a la fuente de datos
            }
            else
            {
                MessageBox.Show("No se encontraron eventos.");
            }*/
            /*DataTable lista = new DataTable();
            Crt_usuario _usuario = new Crt_usuario();
            _usuario.ListaUsuarios(lista);
            dgv_usuarios.ItemsSource = null;
            dgv_usuarios.SelectedValuePath = "id_usuario";
            dgv_usuarios.ItemsSource = lista.DefaultView;*/

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            eformulario.IsEnabled = true;
            this.valor = 1;
            limpiar();
            etxt_cuenta.Text = "";

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            limpiar();
            etxt_cuenta.Text = "";
            eformulario.IsEnabled = false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (dgv_leventos.SelectedItem != null)
            {
                this.valor = 2;
                eformulario.IsEnabled = true;
                M_EventoM fila = (M_EventoM)dgv_leventos.SelectedItem;
                etxt_cuenta.Text = fila.cuenta.ToString();
                etxt_nombre.Text = fila.nombre.ToString();
                etxt_departamento.Text = fila.departamento.ToString();
                ecb_tipo.Text = fila.tipo.ToString();
                etxt_observaciones.Text = fila.observaciones.ToString();
                id_m = fila.id_multicentro;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (dgv_leventos.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Estas seguro de eliminar", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                Ctr_EventoM evento = new Ctr_EventoM();
                if (result == MessageBoxResult.OK)
                {
                    evento.delete_eventoM(dgv_leventos.SelectedValue.ToString());
                    l_eventos();
                    limpiar();
                    eformulario.IsEnabled = false;
                    etxt_cuenta.Text = "";
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var dataList = dgv_leventos.ItemsSource as List<M_EventoM>; // o la colección que estés usando

            if (dataList != null && dataList.Any())
            {
                /*List<object>nuevo = new List<object>();
                foreach (var item in dataList)
                {
                    nuevo.Add(item);

                }*/
                var listaEventos = dataList.Select(x => new
                {
                    //datos = new { x.nombre },
                    x.nombre,
                    x.cuenta,
                    x.hora_e,
                    x.fecha_e,
                    x.departamento,
                    x.tipo,
                    x.name,
                    x.observaciones,
                }).ToList();
                string json = JsonSerializer.Serialize(listaEventos, new JsonSerializerOptions { WriteIndented = true });
                MessageBoxResult result = MessageBox.Show("Estas seguro de guardar la informacion", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    M_ReporteAM m_ReporteAM = new M_ReporteAM();
                    m_ReporteAM.data = json;
                    m_ReporteAM.id_alarma = this._id;
                    Ctr_ReporteAM reporte = new Ctr_ReporteAM();
                    bool resp = reporte.save_reporte_am(m_ReporteAM);
                    if (resp)
                    {
                        Ctr_EventoM evento = new Ctr_EventoM();
                        bool resp2 = evento.successReporte(this._id);
                        if (resp2)
                        {
                            Ctr_AlarmaM alarma = new Ctr_AlarmaM();
                            bool resp3 = alarma.updateReportSuccess(this._id);
                            if (resp3)
                            {
                                MessageBox.Show("La Accion se realizó Correctamente");
                                menuMulticentros.PagReportesM();

                            }
                        }



                    }
                    //MessageBox.Show(json, "Lista de Estudiantes en JSON", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("No hay datos disponibles.", "Lista de Datos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
