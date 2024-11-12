using Cecom.Controladores.MulticentroCtr;
using Cecom.Modelos.MulticentroMod;
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

namespace Cecom.Vista.Multicentros.AlarmasM
{
    /// <summary>
    /// Lógica de interacción para C_AlarmaM.xaml
    /// </summary>
    public partial class C_AlarmaM : Window
    {
        M_AlarmaM alarmaEdit;
        int numero;
        public C_AlarmaM(int num, M_AlarmaM data)
        {
            InitializeComponent();
            this.numero = num;
            this.alarmaEdit = data;
            setdata();
        }

        public void setdata()
        {
            if (alarmaEdit != null)
            {
                amtxt_evento.Text = alarmaEdit.evento.ToString();
                //amtxt_evento.Text = alarmaEdit.id_alarma.ToString();
                amdt_fecha.Text = alarmaEdit.fecha.ToString();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (numero == 1)
            {
                M_AlarmaM alarmaM = new M_AlarmaM();
                alarmaM.evento = amtxt_evento.Text.Trim();
                alarmaM.fecha = amdt_fecha.Text;
                alarmaM.estado = 1;
                Ctr_AlarmaM ctr_AlarmaM = new Ctr_AlarmaM();
                bool resp = ctr_AlarmaM.save_alarmaM(alarmaM);
                if (resp)
                {
                    MessageBox.Show("registrado");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
            else
            {
                M_AlarmaM alarmaM = new M_AlarmaM();
                alarmaM.evento = amtxt_evento.Text.Trim();
                alarmaM.fecha = amdt_fecha.Text;
                alarmaM.id_alarma = alarmaEdit.id_alarma;
                Ctr_AlarmaM ctr_AlarmaM = new Ctr_AlarmaM();
                bool resp = ctr_AlarmaM.edit_alarmaM(alarmaM);
                if (resp)
                {
                    MessageBox.Show("Actualizado");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
        }
    }
}
