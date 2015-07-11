using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumObecnosciWPamieci: IRepozytoriumObecnosci
    {
        public IList<Widoki.ObecnoscUcznia> PobierzPoKlasieIDacie(string klasa, DateTime data)
        {
            return null;
        }

        public void Zapisz(IList<Widoki.ObecnoscUcznia> obecnosci)
        {
            throw new NotImplementedException();
        }
    }
}
