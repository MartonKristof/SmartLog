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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RaktarModel;
using System.IO;
using System.Collections.ObjectModel;

namespace RaktarGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Termek> Termekek = new ObservableCollection<Termek>();
        public MainWindow()
        {
            InitializeComponent();
            Adatbetoltes("termekek.txt");
            Kiiras();
        }

        #region Kiiras
        private void Kiiras()
        {
            dgTermekek.ItemsSource = Termekek;

            if (Termekek != null && Termekek.Any())
            {
                double maxAr = Termekek.Max(t => t.Egysegar);
                txtStatus.Text = $"Legnagyobb egységár: {maxAr:F2}";
            }
            else
            {
                txtStatus.Text = "Nincsenek beolvasott termékek";
            }
        }
        #endregion

        #region Adatbetoltes
        public static bool Adatbetoltes(string fajl)
        {
            try
            {
                var adatok = Termek.Beolvas(fajl);
                Termekek = new ObservableCollection<Termek>(adatok);
                return true;
            }
            catch (Exception ex)
            {
                Hibauzenet(ex);
                return false;
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

    }
}
