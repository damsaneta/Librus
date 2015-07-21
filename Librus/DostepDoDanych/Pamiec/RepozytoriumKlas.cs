using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.Pamiec
{
    public class RepozytoriumKlas : Librus.DostepDoDanych.IRepozytoriumKlas
    {
        private static readonly Dictionary<string, Klasa> Klasy = new Dictionary<string, Klasa>();

        public void Dodaj(Klasa klasa)
        {
            Klasy.Add(klasa.Id, klasa);
        }
        public IList<Klasa> PobierzWszystkie()
        {
            return Klasy.Values.ToList();
        }
        public Klasa ZnajdzKlase(string id)
        {
            var lista =  Klasy.Values.Where(k => (k.Id.StartsWith(id))).ToList();
            return lista.First();
        }
    }
}
