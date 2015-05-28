using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    class Rodzic:Uzytkownik
    {
        public Rodzic()
        {

        }
        public Rodzic(string imie, string nazwisko, string mail, TypRoli rola, string haslo,List<Uczen> lista) :
            base(imie, nazwisko, mail, rola, haslo)
        {
            this.Dzieci = lista;
        }

        public List<Uczen> Dzieci { get; set; }
    }
}
