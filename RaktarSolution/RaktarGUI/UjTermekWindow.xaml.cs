using RaktarModel;
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

namespace RaktarGUI
{
    // valami 
    public partial class UjTermekWindow : Window
    {
        public Termek UjTermek;
        public UjTermekWindow()
        {
            InitializeComponent();
        }

        private void Button_Mentes_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbID.Text) ||
                string.IsNullOrWhiteSpace(tbNev.Text) ||
                string.IsNullOrWhiteSpace(tbKategoria.Text) ||
                string.IsNullOrWhiteSpace(tbAr.Text) ||
                string.IsNullOrWhiteSpace(tbMennyiseg.Text))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!");
                return;
            }

            if (!int.TryParse(tbID.Text, out int id))
            {
                MessageBox.Show("Az ID csak szám lehet!");
                return;
            }

            if (!double.TryParse(tbAr.Text, out double ar) || ar < 0)
            {
                MessageBox.Show("Hibás ár!");
                return;
            }

            if (!int.TryParse(tbMennyiseg.Text, out int mennyiseg) || mennyiseg < 0)
            {
                MessageBox.Show("Hibás mennyiség!");
                return;
            }

            UjTermek = new Termek(
                id,
                tbNev.Text,
                tbKategoria.Text,
                mennyiseg,
                ar
            );

            MessageBox.Show("Termék sikeresen hozzáadva!");

            Close();
        }
    }
}
