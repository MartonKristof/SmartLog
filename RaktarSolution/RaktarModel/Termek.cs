using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RaktarModel
{
    public class Termek
    {
        public int ID { get; set; }
        public string Megnevezes { get; set; }
        public string Kategoria { get; set; }
        public int Mennyiseg { get; set; }
        public double Egysegar { get; set; }

        public Termek(int iD, string megnevezes, string kategoria, int mennyiseg, double egysegar)
        {
            ID = iD;
            Megnevezes = megnevezes;
            Kategoria = kategoria;
            Mennyiseg = mennyiseg;
            Egysegar = egysegar;
        }

        public override string ToString()
        {
            return $"{ID};{Megnevezes};{Kategoria};{Mennyiseg};{Egysegar}";
        }
    }
}
