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
        public Rodzic(string imie, string nazwisko, string mail, string haslo,List<Uzytkownik> lista) :
            base(imie, nazwisko, mail,TypRoli.Rodzic, haslo)
        {
            this.Dzieci = lista;
        }

        public List<Uzytkownik> Dzieci { get; set; }
    }
}
