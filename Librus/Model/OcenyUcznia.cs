using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class OcenyUcznia
    {

        public OcenyUcznia(Uczen uczen, Przedmiot przedmiot)
        {
            this.Uczen = uczen;
            this.Przedmiot = przedmiot;
        }

        public Uczen Uczen { get; private set; }

        public Przedmiot Przedmiot { get; private set; }

        public string Oceny { get; set; }
    }
}
