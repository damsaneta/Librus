using Librus.DostepDoDanych;
using Librus.DostepDoDanych.Pamiec;
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

namespace Librus.Widoki
{
    /// <summary>
    /// Interaction logic for SprawdzanieObecnosci.xaml
    /// </summary>
    public partial class SprawdzanieObecnosci : Window
    {

        private readonly IRepozytoriumKlas repozytoriumKlas = new RepozytoriumKlas();
        private readonly IRepozytoriumObecnosci repozytoriumObecnosci = new RepozytoriumObecnosciWPamieci();
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow = new RepozytoriumUzytkownikowWPamieci();
       
        public SprawdzanieObecnosci()
        {

            InitializeComponent();
            this.wyborDaty.SelectedDate = DateTime.Now.Date;
            this.klasaComboBox.ItemsSource = this.repozytoriumKlas.PobierzWszystkie();
            this.klasaComboBox.DisplayMemberPath = "Nazwa";
            this.klasaComboBox.SelectedValuePath = "Id";// zmienic na nazwa
        }

        private void KlasaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, errData);//walidaxja daty
            if ((int)this.klasaComboBox.SelectedValue != 0 && test)
            {
                Klasa klasa = this.klasaComboBox.SelectedItem as Klasa;
                var obecnosci = this.repozytoriumObecnosci.PobierzPoKlasieIDacie(klasa.Nazwa, this.wyborDaty.SelectedDate.Value);
                if(obecnosci == null || obecnosci.Count ==0)
                {
                    var uczniowie = repozytoriumUzytkownikow.WyszukajPoKlasie(klasa.Nazwa);
                    obecnosci = uczniowie.Select(x => new ObecnoscUcznia(x, this.wyborDaty.SelectedDate.Value)).ToList();
                    this.nieobecnosciDataGrid.ItemsSource = obecnosci;
                }
                
            }

        }

        //private void GodzinaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    int ind = godzinaComboBox.SelectedIndex;
        //    if (ind != 0)
        //    {
        //        ind += 7;
        //        string wynik = ind + ":00";

        //        this.nieobecnosciDataGrid.Columns[1].Header = wynik;
        //    }
        //}

        private void DodajClick(object sender, RoutedEventArgs e)
        {
            bool test = true;
            test &= Walidator.WalidacjaWymaganegoPolaDaty(this.wyborDaty, this.errData);
            if ((int)this.klasaComboBox.SelectedValue != 0 && test)
            {
                var g = this.nieobecnosciDataGrid;
                var v = g.Items.SourceCollection as IList<ObecnoscUcznia>;
                this.repozytoriumObecnosci.Zapisz(v);

            }
            
        }
    }
}
