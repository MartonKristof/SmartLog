using RaktarModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace RaktarGUI
{
    public partial class WindowStatistika : Window
    {
        private List<Termek> termekek;

        public WindowStatistika(ObservableCollection<Termek> lista)
        {
            InitializeComponent();

            termekek = lista.ToList();

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

            double teljesErtek = termekek.Sum(t => t.Mennyiseg * t.Egysegar);
            txtKeszletErtek.Text = teljesErtek.ToString("N0") + " Ft";

            
            txtAlacsonyKeszlet.Text = termekek
                .Count(t => t.Mennyiseg <= 5)
                .ToString();

            
            dgTopTermekek.ItemsSource = termekek
                .OrderByDescending(t => t.Mennyiseg * t.Egysegar)
                .Take(5)
                .ToList();

            
            txtInfo1.Text = "• Utolsó frissítés: " + DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            txtInfo2.Text = "• Betöltött termékek: " + termekek.Count;
            txtInfo3.Text = "• Rendszer állapot: Stabil";
        }
    }
}