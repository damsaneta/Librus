using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    class Nauczyciel : Uzytkownik
    {
        public Nauczyciel()
        {

        }
        public Nauczyciel(string imie, string nazwisko, string mail, string haslo) :
            base(imie, nazwisko, mail, Rola.Nauczyciel, haslo)
        {

        }
    }
}
