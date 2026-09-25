using System.Collections.Generic;

namespace RendelesAlkalmazas
{
    // HelybenRendelés (leszármazott osztály)
    public class HelybenRendeles : Rendeles
    {
        public int AsztalSzam { get; set; }

        public HelybenRendeles() : base()
        {
            AsztalSzam = 0;
        }

        public HelybenRendeles(int rendelesId, List<string> etelek, double ar, int asztalSzam)
            : base(rendelesId, etelek, ar)
        {
            AsztalSzam = asztalSzam;
        }

        public override string RendelesKiirasa()
        {
            return base.RendelesKiirasa() + $", Asztalszám: {AsztalSzam}";
        }
    }
}
