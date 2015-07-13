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
    /// Interaction logic for WyswietlanieOcen.xaml
    /// </summary>
    public partial class WyswietlanieOcen : Window
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow = new RepozytoriumUzytkownikowWPamieci();
        private readonly IRepozytoriumOcenUcznia repozytoriumOcenUcznia = new RepozytoriumOcenUcznia();
        public WyswietlanieOcen()
        {
            //TO DO: Pobieranie emaila z logowania ??
            InitializeComponent();
            Rodzic rodzic = repozytoriumUzytkownikow.PobierzPoEmailu("damsA@gmail.com") as Rodzic;
            this.uczenComboBox.ItemsSource = rodzic.Dzieci.ToList();
            this.uczenComboBox.DisplayMemberPath = "PelnaNazwa";
            this.uczenComboBox.SelectedValuePath = "PelnaNazwa";
        }

        public void UczenComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool test = true;
             test &= Walidator.WalidacjaWymaganegoComboBoxa(this.uczenComboBox, this.errUczen);
            if (test && !string.IsNullOrEmpty(this.uczenComboBox.SelectedValue.ToString()))
            {
                var uczen = this.uczenComboBox.SelectedItem as Uczen;
                this.ocenyDataGrid.ItemsSource = repozytoriumOcenUcznia.PobierzOcenyPoUczniu(uczen);
            }
        }
    }
}
