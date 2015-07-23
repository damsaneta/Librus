using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Rodzic:Uzytkownik
    {
        public Rodzic()
        {

        }
        public Rodzic(string imie, string nazwisko, string mail, string haslo,IList<Uzytkownik> lista) :
            base(imie, nazwisko, mail,Rola.Rodzic, haslo)
        {
            this.Dzieci = lista;
        }

        public IList<Uzytkownik> Dzieci { get; set; }
    }
}
