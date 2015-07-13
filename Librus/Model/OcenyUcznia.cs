using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public class OcenyUcznia
    {
        public Uczen Uczen { get; set; }
        public Przedmiot Przedmiot { get; set; }
        public string Oceny { get; set; }
        public OcenyUcznia(Uczen uczen, Przedmiot przedmiot)
        {
            this.Uczen = uczen;
            this.Przedmiot = przedmiot;
        }

    }
}
