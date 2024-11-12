using Cecom.Controladores.MulticentroCtr;
using Cecom.Modelos.MulticentroMod;
using Cecom.Vista.Multicentros.AlarmasM;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Cecom.Multicentros
{
    public partial class RegistroAlarma : Page
    {
        private MenuMulticentros menuMulticentros;
        public RegistroAlarma(MenuMulticentros menu)
        {
            InitializeComponent();
            menuMulticentros = menu;
            l_alarmaM();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            C_AlarmaM alarma = new C_AlarmaM(1, null);
            bool? resp = alarma.ShowDialog();
            if (resp == true)
            {
                l_alarmaM();
            }
        }

        private void l_alarmaM()
        {
            Ctr_AlarmaM ctr_AlarmaM = new Ctr_AlarmaM();
            List<M_AlarmaM> listaAlarmas = ctr_AlarmaM.Lista_alarmaM();
            int cont = listaAlarmas.Count;
            stack1.Children.Clear();
            for (int i = 0; i < cont; i++)
            {
                int currentIndex = i;
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) }); // 70%
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) }); // 30%
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.1, GridUnitType.Star) }); // 30%
                TextBlock textBlock = new TextBlock
                {
                    Text = listaAlarmas[currentIndex].estado != 2 ? $"{listaAlarmas[currentIndex].evento}" : $"{listaAlarmas[currentIndex].evento} - Cerrado",
                    Padding = new Thickness(10),
                    Margin = new Thickness(20, 5, 0, 5),
                    Background = Brushes.LightBlue,
                    TextAlignment = TextAlignment.Center,
                    FontWeight = FontWeights.Bold,
                    FontSize = 15,

                };
                //textBlock.MouseDown += (sender, e) => { menuMulticentros.eventos(listaAlarmas[currentIndex].evento); };// para la navegacion de pagina a R_evento
                textBlock.MouseDown += (sender, e) =>
                {
                    if (listaAlarmas[currentIndex].estado != 2)
                    {
                        menuMulticentros.eventos(listaAlarmas[currentIndex]);// para la navegacion de pagina a R_evento
                    }

                    //M_EventoM eveb = new M_EventoM();
                    //eveb.idAlarma(listaAlarmas[currentIndex].id_alarma);
                };

                textBlock.MouseEnter += (s, e) => { textBlock.Foreground = Brushes.White; };

                textBlock.MouseLeave += (s, e) => { textBlock.Foreground = Brushes.Black; };

                Button btn = new Button
                {
                    Content = "Actualizar",
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 5, 0, 5),
                    IsEnabled = listaAlarmas[currentIndex].estado != 2 ? true : false,
                };
                btn.Click += (sender, e) =>
                {
                    C_AlarmaM alarma = new C_AlarmaM(2, listaAlarmas[currentIndex]);
                    bool? resp = alarma.ShowDialog();
                    if (resp == true)
                    {
                        l_alarmaM();
                    }

                    //MessageBox.Show(listaAlarmas[currentIndex].evento);
                    //menuMulticentros.eventos(listaAlarmas[currentIndex].evento);
                };
                Button btn2 = new Button
                {
                    Content = "Reporte",
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 5, 20, 5),
                    IsEnabled = listaAlarmas[currentIndex].estado != 1 ? true : false,
                };
                btn2.Click += (sender, e) =>
                {
                    Ctr_ReporteAM report = new Ctr_ReporteAM();
                    List<M_ReporteAM> lista = report.getlistaAM(listaAlarmas[currentIndex].id_alarma); //obtener datos




                    var jsonData = lista[0].data;
                    //List<M_Modelo> reportes = JsonSerializer.Deserialize<List<M_Modelo>>(jsonData);//deserealizado
                    List<M_EventoM> reportes = JsonSerializer.Deserialize<List<M_EventoM>>(jsonData);//deserealizado
                                                                                                     //MessageBox.Show(reportes.ToString());

                    SaveFileDialog save = new SaveFileDialog
                    {
                        FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf",
                        Filter = "PDF Files (*.pdf)|*.pdf",
                        Title = "Guardar Reporte PDF"
                    };
                    string paginahtml = Properties.Resources.reporte_eventosM.ToString();
                    string filas = string.Empty;
                    StringBuilder tabla = new StringBuilder();
                    int index = 1;
                    foreach (var dat in reportes)
                    {
                        // Cast al tipo adecuado; ajusta según el tipo real de los datos en Items
                        //var rowView = dat as DataRowView; // Cambia 'MiTipoDeObjeto' al tipo real
                        //var dato = JsonSerializer.Deserialize<M_ReporteAM>(dat);

                        filas += "<tr>";

                        filas += $"<td>{index}</td>";
                        filas += $"<td>{dat.nombre}</td>";
                        filas += $"<td>{dat.cuenta}</td>";
                        filas += $"<td>{dat.hora_e}</td>";
                        filas += $"<td>{dat.fecha_e}</td>";
                        filas += $"<td>{dat.departamento}</td>";
                        filas += $"<td>{dat.tipo}</td>";
                        filas += $"<td>{dat.name}</td>";
                        filas += $"<td>{dat.observaciones}</td>";
                        filas += "</tr>";
                        index++;
                    }
                    paginahtml = paginahtml.Replace("@FILAS", filas);
                    VistaPrevia vistaPrevia = new VistaPrevia(paginahtml);
                    if (vistaPrevia.ShowDialog() == true)
                    {

                        bool? result = save.ShowDialog();
                        if (result == true)
                        {
                            using (FileStream stream = new FileStream(save.FileName, FileMode.Create, FileAccess.Write))
                            {
                                using (Document pagina = new Document(PageSize.A4, 25, 25, 25, 25))
                                {

                                    PdfWriter doc = PdfWriter.GetInstance(pagina, stream);

                                    pagina.Open();
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.logo_entel,System.Drawing.Imaging.ImageFormat.Png);
                                    img.ScaleToFit(80, 80);
                                    //img.SetAbsolutePosition((pagina.PageSize.Width - img.ScaledWidth) / 2, (pagina.PageSize.Height - img.ScaledHeight) / 2); // Centrar en la página
                                    img.SetAbsolutePosition(pagina.LeftMargin, pagina.Top-60); // Centrar en la página

                                    pagina.Add(img);
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
                };
                Grid.SetColumn(textBlock, 0);
                Grid.SetColumn(btn, 1);
                Grid.SetColumn(btn2, 2);
                grid.Children.Add(textBlock);
                grid.Children.Add(btn);
                grid.Children.Add(btn2);
                stack1.Children.Add(grid);
            }
        }
    }
}
