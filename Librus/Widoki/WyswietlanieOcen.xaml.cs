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
        private readonly IRepozytoriumOcenUcznia repozytoriumOcenUcznia = new RepozytoriumOcenUczniaWPamieci();
        public WyswietlanieOcen()
        {
            //TO DO: Pobieranie emaila z logowania ??
            InitializeComponent();
            string mail = "damsA@gmail.com";
           // string mail = "jamr@gmail.com";
            Uzytkownik user = repozytoriumUzytkownikow.PobierzPoEmailu(mail);
            //Rodzic rodzic = repozytoriumUzytkownikow.PobierzPoEmailu(mail) as Rodzic;
            switch (user.Rola.Typ)
            {
                case TypRoli.Rodzic:
                    Rodzic rodzic = user as Rodzic;
                    this.uczenComboBox.Visibility = Visibility.Visible;
                    this.uczenLbl.Visibility = Visibility.Visible;
                    this.uczenComboBox.ItemsSource = rodzic.Dzieci.ToList();
                    this.uczenComboBox.DisplayMemberPath = "PelnaNazwa";
                    this.uczenComboBox.SelectedValuePath = "PelnaNazwa";
                    break;
                case TypRoli.Uczen:
                    Uczen uczen = user as Uczen;
                    this.uczenComboBox.Visibility = Visibility.Hidden;
                    this.uczenLbl.Visibility = Visibility.Hidden;
                    this.ocenyDataGrid.ItemsSource = repozytoriumOcenUcznia.PobierzOcenyPoUczniu(uczen);
                    break;
                    

            }

        }

        public void UczenComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var uczen = this.uczenComboBox.SelectedItem as Uczen;
            this.ocenyDataGrid.ItemsSource = repozytoriumOcenUcznia.PobierzOcenyPoUczniu(uczen);
        }
    }
}
