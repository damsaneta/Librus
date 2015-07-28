using Librus.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.BazaDanych
{
    public class RepozytoriumOcenUcznia : IRepozytoriumOcenUcznia
    {
        private readonly string connectionString;
        private readonly IRepozytoriumPrzedmiotow repozytoriumPrzedmiotow;
        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        public RepozytoriumOcenUcznia(string connectionString)
        {
            this.connectionString = connectionString;
            this.repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            this.repozytoriumPrzedmiotow = new RepozytoriumPrzedmiotow(connectionString);
        }

        public IList<OcenyUcznia> PobierzOcenyPoUczniu(Uczen uczen)
        {
            IList<OcenyUcznia> oceny = new List<OcenyUcznia>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Oceny WHERE IdUcznia = @IdUcznia";
                    cmd.Parameters.AddWithValue("@IdUcznia", uczen.Id);
                    oceny = this.WykonajKomende(cmd);

                }
            }
            return oceny;
        }



        public IList<OcenyUcznia> PobierzPoKlasieIPrzedmiocie(string klasaId, string przedmiotId)
        {
            IList<OcenyUcznia> oceny = new List<OcenyUcznia>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT IdUcznia, IdPrzedmiotu, Oceny FROM Oceny o INNER JOIN Uzytkownicy u
                    ON o.IdUcznia = u.Id WHERE u.KlasaId = @klasaId AND o.IdPrzedmiotu = @przedmiotId";
                    cmd.Parameters.AddWithValue("@klasaId", klasaId);
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);

                    oceny = this.WykonajKomende(cmd);

                }
            }
            return oceny;
        }

        public IList<OcenyUcznia> PobierzOcenyPoUczniuIPrzedmiocie(Uczen uczen, string przedmiotId)
        {
            IList<OcenyUcznia> oceny = new List<OcenyUcznia>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT IdUcznia, IdPrzedmiotu, Oceny FROM Oceny WHERE IdUcznia = @idUcznia AND 
                    IdPrzedmiotu = @przedmiotId";
                    cmd.Parameters.AddWithValue("@idUcznia", uczen.Id);
                    cmd.Parameters.AddWithValue("@przedmiotId", przedmiotId);

                    oceny = this.WykonajKomende(cmd);

                }
            }
            return oceny;
        }
        public void Zapisz(IList<OcenyUcznia> oceny)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Oceny (IdUcznia, IdPrzedmiotu, Oceny) VALUES (@IdUcznia, @IdPrzedmiotu, @Oceny)";
                    foreach (OcenyUcznia o in oceny)
                    {
                        var c = this.PobierzOcenyPoUczniuIPrzedmiocie(o.Uczen, o.Przedmiot.Id);
                        if (c == null || c.Count == 0)
                        {
                            cmd.Parameters.AddWithValue("@IdUcznia", o.Uczen.Id);
                            cmd.Parameters.AddWithValue("@IdPrzedmiotu", o.Przedmiot.Id);
                            cmd.Parameters.AddWithValue("@Oceny", o.Oceny);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        else
                        {
                            this.EdytujOceny(o);
                        }
                    }
                }
            }

        }
        public void EdytujOceny(OcenyUcznia oceny)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Oceny SET Oceny = @Oceny WHERE IdUcznia = @IdUcznia AND IdPRzedmiotu = @IdPrzedmiotu";

                    cmd.Parameters.AddWithValue("@IdUcznia", oceny.Uczen.Id);
                    cmd.Parameters.AddWithValue("@IdPrzedmiotu", oceny.Przedmiot.Id);
                    cmd.Parameters.AddWithValue("@Oceny", oceny.Oceny);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }

        }
        private IList<OcenyUcznia> WykonajKomende(SqlCommand cmd)
        {
            IList<OcenyUcznia> wynik = new List<OcenyUcznia>();
            OcenyUcznia ocenyUcznia = null;
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var idUcznia = (int)reader["IdUcznia"];
                    var idPrzedmiotu = (string)reader["IdPrzedmiotu"];
                    var oceny = (string)reader["Oceny"];
                    Uczen uczen = (Uczen)this.repozytoriumUzytkownikow.PobierzUzytkownikaPoId(idUcznia);
                    Przedmiot przedmiot = this.repozytoriumPrzedmiotow.ZnajdzPrzedmiot(idPrzedmiotu);
                    ocenyUcznia = new OcenyUcznia(uczen, przedmiot);
                    ocenyUcznia.Oceny = oceny;
                    wynik.Add(ocenyUcznia);

                }
            }
            return wynik;
        }
    }
}
