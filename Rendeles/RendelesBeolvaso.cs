using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RendelesAlkalmazas
{
    // Segédosztály, amely az input.txt sorait Rendelés objektumokká alakítja
    public static class RendelesBeolvaso
    {
        public static List<Rendeles> Betolt(string fajlNev)
        {
            var rendelesek = new List<Rendeles>();

            if (!File.Exists(fajlNev))
            {
                Console.WriteLine($"Figyelmeztetés: a(z) {fajlNev} fájl nem található.");
                return rendelesek;
            }

            foreach (var sor in File.ReadAllLines(fajlNev))
            {
                if (string.IsNullOrWhiteSpace(sor)) continue;

                var reszek = sor.Split(';');
                if (reszek.Length < 5) continue; // hibás/hiányos sor kihagyása

                string tipus = reszek[0].Trim();
                int id = int.Parse(reszek[1].Trim(), CultureInfo.InvariantCulture);
                List<string> etelek = reszek[2].Split(',').Select(e => e.Trim()).ToList();
                double ar = double.Parse(reszek[3].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture);

                if (tipus == "H")
                {
                    int asztalSzam = int.Parse(reszek[4].Trim(), CultureInfo.InvariantCulture);
                    rendelesek.Add(new HelybenRendeles(id, etelek, ar, asztalSzam));
                }
                else if (tipus == "E")
                {
                    string cim = reszek[4].Trim();
                    string futarNeve = reszek.Length > 5 ? reszek[5].Trim() : string.Empty;
                    rendelesek.Add(new ElvitelreRendeles(id, etelek, ar, cim, futarNeve));
                }
                else
                {
                    Console.WriteLine($"Ismeretlen rendeléstípus a sorban: {sor}");
                }
            }

            return rendelesek;
        }
    }
}
