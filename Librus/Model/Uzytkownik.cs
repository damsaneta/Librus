﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.Model
{
    public abstract class Uzytkownik
    {
        protected Uzytkownik()
        {

        }

        protected Uzytkownik(string imie, string nazwisko, string email, Rola rola, string haslo)
        {
            this.Imie = imie;
            this.Nazwisko = nazwisko;
            this.Email = email;
            this.Rola = rola;
            this.Haslo = haslo;
        }

        public string PelnaNazwa { get { return this.Imie + " " + this.Nazwisko; } }

        public string Email { get; private set; }

        public Rola Rola { get; private set; }

        public string Imie { get; private set; }

        public string Nazwisko { get; private set; }

        public int Id { get; set; }

        public string Haslo { get; private set; }

    }
}
