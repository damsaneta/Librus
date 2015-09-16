﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;

namespace Librus.TestySQL
{
    [TestClass]
    public class TestyRepozytoriumPrzedmiotow
    {
        private const string connectionString = @"Data Source=(LocalDB)\v12.0;AttachDbFilename=D:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";
        [TestMethod]

        public void test1()
        {
            var repo = new RepozytoriumPrzedmiotow(connectionString);
            var lista = repo.ZnajdzPrzedmiot("jpol");
        }
    }
}
