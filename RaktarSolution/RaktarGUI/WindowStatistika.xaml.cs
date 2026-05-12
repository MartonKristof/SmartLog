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
    public partial class WindowStatistika : Window
    {
        private List<Termek> termekek;

        public WindowStatistika(List<Termek> lista)
        {
            InitializeComponent();

            termekek = lista;

            BetoltStatisztika();
        }

        private void BetoltStatisztika()
        {
            txtOsszesTermek.Text = termekek.Count.ToString();

            txtKategoriak.Text = termekek
                .Select(t => t.Kategoria)
                .Distinct()
                .Count()
                .ToString();

            int teljesErtek = (int)termekek.Sum(t => t.KeszletErtek);

            txtKeszletErtek.Text = teljesErtek.ToString("N0") + " Ft";

            txtAlacsonyKeszlet.Text = termekek
                .Count(t => t.Mennyiseg < 5)
                .ToString();

            dgTopTermekek.ItemsSource = termekek
                .OrderByDescending(t => t.KeszletErtek)
                .Take(5)
                .ToList();

            txtInfo1.Text = "• Utolsó frissítés: " +
                            DateTime.Now.ToString("yyyy.MM.dd HH:mm");

            txtInfo2.Text = "• Betöltött termékek: " +
                            termekek.Count;

            txtInfo3.Text = "• Rendszer állapot: Stabil";
        }
    }
}


