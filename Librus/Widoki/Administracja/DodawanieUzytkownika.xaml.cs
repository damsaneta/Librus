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
            result &= Walidator.WalidacjaComboBox(this.comboRola, this.errRola);
            Uzytkownik u = repozytorium.PobierzPoEmailu(this.txtEmail.Text);
            
            if(result == true)
            {
                if (u == null)
                {
                    Walidator.UsunBlad(this.txtEmail,this.errEmail);
                    TypRoli typ = (TypRoli)this.comboRola.SelectedIndex;
                    Uzytkownik uzytkownik = new Uzytkownik(this.txtImie.Text, this.txtNazwisko.Text,
                        this.txtEmail.Text, typ);
                    this.repozytorium.Dodaj(uzytkownik);
                    this.Close();
                }
                else
                {
                    Walidator.WyswietlBlad(this.txtEmail,this.errEmail, "Uzytkownik juz istnieje!");
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

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
    }
}
