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
        public Uczen(string imie, string nazwisko, string mail, string haslo, Klasa klasa) :
            base(imie, nazwisko, mail, Rola.Uczen, haslo)
        {
            this.Klasa = klasa;
        }
        public Klasa Klasa { get; set; }
    }
}
