using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;
using System.Transactions;
using Librus.Widoki;
using System.Collections;
using System.Collections.Generic;

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
            //var lista = repozytorium.PobierzWszystkie();
            var klasa = repozytorium.ZnajdzKlase("IA");

        }
        [TestMethod]
        public void Test2()
        {
            var repozytorium1 = new RepozytoriumUzytkownikow(connectionString);
            var repozytorium = new RepozytoriumObecnosci(connectionString);
            // repozytorium.Dodaj(new Klasa("9z", "klasa testowa"));
           // repozytorium.Dodaj(new Rodzic("Aneta", "Dams", "aneta@gmail.com", "AneDam",null));
         //   repozytorium.Dodaj(new Uczen("Justyna", "Kowalska", "justyna@gmail.com", "JusKow",new Klasa("IA", "IA (mat - fiz)")));
           //repozytorium.Dodaj(new Uczen("Anna", "Kowalska", "das@gmail.com", "AnnKow", new Klasa("IA", "IA (mat - fiz)")));
           //repozytorium.Dodaj(new Uczen("Michalina", "Nowak", "mich@gmail.com", "MicNow",new Klasa("IA ","(mat - fiz)")));
            //IList<ObecnoscUcznia> lista = new List<ObecnoscUcznia>();
            var u2 = (Uczen)repozytorium1.PobierzPoEmailu("das@gmail.com");
            //var u1 =(Uczen)repozytorium1.PobierzPoEmailu("justyna@gmail.com");
            //ObecnoscUcznia o1 = new ObecnoscUcznia(u2, DateTime.Now.Date);
            //o1.Godzina8 = true;
            //o1.Godzina9 = true;
            //o1.Godzina10 = true;
            //o1.Godzina11 = true;
            //o1.Godzina12 = true;
            //o1.Godzina13 = true;
            //o1.Godzina14 = true;
            //o1.Godzina15 = true;
            //lista.Add(o1);
            //repozytorium.Zapisz(lista);
            var wynik = repozytorium.PobierzObecnoscPoUczniuIDacie(u2, DateTime.Now.Date);
        }
    }
}
