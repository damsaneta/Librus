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
        public static bool WalidacjaPolaEmail(TextBox txt,Label lbl)
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
    }
}
