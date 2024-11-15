﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Cecom.Vista.Multicentros.AlarmasM
{
    /// <summary>
    /// Lógica de interacción para VistaPrevia.xaml
    /// </summary>
    public partial class VistaPrevia : Window
    {
        public VistaPrevia(string htmlContent)
        {
            InitializeComponent();
            webBrowser.NavigateToString(htmlContent);
        }
        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; // Esto puede ser opcional
            Close();
        }
    }
}
