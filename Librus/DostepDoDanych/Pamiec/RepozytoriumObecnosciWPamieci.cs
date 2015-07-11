using Librus.Widoki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumObecnosciWPamieci: IRepozytoriumObecnosci
    {
        public static readonly Dictionary<int, ObecnoscUcznia> obecnosciUcznia = new Dictionary<int, ObecnoscUcznia>();
        public IList<ObecnoscUcznia> PobierzPoKlasieIDacie(string klasa, DateTime data)
        {
            return obecnosciUcznia.Values.Where(x => ((x.Data == data) && (x.Uczen.Klasa.Nazwa == klasa))).ToList();
        }

        public void Zapisz(IList<ObecnoscUcznia> obecnosci)
        {
            foreach(ObecnoscUcznia obe in obecnosci)
            {
                var c = obecnosciUcznia.Values.Where(x =>( (x.Uczen == obe.Uczen) && (x.Data==obe.Data))).ToList();
               if(c==null || c.Count ==0)
               {
                   obecnosciUcznia.Add((obecnosciUcznia.Count+1),obe);
               }
            }
        }
    }
}
