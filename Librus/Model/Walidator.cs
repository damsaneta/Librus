using Librus.Resources;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Librus.Model
{
    public static class Walidator
    {
        static Walidator()
        {

        }

        public static bool WalidacjaPolaNazwy(TextBox txt, Label lbl)
        {
            foreach (char c in txt.Text)
            {
                if (!char.IsLetter(c))
                {
                    WyswietlBlad(txt, lbl, BledyWalidacji.KomunikatProszeWybracPoprawnaWartosc);
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
                WyswietlBlad(box, lbl, BledyWalidacji.KomunikatProszeWybrac);
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
            WyswietlBlad(txt, lbl, BledyWalidacji.KomunikatProszeWybracPoprawnaWartosc);
            return false;
        }

        public static bool WalidacjaWymaganegoPolaTekstowego(TextBox kontrolka, Label lbl)
        {
            if (string.IsNullOrEmpty(kontrolka.Text))
            {
                WyswietlBlad(kontrolka, lbl, BledyWalidacji.KomunikatPoleJestWymagane);
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
            if (kontrolka.SelectedValue == null)
            {
                WyswietlBlad(kontrolka, lbl, BledyWalidacji.KomunikatProszeWybrac);
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
            if (data.SelectedDate == null)
            {
                WyswietlBlad(data, lbl, BledyWalidacji.KomunikatWybierzDate);
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
                WyswietlBlad(passwordBox, label, BledyWalidacji.KomunikatPoleJestWymagane);
                return false;
            }
            else
            {
                UsunBlad(passwordBox, label);
                return true;
            }
        }
    }
}
