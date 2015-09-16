using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.DostepDoDanych.Pamiec;
using Librus.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Librus.Widoki.Administracja
{
    /// <summary>
    /// Interaction logic for DodawanieUzytkownika.xaml
    /// </summary>
    public partial class DodawanieUzytkownika : Window
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        private readonly IRepozytoriumKlas repozytoriumKlas;

        public DodawanieUzytkownika()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.repozytoriumKlas = new RepozytoriumKlas(connectionString);
            InitializeComponent();
            this.comboKlasa.ItemsSource = this.repozytoriumKlas.PobierzWszystkie();
            this.comboKlasa.DisplayMemberPath = "Nazwa";
            this.comboKlasa.SelectedValuePath = "Id";
        }

        private void DodajClick(object sender, RoutedEventArgs e)
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
                    string test = this.SprawdzanieIstnieniaDzieci(this.txtDziecko.Text);
                    result &= Walidator.WalidacjaDzieci(this.txtDziecko, this.errDziecko, test);
                    dzieci = repozytoriumUzytkownikow.WyszukiwanieDzieci(this.txtDziecko.Text);
                }
            }

            if (result == true)
            {
                Uzytkownik user = repozytoriumUzytkownikow.PobierzPoEmailu(this.txtEmail.Text);

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

                    this.repozytoriumUzytkownikow.Dodaj(uzytkownik);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    Walidator.WyswietlBlad(this.txtEmail, this.errEmail, "Uzytkownik o takim e-mailu juz istnieje!");
                }
            }
        }

        private void TextBoxImieTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtImie, this.errImie))
            {
                Walidator.WalidacjaPolaNazwy(this.txtImie, this.errImie);
            }
        }

        private void TextBoxNazwiskoTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtNazwisko, this.errNazwisko))
            {
                Walidator.WalidacjaPolaNazwy(this.txtNazwisko, this.errNazwisko);
            }
        }

        private void TextBoxEmailTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtEmail, this.errEmail))
            {
                Walidator.WalidacjaPolaEmail(this.txtEmail, this.errEmail);
            }
        }

        private void txtDzieckoTextChanged(object sender, TextChangedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtDziecko, this.errDziecko);
        }

        private void AnulujClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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

        private void ComboKlasaSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoComboBoxa(this.comboKlasa, errKlasa);
        }

        private string SprawdzanieIstnieniaDzieci(string slowo)
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
                Uzytkownik dziecko = repozytoriumUzytkownikow.WyszukajDziecka(imie, nazwisko);
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
