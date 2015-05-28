using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    class Administrator:Uzytkownik
    {
        public Administrator()
        {

        }
        public Administrator(string imie, string nazwisko, string mail, TypRoli rola, string haslo):
            base(imie,nazwisko,mail,rola,haslo)
        {

        }
    }
}
