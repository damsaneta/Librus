﻿using Librus.DostepDoDanych.Pamiec;
using Librus.Model;
using System;
using System.Collections.Generic;
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
        private readonly RepozytoriumUzytkownikowWPamieci repozytorium = new RepozytoriumUzytkownikowWPamieci();
        public DodawanieUzytkownika()
        {
            InitializeComponent();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
           
            bool result = true;
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtImie, this.errImie);
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtNazwisko, this.errNazwisko);
            result &= Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtEmail, this.errEmail);
            if( Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtImie, this.errImie))
            {
               result &= Walidator.WalidacjaPolaNazwy(txtImie, this.errImie);
            }
            if(Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtNazwisko,this.errNazwisko))
            {
               result &= Walidator.WalidacjaPolaNazwy(this.txtNazwisko, this.errNazwisko);
            }
            if( Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtEmail,this.errEmail))
            {
                result &= Walidator.WalidacjaPolaEmail(this.txtEmail, this.errEmail);
            }
            //result &= Walidator.WalidacjaIstnieniaUzytkownika(this.txtEmail, this.errEmail);
            result &= Walidator.WalidacjaComboBox(this.comboRola, this.errRola);
            Uzytkownik u = repozytorium.PobierzPoEmailu(this.txtEmail.Text);

            if (TypRoli.Uczen==(TypRoli)this.comboRola.SelectedIndex)
            {
                result &=Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtKlasa, this.errKlasa);

            }
            if(result == true)
            {
                if (u == null)
                {
                    Walidator.UsunBlad(this.txtEmail,this.errEmail);
                    TypRoli typ = (TypRoli)this.comboRola.SelectedIndex;
                    string haslo = this.txtImie.Text.Substring(0, 3) + this.txtNazwisko.Text.Substring(0, 3);

                   
                    
                    Uzytkownik uzytkownik = new Uzytkownik(this.txtImie.Text, this.txtNazwisko.Text,
                       this.txtEmail.Text, typ, haslo);

                    this.repozytorium.Dodaj(uzytkownik);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    Walidator.WyswietlBlad(this.txtEmail,this.errEmail, "Uzytkownik o takim e-mailu juz istnieje!");
                }
               
            }  
         
        }

        private void TextBoxImie_TextChanged(object sender, TextChangedEventArgs e)
        {
           if( Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtImie, this.errImie))
           {
               Walidator.WalidacjaPolaNazwy(this.txtImie, this.errImie);
           }
        }

        private void TextBoxNazwisko_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtNazwisko,this.errNazwisko))
            {
                Walidator.WalidacjaPolaNazwy(this.txtNazwisko, this.errNazwisko);
            }
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtEmail,this.errEmail))
            {
                Walidator.WalidacjaPolaEmail(this.txtEmail, this.errEmail);
            }
        }

        private void TxtKlasa_TextChanged(object sender, TextChangedEventArgs e)
        {
            Walidator.WalidacjaWymaganegoPolaTekstowego(this.txtKlasa, this.errKlasa);
        }  

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboRola_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            bool result = true;
            try
            {
               result= Walidator.WalidacjaComboBox(this.comboRola, this.errRola);
                if(result==true)
                {
                    TypRoli typ = (TypRoli)this.comboRola.SelectedIndex;
                    if(typ == TypRoli.Uczen)
                    {
                        this.txtKlasa.Visibility = Visibility.Visible;
                        this.lblKlasa.Visibility = Visibility.Visible;
                    }
                    else if(typ != TypRoli.Uczen)
                    {
                        this.txtKlasa.Visibility = Visibility.Hidden;
                        this.lblKlasa.Visibility = Visibility.Hidden;
                    }
                     if(typ==TypRoli.Rodzic)
                    {
                        this.txtDziecko.Visibility = Visibility.Visible;
                        this.lblDziecko.Visibility = Visibility.Visible;
                    }
                    else if (typ != TypRoli.Rodzic)
                    {
                        this.txtDziecko.Visibility = Visibility.Hidden;
                        this.lblDziecko.Visibility = Visibility.Hidden;
                    }
                }
                
            }
            catch
            {

            }  
        }

             private void txtDziecko_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

            
    }
}
