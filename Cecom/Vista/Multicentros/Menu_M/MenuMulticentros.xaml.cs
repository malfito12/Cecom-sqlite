using Cecom.Modelos.MulticentroMod;
using Cecom.Vista.Multicentros.EventosM;
using Cecom.Vista.Multicentros.ReportesM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Cecom.Multicentros
{
    public partial class MenuMulticentros : Page
    {
        public MenuMulticentros()
        {
            InitializeComponent();
            RegistroAlarma registroAlarma = new RegistroAlarma(this);
            FrameMulticentros.Navigate(registroAlarma);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            R_Multicentros r_Multicentros = new R_Multicentros();
            FrameMulticentros.Navigate(r_Multicentros);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            RegistroAlarma registroAlarma = new RegistroAlarma(this);
            FrameMulticentros.Navigate(registroAlarma);
        }

        public void eventos(M_AlarmaM data)
        {
            R_Eventos r_eventos= new R_Eventos(data,this);
            FrameMulticentros.Navigate(r_eventos);
            r_eventos.labelTitulo.Content = data.evento;
            r_eventos.idEvento.Content = data.id_alarma;
            //Label label = new Label { Content = data.evento, HorizontalAlignment= HorizontalAlignment.Center, FontWeight = FontWeights.Bold, FontSize =15 };
            //r_eventos.listaEventos.Children.Add(label);
        }

        public void PagReportesM()
        {
            R_ReportesM reportesM = new R_ReportesM();
            FrameMulticentros.Navigate (reportesM);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            R_ReportesM reportesM = new R_ReportesM();
            FrameMulticentros.Navigate (reportesM);

        }
    }
}
