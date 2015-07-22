using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;

namespace Librus.TestySQL
{
    [TestClass]
    public class TestyRepozytoriumPrzedmiotow
    {
        private const string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";
        [TestMethod]

        public void test1()
        {
            var repo = new RepozytoriumPrzedmiotow(connectionString);
            repo.Dodaj(new Przedmiot("rel","Religia"));
        }
    }
}
