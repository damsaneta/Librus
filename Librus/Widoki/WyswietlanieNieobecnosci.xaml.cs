using Librus.DostepDoDanych;
using Librus.DostepDoDanych.BazaDanych;
using Librus.DostepDoDanych.Pamiec;
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
    /// <summary>
    /// Interaction logic for Wyswietlanie_nieobecnosci.xaml
    /// </summary>
    public partial class WyswietlanieNieobecnosci : Window
    {
      //  private const string connectionString = @"Data Source=(LocalDB)\v12.0;AttachDbFilename=D:\Users\aneta\Desktop\Librus\Librus\LibrusDatabase.mdf;Integrated Security=True";
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        private readonly IRepozytoriumObecnosci repozytoriumObecnosciUcznia;
        public WyswietlanieNieobecnosci(string m)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.repozytoriumObecnosciUcznia = new RepozytoriumObecnosci(connectionString);
            //TO DO: Pobieranie emaila z logowania ??
            InitializeComponent();
            string mail = m;
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
                    this.nieobecnosciDataGrid.ItemsSource = repozytoriumObecnosciUcznia.PobierzObecnosciPoUczniu(uczen);
                    break;
            }
        }

        private void UczenComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var uczen = this.uczenComboBox.SelectedItem as Uczen;
            this.nieobecnosciDataGrid.ItemsSource = repozytoriumObecnosciUcznia.PobierzObecnosciPoUczniu(uczen);
        }
    }
}
