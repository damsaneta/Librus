using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Librus.Widoki
{
    public class ObecnoscUcznia
    {
        public ObecnoscUcznia(Uczen uczen, DateTime data)
        {
            this.Uczen = uczen;
            this.Data = data;
        }

        public Uczen Uczen { get; private set; }

        public DateTime Data { get; private set; }

        public bool Godzina8 { get; set; }

        public bool Godzina9 { get; set; }

        public bool Godzina10 { get; set; }

        public bool Godzina11 { get; set; }

        public bool Godzina12 { get; set; }

        public bool Godzina13 { get; set; }

        public bool Godzina14 { get; set; }

        public bool Godzina15 { get; set; }

    }
}
