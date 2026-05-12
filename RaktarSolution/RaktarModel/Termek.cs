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

        #region Beolvas
        public static List<Termek> Beolvas(string fajl)
        {
            if (!File.Exists(fajl))
            {
                throw new FileNotFoundException("A fájl nem található", fajl);
            }

            return File.ReadLines(fajl, Encoding.UTF8)
                .Skip(1)
                .Select(sor =>
                {
                    var darabolt = sor.Split(';');
                    if (darabolt.Length != 5)
                    {
                        throw new FormatException($"Hibás sor: {sor}");
                    }
                    return new Termek(
                        int.Parse(darabolt[0]),
                        darabolt[1],
                        darabolt[2],
                        int.Parse(darabolt[3]),
                        double.Parse(darabolt[4].Replace(',', '.'))
                    );
                })
                .ToList();
        }
        #endregion
        #region AppentoFile
        public void AppendToFile(string fajl)
        {
            File.AppendAllText(fajl, Environment.NewLine + this.ToString(), Encoding.UTF8);
        }
        #endregion
    }
}
