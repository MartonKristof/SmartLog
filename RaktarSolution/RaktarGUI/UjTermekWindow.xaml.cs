using RaktarModel;
using System;
using System.IO; 
using System.Windows;

namespace RaktarGUI
{
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

            try
            {
                string ujSor = $"{UjTermek.ID};{UjTermek.Megnevezes};{UjTermek.Kategoria};{UjTermek.Mennyiseg};{UjTermek.KeszletErtek}" + Environment.NewLine;

                File.AppendAllText("Termekek.txt", ujSor);

                MessageBox.Show("Termék sikeresen mentve a Termekek.txt fájlba!");
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba a mentés során: " + ex.Message);
            }
        }
    }
}