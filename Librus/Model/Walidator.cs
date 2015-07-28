using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.DostepDoDanych.Pamiec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Librus.Model
{
    public static class Walidator
    {
        private const  string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";

        private static readonly IRepozytoriumUzytkownikow repozytorium = new RepozytoriumUzytkownikow(connectionString);
       
        public static bool WalidacjaPolaNazwy(TextBox txt, Label lbl)
        {
            foreach (char c in txt.Text)
            {
                if (char.IsLetter(c) == false)
                {
                    WyswietlBlad(txt, lbl, "Proszę podać poprawną wartość");
                    return false;
                }
            }
            UsunBlad(txt, lbl);
            return true;
        }

        public static bool WalidacjaComboBox(ComboBox box, Label lbl)
        {
            TypRoli typ = (TypRoli)box.SelectedIndex;
            if (typ == TypRoli.Nieznany)
            {
                WyswietlBlad(box, lbl, "Proszę wybrać.");
                return false;
            }
            UsunBlad(box, lbl);
            return true;
        }
        public static bool WalidacjaPolaEmail(TextBox txt, Label lbl)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txt.Text);
            if (match.Success)
            {
                UsunBlad(txt, lbl);
                return true;
            }
            WyswietlBlad(txt, lbl, "Prosze podać poprawną wartość");
            return false;
        }
        public static bool WalidacjaWymaganegoPolaTekstowego(TextBox kontrolka, Label lbl)
        {
            if (string.IsNullOrEmpty(kontrolka.Text))
            {
                WyswietlBlad(kontrolka, lbl, "Pole jest wymagane.");
                return false;
            }
            else
            {
                UsunBlad(kontrolka, lbl);
                return true;
            }
        }
        public static bool WalidacjaWymaganegoComboBoxa(ComboBox kontrolka, Label lbl)
        {
            if (kontrolka.SelectedValue==null)
            {
                WyswietlBlad(kontrolka, lbl, "Proszę wybrać.");
                return false;
            }
            else
            {
                UsunBlad(kontrolka, lbl);
                return true;
            }
        }
        public static bool WalidacjaWymaganegoPolaDaty(DatePicker data, Label lbl)
        {
            if(data.SelectedDate==null)
            {
                WyswietlBlad(data, lbl, "Wybierz datę");
                return false;
            }
            else
            {
                UsunBlad(data, lbl);
                return true;
            }
        }
        //public static bool WalidacjaIstnieniaUzytkownika(TextBox textBox, Label label)
        //{
        //    if(repozytorium.PobierzPoEmailu(textBox.Text)!=null)
        //    {
        //        WyswietlBlad(textBox, label, "Użytkownik o takim e-mailu istnieje.");
        //        return false;
        //    }
        //    else
        //    {
        //        UsunBlad(textBox, label);
        //        return true;
        //    }
        //}
        public static void WyswietlBlad(Control kontrolka, Label lbl, string komunikat)
        {
            kontrolka.Background = Brushes.Yellow;
            lbl.Visibility = Visibility.Visible;
            lbl.Content = komunikat;
        }
        public static void UsunBlad(Control kontrolka, Label lbl)
        {
            kontrolka.Background = Brushes.White;
            lbl.Visibility = Visibility.Hidden;
        }

        public static bool WalidacjaDzieci(TextBox textBox, Label label, string test)
        {
            if (String.IsNullOrEmpty(test))
            {
                UsunBlad(textBox, label);
                return true;
            }
            else
            {
                WyswietlBlad(textBox, label, test);
                return false;
            }


        }


        public static bool WalidacjaHasla(PasswordBox passwordBox, Label label)
        {
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                WyswietlBlad(passwordBox, label, "Pole jest wymagane.");
                return false;
            }
            else
            {
                UsunBlad(passwordBox, label);
                return true;
            }
        }


        public static string SprawdzanieIstnieniaDzieci(TextBox textBox)
        {
            string slowo = textBox.Text;
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
                Uzytkownik dziecko = repozytorium.WyszukajDziecka(imie, nazwisko);
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
