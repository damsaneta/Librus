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

        public Task Dodaj(Uzytkownik uzytkownik)
        {
            return Task.Factory.StartNew(() =>
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
                            var rodzic = uzytkownik as Rodzic;
                            if (rodzic != null)
                            {
                                foreach (var dziecko in rodzic.Dzieci)
                                {
                                    DodajDziecko(rodzic, dziecko);
                                }
                            }
                        }
                    }
                });
        }

        private void DodajDziecko(Rodzic rodzic, Uzytkownik dziecko)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Dzieci (IdRodzica, IdDziecka) VALUES(@IdRodzica, @IdDziecka)";
                    cmd.Parameters.AddWithValue("@IdRodzica", rodzic.Id);
                    cmd.Parameters.AddWithValue("@IdDziecka", dziecko.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Task<Uzytkownik> PobierzPoEmailu(string email)
        {
            return Task.Factory.StartNew(() =>
                {
                    IList<Uzytkownik> wynik = new List<Uzytkownik>();
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        connection.Open();
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "SELECT Id, Imie, Nazwisko, Email, Rola, Haslo, KlasaId FROM Uzytkownicy WHERE Email = @Email";
                            cmd.Parameters.AddWithValue("@Email", email);
                            wynik = this.WywolanieKomendy(cmd);
                        }
                    }

                    return wynik.SingleOrDefault();
                });
        }
        public Task<Uzytkownik> PobierzUzytkownikaPoId(int id)
        {
            return Task.Factory.StartNew(() =>
                {
                    IList<Uzytkownik> wynik = new List<Uzytkownik>();
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        connection.Open();
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "SELECT Id, Imie, Nazwisko, Email, Rola, Haslo, KlasaId FROM Uzytkownicy WHERE Id = @id";
                            cmd.Parameters.AddWithValue("@id", id);
                            wynik = this.WywolanieKomendy(cmd);
                        }
                    }

                    return wynik.SingleOrDefault();
                });
        }

        public Task<IList<Uzytkownik>> PobierzWszystkich()
        {
            return Task.Factory.StartNew(() =>
                {
                    IList<Uzytkownik> wynik = new List<Uzytkownik>();
                    using (var connection = new SqlConnection(this.connectionString))
                    {
                        connection.Open();
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "SELECT Id, Imie, Nazwisko, Email, Rola, Haslo, KlasaId FROM Uzytkownicy";
                            wynik = this.WywolanieKomendy(cmd);
                        }
                    }
                    return wynik;
                });
        }

        public Task<Uzytkownik> WyszukajDziecka(string imie, string nazwisko)
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Uzytkownik> wynik = new List<Uzytkownik>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Id, Imie, Nazwisko, Email, Rola, Haslo, KlasaId FROM Uzytkownicy WHERE Imie = @imie AND Nazwisko = @nazwisko";
                        cmd.Parameters.AddWithValue("@imie", imie);
                        cmd.Parameters.AddWithValue("@nazwisko", nazwisko);
                        wynik = this.WywolanieKomendy(cmd);
                    }
                }

                return wynik.SingleOrDefault();
            });
        }

        public Task<IList<Uczen>> WyszukajPoKlasie(string wzorzec)
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Uczen> wynik = new List<Uczen>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Uzytkownicy WHERE KlasaId = @KlasaId";
                        cmd.Parameters.AddWithValue("@KlasaId", wzorzec);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = (int)reader["Id"];
                                var imie = reader["Imie"].ToString();
                                var nazwisko = reader["Nazwisko"].ToString();
                                var email = reader["Email"].ToString();
                                var haslo = reader["Haslo"].ToString();
                                var klasaId = reader["KlasaId"].ToString();
                                Klasa klasa = repozytoriumKlas.ZnajdzKlase(klasaId);
                                Uczen uczen = new Uczen(imie, nazwisko, email, haslo, klasa);
                                uczen.Id = id;
                                wynik.Add(uczen);

                            }
                        }
                    }
                }
                return wynik;
            });
        }

        public Task<IList<Uzytkownik>> WyszukajPoRoli(string wzorzec)
        {
            return Task.Factory.StartNew(() =>
            {
                var typRoli = (TypRoli)Enum.Parse(typeof(TypRoli), wzorzec);
                IList<Uzytkownik> wynik = new List<Uzytkownik>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Uzytkownicy WHERE Rola = @Rola";
                        cmd.Parameters.AddWithValue("@Rola", wzorzec);
                        wynik = this.WywolanieKomendy(cmd);
                    }
                }
                return wynik;
            });
        }

        public Task<IList<Uzytkownik>> WyszukajPoRoliIWzorcu(string wzorzec, string rola)
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Uzytkownik> wynik = new List<Uzytkownik>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Uzytkownicy WHERE (Imie LIKE @Wzorzec OR Nazwisko LIKE @Wzorzec OR Email LIKE @Wzorzec) AND Rola LIKE @Rola";
                        cmd.Parameters.AddWithValue("@Wzorzec", wzorzec + "%");
                        cmd.Parameters.AddWithValue("@Rola", rola + "%");
                        wynik = this.WywolanieKomendy(cmd);
                    }
                }
                return wynik;
            });
        }

        public Task<IList<Uzytkownik>> WyszukajUzytkownikow(string wzorzec)
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Uzytkownik> wynik = new List<Uzytkownik>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Uzytkownicy WHERE Imie LIKE @Wzorzec OR Nazwisko LIKE @Wzorzec OR Email LIKE @Wzorzec";
                        cmd.Parameters.AddWithValue("@Wzorzec", wzorzec + "%");
                        wynik = this.WywolanieKomendy(cmd);
                    }
                }
                return wynik;
            });
        }

        public Task<IList<Uzytkownik>> WyszukiwanieDzieci(string tekst)
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Uzytkownik> dzieci = new List<Uzytkownik>();
                string brakDzieci = string.Empty;
                string[] tab = tekst.Split(new char[] { ',' });
                for (int i = 0; i < tab.Length; i++)
                {
                    tab[i] = tab[i].Trim();
                }
                foreach (string s in tab)
                {
                    string[] wynik = s.Split(new char[] { ' ' });
                    string imie = wynik[0];
                    string nazwisko = wynik[1];
                    Task<Uzytkownik> t = this.WyszukajDziecka(imie, nazwisko);
                    t.Wait();
                    Uzytkownik dziecko = t.Result; 
                    if (dziecko != null)
                    {
                        dzieci.Add(dziecko);
                    }
                }
                return dzieci;
            });
        }

        public Task<IList<Uzytkownik>> PobierzPoIdRodzica(int idRodzica)
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Uzytkownik> wynik = new List<Uzytkownik>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    Uzytkownik uzytkownik = null; ;
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT Id, Imie, Nazwisko, Email, Rola, Haslo, KlasaId 
                        FROM Uzytkownicy u
                        INNER JOIN Dzieci d ON u.Id = d.IdDziecka
                        WHERE d.IdRodzica = @idRodzica";
                        cmd.Parameters.AddWithValue("@idRodzica", idRodzica);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = (int)reader["Id"];
                                var imie = reader["Imie"].ToString();
                                var nazwisko = reader["Nazwisko"].ToString();
                                var email = reader["Email"].ToString();
                                var haslo = reader["Haslo"].ToString();
                                var klasaId = (string)reader["KlasaId"];

                                Klasa klasa = repozytoriumKlas.ZnajdzKlase((string)klasaId);
                                uzytkownik = new Uczen(imie, nazwisko, email, haslo, klasa);
                                uzytkownik.Id = id;

                                wynik.Add(uzytkownik);

                            }
                        }
                    }
                }
                return wynik;
            });
        }

        private IList<Uzytkownik> WywolanieKomendy(SqlCommand cmd)
        {
            Uzytkownik uzytkownik = null;
            IList<Uzytkownik> wynik = new List<Uzytkownik>();
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
                            break;
                        case TypRoli.Nauczyciel:
                            uzytkownik = new Nauczyciel(imie, nazwisko, email, haslo);
                            break;
                        case TypRoli.Rodzic:
                      var t = this.PobierzPoIdRodzica(id);
                            t.Wait();
                            var dzieci = t.Result;
                            uzytkownik = new Rodzic(imie, nazwisko, email, haslo, dzieci);
                            break;
                        case TypRoli.Uczen:
                            Klasa klasa = repozytoriumKlas.ZnajdzKlase((string)klasaId);
                            uzytkownik = new Uczen(imie, nazwisko, email, haslo, klasa);
                            break;
                    }
                    uzytkownik.Id = id;
                    wynik.Add(uzytkownik);
                }
            }
            return wynik;
        }

    }
}
