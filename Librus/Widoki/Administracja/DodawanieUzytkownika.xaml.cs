﻿using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Librus.Widoki.Administracja
{
    public partial class DodawanieUzytkownika : Window
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        private readonly IRepozytoriumKlas repozytoriumKlas;

        public DodawanieUzytkownika()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.repozytoriumKlas = new RepozytoriumKlas(connectionString);
            this.InitializeComponent();
            this.Inicjalizuj();
        }

        private async void Inicjalizuj()
        {
            this.comboKlasa.ItemsSource = await this.repozytoriumKlas.PobierzWszystkie();
            this.comboKlasa.DisplayMemberPath = "Nazwa";
            this.comboKlasa.SelectedValuePath = "Id";
        }

        /// <summary>
        /// DDodawanie nowego użytkownika.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private async void DodajClick(object sender, RoutedEventArgs e)
        {
            bool result = true;
            IList<Uzytkownik> dzieci = new List<Uzytkownik>();
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtImie, this.errImie);
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtNazwisko, this.errNazwisko);
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtEmail, this.errEmail);

            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtImie, this.errImie))
            {
                result &= Walidator.WalidacjaPolaNazwy(txtImie, this.errImie);
            }
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtNazwisko, this.errNazwisko))
            {
                result &= Walidator.WalidacjaPolaNazwy(this.txtNazwisko, this.errNazwisko);
            }
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtEmail, this.errEmail))
            {
                result &= Walidator.WalidacjaPolaEmail(this.txtEmail, this.errEmail);
            }

            result &= Walidator.WalidacjaComboBox(this.comboRola, this.errRola);

            if (TypRoli.Uczen == (TypRoli)this.comboRola.SelectedIndex)
            {
                result &= Walidator.WalidacjaWymaganegoComboBoxa(this.comboKlasa, errKlasa);
            }
            if (TypRoli.Rodzic == (TypRoli)this.comboRola.SelectedIndex)
            {
                if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtDziecko, this.errDziecko))
                {
                    string test = await this.SprawdzanieIstnieniaDzieci(this.txtDziecko.Text);
                    result &= Walidator.WalidacjaDzieci(this.txtDziecko, this.errDziecko, test);
                    dzieci = await repozytoriumUzytkownikow.WyszukiwanieDzieci(this.txtDziecko.Text);
                }
            }

            if (result == true)
            {
                Uzytkownik user = await repozytoriumUzytkownikow.PobierzPoEmailu(this.txtEmail.Text);

                if (user == null)
                {
                    Walidator.UsunBlad(this.txtEmail, this.errEmail);
                    TypRoli typ = (TypRoli)this.comboRola.SelectedIndex;
                    string haslo = this.txtImie.Text.Substring(0, 3) + this.txtNazwisko.Text.Substring(0, 3);
                    string imie = this.txtImie.Text;
                    string nazwisko = this.txtNazwisko.Text;
                    string email = this.txtEmail.Text;
                    Uzytkownik uzytkownik = null;
                    switch (typ)
                    {
                        case TypRoli.Administrator:
                            uzytkownik = new Administrator(imie, nazwisko, email, haslo);
                            break;
                        case TypRoli.Nauczyciel:
                            uzytkownik = new Nauczyciel(imie, nazwisko, email, haslo);
                            break;
                        case TypRoli.Rodzic:
                            uzytkownik = new Rodzic(imie, nazwisko, email, haslo, dzieci);
                            break;
                        case TypRoli.Uczen:
                            var klasa = this.comboKlasa.SelectedItem as Klasa;
                            uzytkownik = new Uczen(imie, nazwisko, email, haslo, klasa);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }

                    await this.repozytoriumUzytkownikow.Dodaj(uzytkownik);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    Walidator.WyswietlBlad(this.txtEmail, this.errEmail, "Uzytkownik o takim e-mailu juz istnieje!");
                }
            }
        }

        /// <summary>
        /// Walidcja imienia.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void TextBoxImieTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtImie, this.errImie))
            {
                Walidator.WalidacjaPolaNazwy(this.txtImie, this.errImie);
            }
        }

        /// <summary>
        /// Walidacja nazwiska.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void TextBoxNazwiskoTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtNazwisko, this.errNazwisko))
            {
                Walidator.WalidacjaPolaNazwy(this.txtNazwisko, this.errNazwisko);
            }
        }

        /// <summary>
        /// Walidacja emaila.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void TextBoxEmailTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtEmail, this.errEmail))
            {
                Walidator.WalidacjaPolaEmail(this.txtEmail, this.errEmail);
            }
        }

        /// <summary>
        /// Walidacja pola dzieci.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void txtDzieckoTextChanged(object sender, TextChangedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtDziecko, this.errDziecko);
        }

        private void AnulujClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Aktywowanie odpowiednich pól w zależności od wybranej roli.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboRolaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool result = true;
            try
            {
                result = Walidator.WalidacjaComboBox(this.comboRola, this.errRola);
                if (result == true)
                {
                    TypRoli typ = (TypRoli)this.comboRola.SelectedIndex;
                    this.comboKlasa.IsEnabled = (typ == TypRoli.Uczen);
                    this.txtDziecko.IsEnabled = (typ == TypRoli.Rodzic);
                }

            }
            catch
            {

            }
        }

        /// <summary>
        /// Walidacja ComboBoxa.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboKlasaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoComboBoxa(this.comboKlasa, errKlasa);
        }

        /// <summary>
        /// Sprawdzanie czy wybrane dzieci istnieją w bazie. Wykorzystywana przy tworzeniu rodzica
        /// </summary>
        /// <param name="slowo">The slowo.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        private async Task<string> SprawdzanieIstnieniaDzieci(string slowo)
        {
            string brakDzieci = string.Empty;
            string[] tab = slowo.Split(new char[] { ',' });
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = tab[i].Trim();
            }
            foreach (string s in tab)
            {
                string[] wynik = s.Split(new char[] { ' ' });
                string imie = wynik[0];
                string nazwisko = wynik[1];
                Uzytkownik dziecko = await repozytoriumUzytkownikow.WyszukajDziecka(imie, nazwisko);
                if (dziecko == null)
                {
                    if (string.IsNullOrEmpty(brakDzieci))
                    {
                        brakDzieci = "Brak ucznia: " + imie + " " + nazwisko;
                    }
                    else
                    {
                        brakDzieci = ", " + imie + " " + nazwisko;
                    }

                }

            }
            return brakDzieci;
        }
    }
}
