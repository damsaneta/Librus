using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumOcenUczniaWPamieci : Librus.DostepDoDanych.IRepozytoriumOcenUcznia 
    {
        public static readonly Dictionary<int, OcenyUcznia> ocenyUcznia = new Dictionary<int, OcenyUcznia>();

        public IList<OcenyUcznia> PobierzPoKlasieIPrzedmiocie(string klasaId, string przedmiotId)
        {
            return ocenyUcznia.Values.Where(x => x.Przedmiot.Id == przedmiotId && x.Uczen.Klasa.Id == klasaId).ToList();
        }
        public IList<OcenyUcznia> PobierzOcenyPoUczniu(Uczen uczen)
        {
            return ocenyUcznia.Values.Where(x => x.Uczen.Email == uczen.Email).ToList();
        }
        public void Zapisz(IList<OcenyUcznia> oceny)
        {
            foreach (OcenyUcznia o in oceny)
            {
                var c = ocenyUcznia.Values.Where(x => x.Uczen == o.Uczen && o.Przedmiot == x.Przedmiot).ToList();
                if (c == null || c.Count == 0)
                {
                    ocenyUcznia.Add((ocenyUcznia.Count + 1), o);
                }
            }
        }
    }
}
