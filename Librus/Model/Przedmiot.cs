using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class Przedmiot
    {
        public Przedmiot(TypPrzedmiotu przedmiot)
        {
            this.Typ = przedmiot;
            this.Nazwa = przedmiot.ToString();
        }

        public TypPrzedmiotu Typ { get; private set; }

        public string Nazwa { get; private set; }

        public override string ToString()
        {
            return this.Nazwa;
        }
    }
}
