using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Rola
    {
        public static readonly Rola Nieznany = new Rola(TypRoli.Nieznany, "Nieznany");
        public static readonly Rola Administrator = new Rola(TypRoli.Administrator, "Administrator");
        public static readonly Rola Nauczyciel = new Rola(TypRoli.Nauczyciel, "Nauczyciel");
        public static readonly Rola Rodzic = new Rola(TypRoli.Rodzic, "Rodzic");
        public static readonly Rola Uczen = new Rola(TypRoli.Uczen, "Uczeń");

        private Rola(TypRoli typ , string nazwa)
        {
            this.Typ = typ;
            this.Nazwa = nazwa;
        }

        public TypRoli Typ { get; private set; }

        public string Nazwa { get; private set; }

        public override string ToString()
        {
            return this.Nazwa;
        }
    
    }
}
