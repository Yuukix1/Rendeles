using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RendelesAlkalmazas
{
    // Rendelés (ősosztály)
    public class Rendeles
    {
        public int RendelesId { get; set; }
        public List<string> Etelek { get; set; }
        public double Ar { get; set; }

        public Rendeles()
        {
            RendelesId = 0;
            Etelek = new List<string>();
            Ar = 0;
        }

        public Rendeles(int rendelesId, List<string> etelek, double ar)
        {
            RendelesId = rendelesId;
            Etelek = etelek ?? new List<string>();
            Ar = ar;
        }

        // Étel hozzáadása a rendeléshez
        public void EtelHozzaadas(string etel)
        {
            Etelek.Add(etel);
        }

        public virtual string RendelesKiirasa()
        {
            string etelekSzovege = string.Join(", ", Etelek);
            return string.Format(CultureInfo.InvariantCulture,
                "RendelésID: {0}, Ételek: {1}, Ár: {2} Ft",
                RendelesId, etelekSzovege, Ar);
        }

        public override string ToString()
        {
            return RendelesKiirasa();
        }
    }
}
