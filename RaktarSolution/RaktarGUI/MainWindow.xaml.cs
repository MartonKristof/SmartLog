using RaktarModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RaktarGUI
{
    public partial class MainWindow : Window
    {

        public ObservableCollection<Termek> MegjelenitettTermekek { get; set; } = new ObservableCollection<Termek>();
        private List<Termek> MindenTermek = new List<Termek>();

        public MainWindow()
        {
            InitializeComponent();

            if (Adatbetoltes("termekek.txt"))
            {
                dgTermekek.ItemsSource = MegjelenitettTermekek;
                Kiiras();
            }

            txtKereses.TextChanged += TxtKereses_TextChanged;
        }

        #region Adatbetoltes
        public bool Adatbetoltes(string fajl)
        {
            try
            {
                var adatok = Termek.Beolvas(fajl);
                MindenTermek = adatok.ToList();

                MegjelenitettTermekek.Clear();
                foreach (var t in MindenTermek)
                {
                    MegjelenitettTermekek.Add(t);
                }
                return true;
            }
            catch (Exception ex)
            {
                Hibauzenet(ex);
                return false;
            }
        }
        #endregion
        #region Keresés és Szűrés (A lényeg)
        private void ApplyFilter()
        {
            string keresendoText = txtKereses.Text?.Trim().ToLower();

            MegjelenitettTermekek.Clear();


            if (string.IsNullOrEmpty(keresendoText))
            {
                foreach (var t in MindenTermek)
                {
                    MegjelenitettTermekek.Add(t);
                }
            }
            else
            {

                var szurtLista = MindenTermek.Where(t =>
                    t.Megnevezes.ToLower().Contains(keresendoText) ||
                    t.Kategoria.ToLower().Contains(keresendoText)
                ).ToList();


                foreach (var t in szurtLista)
                {
                    MegjelenitettTermekek.Add(t);
                }
            }

            Kiiras();
        }
        private void TxtKereses_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }
        #endregion
        #region Segédmetódusok
        private void Kiiras()
        {
            if (MegjelenitettTermekek.Any())
            {
                double maxAr = MegjelenitettTermekek.Max(t => t.Egysegar);
                txtStatus.Text = $"Találatok: {MegjelenitettTermekek.Count} db | Legdrágább: {maxAr:N0} Ft";
            }
            else
            {
                txtStatus.Text = "Nincs találat a keresett kifejezésre.";
            }
        }
        #endregion
        #region Hibauzenet
        public static void Hibauzenet(Exception ex)
        {
            string uzenet;
            switch (ex)
            {
                case FileNotFoundException fnf:
                    uzenet = $"A megadott fájl nem található: {fnf.FileName}";
                    break;
                case FormatException fe:
                    uzenet = $"Formátum hiba: {fe.Message}";
                    break;
                default:
                    uzenet = $"Ismeretlen hiba történt: {ex.Message}";
                    break;
            }
            MessageBox.Show(uzenet, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        #endregion

        private void btnStatisztika_Click(object sender, RoutedEventArgs e)
        {
            WindowStatistika ablak = new WindowStatistika(MegjelenitettTermekek);
            ablak.ShowDialog();
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            UjTermekWindow ablak = new UjTermekWindow();
            if (ablak.ShowDialog() == true)
            {
                Adatbetoltes("Termekek.txt");
            }
        }
    }
}