using Librus.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.BazaDanych
{
    public class RepozytoriumUzytkownikow : IRepozytoriumUzytkownikow
    {
        private readonly string connectionString;
        private readonly IRepozytoriumKlas repozytoriumKlas;
        public RepozytoriumUzytkownikow(string connectionString)
        {
            this.connectionString = connectionString;
            repozytoriumKlas = new RepozytoriumKlas(this.connectionString);
        }

        public void Dodaj(Uzytkownik uzytkownik)
        {
            var uczen = uzytkownik as Uczen;
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Uzytkownicy (Imie, Nazwisko, Email, Rola,Haslo, KlasaId) 
                        VALUES (@Imie,@Nazwisko,@Email,@Rola,@Haslo,@KlasaId)
                        SET @Id = SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("@Imie", uzytkownik.Imie);
                    cmd.Parameters.AddWithValue("@Nazwisko", uzytkownik.Nazwisko);
                    cmd.Parameters.AddWithValue("@Email", uzytkownik.Email);
                    cmd.Parameters.AddWithValue("@Rola", uzytkownik.Rola.Typ.ToString());
                    cmd.Parameters.AddWithValue("@Haslo", uzytkownik.Haslo);
                    cmd.Parameters.AddWithValue("@KlasaId", uczen != null ? (object)uczen.Klasa.Id : DBNull.Value);
                    var parameter = cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                    parameter.Direction = System.Data.ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    uzytkownik.Id = (int)parameter.Value;
                }
            }

        }

        public Uzytkownik PobierzPoEmailu(string email)
        {
            throw new NotImplementedException();
        }

        public IList<Uzytkownik> PobierzWszystkich()
        {
            IList<Uzytkownik> wynik = new List<Uzytkownik>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                Uzytkownik uzytkownik = null; ;
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Imie, Nazwisko, Email, Rola, Haslo, KlasaId FROM Uzytkownicy";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (int)reader["Id"];
                            var imie = reader["Imie"].ToString();
                            var nazwisko = reader["Nazwisko"].ToString();
                            var email = reader["Email"].ToString();
                            var haslo = reader["Haslo"].ToString();
                            var klasaId = reader["KlasaId"] == DBNull.Value ? null : (object)reader["KlasaId"].ToString();
                            var rola = reader["Rola"].ToString();
                            var typRoli = (TypRoli)Enum.Parse(typeof(TypRoli), rola);
                            switch (typRoli)
                            {
                                case TypRoli.Administrator:
                                    uzytkownik = new Administrator(imie, nazwisko, email, haslo);
                                    uzytkownik.Id = id;
                                    break;
                                case TypRoli.Nauczyciel:
                                    uzytkownik = new Nauczyciel(imie, nazwisko, email, haslo);
                                    uzytkownik.Id = id;
                                    break;
                                case TypRoli.Rodzic:
                                    uzytkownik = new Rodzic(imie, nazwisko, email, haslo, null);
                                    uzytkownik.Id = id;
                                    break;
                                case TypRoli.Uczen:
                                    Klasa klasa = repozytoriumKlas.ZnajdzKlase((string)klasaId);
                                    uzytkownik = new Uczen(imie, nazwisko, email, haslo, klasa);
                                    uzytkownik.Id = id;
                                    break;
                            }
                            wynik.Add(uzytkownik);

                        }
                    }
                }
            }
            return wynik;
        }

        public Model.Uzytkownik WyszukajDziecka(string imie, string nazwisko)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Uczen> WyszukajPoKlasie(string wzorzec)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Uzytkownik> WyszukajPoRoli(string wzorzec)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Uzytkownik> WyszukajPoRoliIWzorcu(string wzorzec, string rola)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Uzytkownik> WyszukajUzytkownikow(string wzorzec)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Uzytkownik> WyszukiwanieDzieci(string tekst)
        {
            throw new NotImplementedException();
        }
    }
}
