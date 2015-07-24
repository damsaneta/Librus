using Librus.Model;
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
        public IList<ObecnoscUcznia> PobierzPoKlasieIDacie(string klasaId, DateTime data)
        {
            return obecnosciUcznia.Values.Where(x => ((x.Data.Year == data.Year && x.Data.Month==data.Month && x.Data.Day == data.Day) && (x.Uczen.Klasa.Id == klasaId))).ToList();
        }
        public IList<ObecnoscUcznia> PobierzObecnosciPoUczniu(Uczen uczen)
        {
            return obecnosciUcznia.Values.Where(x => x.Uczen.Email == uczen.Email).ToList();
        }
        public IList<ObecnoscUcznia> PobierzObecnoscPoUczniuIDacie(Uczen uczen, DateTime data)
        {
            return null;
        }
        public void Zapisz(IList<ObecnoscUcznia> obecnosci)
        {
            foreach(ObecnoscUcznia obe in obecnosci)
            {
                var c = obecnosciUcznia.Values.Where(x =>x.Uczen == obe.Uczen && x.Data==obe.Data).ToList();
               if(c==null || c.Count ==0)
               {
                   obecnosciUcznia.Add((obecnosciUcznia.Count+1),obe);
               }
            }
        }
    }
}
