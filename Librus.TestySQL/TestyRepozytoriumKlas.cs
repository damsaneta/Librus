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
            var repozytorium = new RepozytoriumKlasB(connectionString);
            repozytorium.Dodaj(new Klasa("9z", "klasa testowa"));


        }
    }
}
