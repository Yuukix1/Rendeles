using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RendelesAlkalmazas
{
    class Program
    {
        static void Main(string[] args)
        {
            // 5. Az ételek árlistájának betöltése (etelek.txt)
            var etelArak = EtelTar.Betolt("etelek.txt");

            // 1-2-3. A rendelések beolvasása az input.txt-ből, objektumok létrehozása, listába töltés
            var rendelesek = RendelesBeolvaso.Betolt("input.txt");

            Console.WriteLine($"Beolvasott rendelések száma: {rendelesek.Count}");
            Console.WriteLine();

            // Új rendelés beolvasása a konzolról
            UjRendelesFelvetele(rendelesek, etelArak);

            // 4. A rendelések kiírása két külön fájlba
            RendelesekMentese(rendelesek, "helyben.txt", "elvitel.txt");

            Console.WriteLine();
            Console.WriteLine("A rendelések elmentve: helyben.txt és elvitel.txt");

            Console.WriteLine();
            Console.WriteLine("Nyomj meg egy billentyűt a kilépéshez...");
            Console.ReadKey();
        }

        // Új rendelés bekérése a felhasználótól a konzolon keresztül
        static void UjRendelesFelvetele(List<Rendeles> rendelesek, Dictionary<string, double> etelArak)
        {
            Console.WriteLine("=== Új rendelés felvétele ===");

            Console.Write("Rendelés típusa (H = helyben, E = elvitel): ");
            string tipus = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            Console.Write("Ételek megadása vesszővel elválasztva (pl. Pizza,Kóla): ");
            string etelekBemenet = Console.ReadLine() ?? "";
            List<string> etelek = etelekBemenet.Split(',')
                                                .Select(e => e.Trim())
                                                .Where(e => e.Length > 0)
                                                .ToList();

            // Ár kiszámítása az etelek.txt alapján
            double osszAr = 0;
            foreach (var etel in etelek)
            {
                if (etelArak.TryGetValue(etel, out double ar))
                {
                    osszAr += ar;
                }
                else
                {
                    Console.WriteLine($"Figyelmeztetés: '{etel}' nem található az etelek.txt-ben, 0 Ft-tal kerül rögzítésre.");
                }
            }

            int ujId = rendelesek.Count > 0 ? rendelesek.Max(r => r.RendelesId) + 1 : 1;

            if (tipus == "H")
            {
                Console.Write("Asztalszám: ");
                int asztalSzam = int.Parse((Console.ReadLine() ?? "0").Trim(), CultureInfo.InvariantCulture);

                var ujRendeles = new HelybenRendeles(ujId, etelek, osszAr, asztalSzam);
                rendelesek.Add(ujRendeles);

                Console.WriteLine();
                Console.WriteLine("Új rendelés rögzítve:");
                Console.WriteLine(ujRendeles.RendelesKiirasa());
            }
            else if (tipus == "E")
            {
                Console.Write("Cím: ");
                string cim = (Console.ReadLine() ?? "").Trim();

                Console.Write("Futár neve: ");
                string futarNeve = (Console.ReadLine() ?? "").Trim();

                var ujRendeles = new ElvitelreRendeles(ujId, etelek, osszAr, cim, futarNeve);
                rendelesek.Add(ujRendeles);

                Console.WriteLine();
                Console.WriteLine("Új rendelés rögzítve:");
                Console.WriteLine(ujRendeles.RendelesKiirasa());
            }
            else
            {
                Console.WriteLine("Ismeretlen típus, a rendelés nem került rögzítésre.");
            }
        }
        static void RendelesekMentese(List<Rendeles> rendelesek, string helybenFajl, string elvitelFajl)
        {
            var helybenSorok = rendelesek.OfType<HelybenRendeles>()
                                          .Select(r => r.RendelesKiirasa());

            var elvitelSorok = rendelesek.OfType<ElvitelreRendeles>()
                                          .Select(r => r.RendelesKiirasa());

            File.WriteAllLines(helybenFajl, helybenSorok);
            File.WriteAllLines(elvitelFajl, elvitelSorok);
        }
    }
}
