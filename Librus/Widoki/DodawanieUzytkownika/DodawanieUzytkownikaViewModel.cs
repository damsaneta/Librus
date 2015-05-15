using Librus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Librus.Widoki.DodawanieUzytkownika
{
    public class DodawanieUzytkownikaViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Uzytkownik model = new Uzytkownik();
        protected Dictionary<string, string> errors = new Dictionary<string, string>();

        public string TxtImie
        {
            get { return this.model.Imie; }
            set 
            {
                this.WyczyscBlad("TxtImie");
                this.WalidujWymaganePole("TxtImie", value);
                this.WalidujPoleNazwy("TxtImie", value);
                model.Imie = value; 
            }
        }
        public string TxtNazwisko
        {
            get { return this.model.Nazwisko; }
            set
            {
                this.WyczyscBlad("TxtNazwisko");
                this.WalidujWymaganePole("TxtNazwisko", value);
                this.WalidujPoleNazwy("TxtNazwisko", value);
                model.Nazwisko = value;

            }
        }
        public string TxtEmail
        {
            get { return this.model.Email; }
            set
            {
                this.WyczyscBlad("TxtEmail");
                this.WalidujWymaganePole("TxtEmail", value);
                this.WalidujPoleEmail("TxtEmail", value);
                model.Email = value;
            }
        }
      
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return new List<string> { this.errors[propertyName] };
        }

        public bool HasErrors
        {
            get { return this.errors.Count>0; }
        }

        protected void WyczyscBlad(string nazwa)
        {
            if(this.errors.ContainsKey(nazwa))
            {
                this.errors.Remove(nazwa);
            }
        }
        protected void DodajBlad(string nazwa, string blad)
        {
            if(!this.errors.ContainsKey(nazwa))
            {
                this.errors.Add(nazwa, blad);
                this.PowiadomOBledzie(nazwa);
            }
        }
        protected void PowiadomOBledzie(string nazwa)
        {
            if(this.ErrorsChanged!=null)
            {
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(nazwa));
            }
        }
        protected void WalidujPoleNazwy(string nazwa, string wartosc)
        {
            foreach (char c in wartosc)
            {
                if (char.IsLetter(c) == false)
                {
                    this.DodajBlad(nazwa, "Podaj poprawną wartość");
                }
            }      
        }
        protected void WalidujPoleEmail(string nazwa, string wartosc)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(wartosc);
            if (!match.Success)
            {
                DodajBlad(nazwa, "Podaj poprawny e-mail");
            } 
        }

        protected void WalidujWymaganePole(string nazwa, string wartosc)
        {
            if(string.IsNullOrEmpty(wartosc))
            {
                this.DodajBlad(nazwa, "Pole jest wymagane");
            }
        }
    }
}
