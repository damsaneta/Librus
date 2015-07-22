using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumPrzedmiotowWPamieci : Librus.DostepDoDanych.IRepozytoriumPrzedmiotow
    {
        private static readonly Dictionary<string, Przedmiot> Przedmioty = new Dictionary<string, Przedmiot>();

        public void Dodaj(Przedmiot przedmiot)
        {       
            Przedmioty.Add(przedmiot.Id,przedmiot);
        }
        public IList<Przedmiot> PobierzWszystkie()
        {
            return Przedmioty.Values.ToList();
        }
        public Przedmiot ZnajdzPrzedmiot(string id)
        {
            var lista = Przedmioty.Values.Where(k => k.Id == id);
            return lista.First();
        }
    }
}
