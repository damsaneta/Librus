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

        /// <summary>
        /// Testowanie czy użytkownik wpisał tylko litery czy tez liczby w miejscach gdzie wymagane sa litery
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <param name="lbl">The label.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Walidacja czy została wybrana rola nowego użytkownika.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <param name="lbl">The label.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Walidacja podanego emaila przez użytkownika
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <param name="lbl">The label.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Walidacja czy użytkownik wypełnij wymagane pola.
        /// </summary>
        /// <param name="kontrolka">The kontrolka.</param>
        /// <param name="lbl">The label.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool WalidacjaWymaganegoPolaTekstowego(TextBox kontrolka, Label lbl)
        {
            if (string.IsNullOrEmpty(kontrolka.Text))
            {
                WyswietlBlad(kontrolka, lbl, BledyWalidacji.KomunikatPoleJestWymagane);
                return false;
            }
            UsunBlad(kontrolka, lbl);
            return true;
        }

        /// <summary>
        /// Testowanie czy użytkownik wybrał opcję w comboBoxie.
        /// </summary>
        /// <param name="kontrolka">The kontrolka.</param>
        /// <param name="lbl">The label.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool WalidacjaWymaganegoComboBoxa(ComboBox kontrolka, Label lbl)
        {
            if (kontrolka.SelectedValue == null)
            {
                WyswietlBlad(kontrolka, lbl, BledyWalidacji.KomunikatProszeWybrac);
                return false;
            }
            UsunBlad(kontrolka, lbl);
            return true;
        }

        /// <summary>
        /// Walidacja czy użytkownik wybrał datę.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="lbl">The label.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool WalidacjaWymaganegoPolaDaty(DatePicker data, Label lbl)
        {
            if (data.SelectedDate == null)
            {
                WyswietlBlad(data, lbl, BledyWalidacji.KomunikatWybierzDate);
                return false;
            }
            UsunBlad(data, lbl);
            return true;
        }

        /// <summary>
        /// Wyswietlanie komunikatu o błedzie.
        /// </summary>
        /// <param name="kontrolka">The kontrolka.</param>
        /// <param name="lbl">The label.</param>
        /// <param name="komunikat">The komunikat.</param>
        public static void WyswietlBlad(Control kontrolka, Label lbl, string komunikat)
        {
            kontrolka.Background = Brushes.Yellow;
            lbl.Visibility = Visibility.Visible;
            lbl.Content = komunikat;
        }

        /// <summary>
        /// Usuwanie komunikatu o błędzie.
        /// </summary>
        /// <param name="kontrolka">The kontrolka.</param>
        /// <param name="lbl">The label.</param>
        public static void UsunBlad(Control kontrolka, Label lbl)
        {
            kontrolka.Background = Brushes.White;
            lbl.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Walidacja istnienia uczniów, przy tworzeniu nowego rodzica.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="label">The label.</param>
        /// <param name="test">The test.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool WalidacjaDzieci(TextBox textBox, Label label, string test)
        {
            if (String.IsNullOrEmpty(test))
            {
                UsunBlad(textBox, label);
                return true;
            }
            WyswietlBlad(textBox, label, test);
            return false;
        }

        /// <summary>
        /// Walidacja czy zostało wpisane hasło uzytkownika podczas logowania.
        /// </summary>
        /// <param name="passwordBox">The password box.</param>
        /// <param name="label">The label.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool WalidacjaHasla(PasswordBox passwordBox, Label label)
        {
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                WyswietlBlad(passwordBox, label, BledyWalidacji.KomunikatPoleJestWymagane);
                return false;
            }
            UsunBlad(passwordBox, label);
            return true;
        }
    }
}