using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class WyswietlanieNieobecnosci : Window
    {
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        private readonly IRepozytoriumObecnosci repozytoriumObecnosciUcznia;
        private readonly string email;

        public WyswietlanieNieobecnosci(string email)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.repozytoriumObecnosciUcznia = new RepozytoriumObecnosci(connectionString);
            this.email = email;
            InitializeComponent();
            this.Inicjalizuj();
        }

        /// <summary>
        /// Inicjalizacja nowego okna, w zależności od roli.
        /// </summary>
        private async void Inicjalizuj()
        {
            Uzytkownik user = await repozytoriumUzytkownikow.PobierzPoEmailu(this.email);
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
                    this.nieobecnosciDataGrid.ItemsSource = await repozytoriumObecnosciUcznia.PobierzObecnosciPoUczniu(uczen);
                    break;
            }
        }

        /// <summary>
        /// Dostosowuje wyświetlaną zawartośc w zależności od wybranego ucznia.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private async void UczenComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var uczen = this.uczenComboBox.SelectedItem as Uczen;
            this.nieobecnosciDataGrid.ItemsSource = await repozytoriumObecnosciUcznia.PobierzObecnosciPoUczniu(uczen);
        }
    }
}
