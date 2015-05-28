using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Uczen:Uzytkownik
    {
        public Uczen()
        {

        }
        public Uczen(string imie, string nazwisko,string mail, TypRoli rola ,string haslo,string klasa):
            base(imie,nazwisko,mail,rola ,haslo)
        {
            this.Klasa = klasa;
        }
        public string Klasa { get; set; }

    }
}
