using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumPrzedmiotow : Librus.DostepDoDanych.IRepozytoriumPrzedmiotow
    {
        private static readonly Dictionary<int, Przedmiot> Przedmioty = new Dictionary<int, Przedmiot>();

        private static int ostatnieID = 0;
        public void Dodaj(Przedmiot przedmiot)
        {
            ostatnieID++;
            Przedmioty.Add(ostatnieID,przedmiot);
        }
        public IList<Przedmiot> PobierzWszystkie()
        {
            return Przedmioty.Values.ToList();
        }
        public Przedmiot ZnajdzPrzedmiot(string nazwa)
        {
            var lista = Przedmioty.Values.Where(k => k.Nazwa == nazwa);
            return lista.First();
        }
    }
}
