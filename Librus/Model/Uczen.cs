using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Uczen : Uzytkownik
    {
        public Uczen()
        {

        }
        public Uczen(string imie, string nazwisko, string mail, string haslo, string klasa) :
            base(imie, nazwisko, mail, TypRoli.Uczen, haslo)
        {
            this.Klasa = klasa;
        }
        public string Klasa { get; set; }

    }
}
