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
            var repozytoriumKlas = new RepozytoriumKlas(connectionString);
            var repozytorium2 = new RepozytoriumOcenUcznia(connectionString);
            //var w = repozytoriumKlas.ZnajdzKlase("IA");
            // repozytorium.Dodaj(new Klasa("9z", "klasa testowa"));
            IList<Uzytkownik> dzieci = new List<Uzytkownik>();
            var u2 = repozytorium1.PobierzPoEmailu("mich@gmail.com");
             var u1 = repozytorium1.PobierzPoEmailu("justyna@gmail.com");
             dzieci.Add(u1);
             dzieci.Add(u2);
            repozytorium1.Dodaj(new Rodzic("Aneta", "Dams", "aneta@gmail.com", "AneDam",dzieci));
         //   repozytorium.Dodaj(new Uczen("Justyna", "Kowalska", "justyna@gmail.com", "JusKow",new Klasa("IA", "IA (mat - fiz)")));
           //repozytorium.Dodaj(new Uczen("Anna", "Kowalska", "das@gmail.com", "AnnKow", new Klasa("IA", "IA (mat - fiz)")));
           //repozytorium.Dodaj(new Uczen("Michalina", "Nowak", "mich@gmail.com", "MicNow",new Klasa("IA ","(mat - fiz)")));
          //  IList<ObecnoscUcznia> lista = new List<ObecnoscUcznia>();
           
         
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
           // var w = repozytorium.PobierzObecnoscPoUczniuIDacie(u2, DateTime.Now.Date);
            //ObecnoscUcznia o2 = new ObecnoscUcznia(u1, DateTime.Now.Date);
            //o2.Godzina8 = false;
            //o2.Godzina9 = false;
            //o2.Godzina10 = true;
            //o2.Godzina11 = false;
            //o2.Godzina12 = true;
            //o2.Godzina13 = true;
            //o2.Godzina14 = true;
            //o2.Godzina15 = true;
            
            //lista.Add(o2);

      

            
            //var przedmiot = new Przedmiot("fiz", "Fizyka");
            //IList<OcenyUcznia> oceny = new List<OcenyUcznia>();
            //OcenyUcznia oc1 = new OcenyUcznia(u1, przedmiot);
            //oc1.Oceny = "4, 5, 5, 5";
            //OcenyUcznia oc2 = new OcenyUcznia(u2, przedmiot);
            //oc2.Oceny = "5, 4, 3, 5, 5";
            //oceny.Add(oc1);
            //oceny.Add(oc2);

         
        }
    }
}
