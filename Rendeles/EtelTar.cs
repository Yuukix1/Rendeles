using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RendelesAlkalmazas
{
    // Segédosztály az etelek.txt beolvasásához (étel név -> ár)
    public static class EtelTar
    {
        public static Dictionary<string, double> Betolt(string fajlNev)
        {
            var etelArak = new Dictionary<string, double>();

            if (!File.Exists(fajlNev))
            {
                Console.WriteLine($"Figyelmeztetés: a(z) {fajlNev} fájl nem található.");
                return etelArak;
            }

            foreach (var sor in File.ReadAllLines(fajlNev))
            {
                if (string.IsNullOrWhiteSpace(sor)) continue;

                var reszek = sor.Split(',');
                if (reszek.Length < 2) continue;

                string nev = reszek[0].Trim();
                if (double.TryParse(reszek[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double ar))
                {
                    // Ha többször szerepel ugyanaz az étel, az utolsó árat tartjuk meg
                    etelArak[nev] = ar;
                }
            }

            return etelArak;
        }
    }
}
