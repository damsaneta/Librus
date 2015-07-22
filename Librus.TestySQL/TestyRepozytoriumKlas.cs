using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;
using System.Transactions;

namespace Librus.TestySQL
{
    [TestClass]
    public class TestyRepozytoriumKlas
    {
        private const string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";

        [TestMethod]
        public void Test()
        {
            var repozytorium = new RepozytoriumKlas(connectionString);
           // repozytorium.Dodaj(new Klasa("9z", "klasa testowa"));
            var lista = repozytorium.PobierzWszystkie();

        }
        [TestMethod]
        public void Test2()
        {
            var repozytorium = new RepozytoriumUzytkownikow(connectionString);
            // repozytorium.Dodaj(new Klasa("9z", "klasa testowa"));
             repozytorium.Dodaj(new Uczen("Magda","Dams","gmail@damdfds.com","MagDam",new Klasa("IA","")));
     
        }
    }
}
