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
        private static readonly Dictionary<int, Klasa> Klasy = new Dictionary<int, Klasa>();

        private static int ostatnieID = 0;
        public void Dodaj(Klasa klasa)
        {
            ostatnieID++;
            klasa.Id = ostatnieID;
            Klasy.Add(ostatnieID, klasa);
        }
        public IList<Klasa> PobierzWszystkie()
        {
            return Klasy.Values.ToList();
        }
        public Klasa ZnajdzKlase(int id)
        {
            var lista =  Klasy.Values.Where(k => (k.Id.ToString().StartsWith(id.ToString()))).ToList();
            return lista.First();
        }
    }
}
