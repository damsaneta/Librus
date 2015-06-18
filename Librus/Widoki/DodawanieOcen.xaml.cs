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
    /// Interaction logic for DodawanieOcen.xaml
    /// </summary>
    public partial class DodawanieOcen : Window
    {
        private readonly RepozytoriumUzytkownikowWPamieci repozytorium = new RepozytoriumUzytkownikowWPamieci();
        public DodawanieOcen()
        {
            InitializeComponent();
        }
        private void KlasaComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bool test = Walidator.WalidacjaComboBox(this.klasaComboBox, this.errKlasa);
                
            }
            catch
            {

            }
            
          
        }

        private void PrzedmiotComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Walidator.WalidacjaComboBox(this.przedmiotComboBox, this.errPrzedmiot);
            }
            catch
            {

            }

        }

        private void BtnDodajClick(object sender, RoutedEventArgs e)
        {
            bool result = true;
            result &= Walidator.WalidacjaComboBox(this.klasaComboBox, this.errKlasa);
            result &= Walidator.WalidacjaComboBox(this.przedmiotComboBox, this.errPrzedmiot);
            if(result)
            {

            }
        }

        private void PokazClick(object sender, RoutedEventArgs e)
        {
            bool result = true;
            result &= Walidator.WalidacjaComboBox(this.klasaComboBox, this.errKlasa);
            result &= Walidator.WalidacjaComboBox(this.przedmiotComboBox, this.errPrzedmiot);
            if(result)
            {
                this.ocenyDataGrid.ItemsSource = this.repozytorium.WyszukajPoKlasie("IA")
                 .Select(uczen => new ObecnoscUcznia(uczen)).ToList();
            }

        }
    }
}

