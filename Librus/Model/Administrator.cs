using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Administrator : Uzytkownik
    {
        public Administrator()
        {

        }

        public Administrator(string imie, string nazwisko, string mail, string haslo) :
            base(imie, nazwisko, mail, Rola.Administrator, haslo)
        {

        }
    }
}
