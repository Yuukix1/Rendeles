using System.Collections.Generic;

namespace RendelesAlkalmazas
{
    // ElvitelreRendelés (leszármazott osztály)
    public class ElvitelreRendeles : Rendeles
    {
        public string Cim { get; set; }
        public string FutarNeve { get; set; }

        public ElvitelreRendeles() : base()
        {
            Cim = string.Empty;
            FutarNeve = string.Empty;
        }

        public ElvitelreRendeles(int rendelesId, List<string> etelek, double ar, string cim, string futarNeve)
            : base(rendelesId, etelek, ar)
        {
            Cim = cim;
            FutarNeve = futarNeve;
        }

        public override string RendelesKiirasa()
        {
            return base.RendelesKiirasa() + $", Cím: {Cim}, Futár: {FutarNeve}";
        }
    }
}
