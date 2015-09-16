using Librus.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.BazaDanych
{
    public class RepozytoriumPrzedmiotow : IRepozytoriumPrzedmiotow
    {
        private readonly string connectionString;

        public RepozytoriumPrzedmiotow(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<IList<Przedmiot>> PobierzWszystkie()
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Przedmiot> wynik = new List<Przedmiot>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Id, Nazwa FROM Przedmioty";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader["Id"].ToString();
                                var nazwa = reader["Nazwa"].ToString();
                                var przedmiot = new Przedmiot(id, nazwa);
                                wynik.Add(przedmiot);

                            }
                        }
                    }
                }
                return wynik;
            });
        }

        public Przedmiot ZnajdzPrzedmiot(string id)
        {
            Przedmiot przedmiot = null;
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Przedmioty WHERE Id LIKE @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nazwa = reader["Nazwa"].ToString();
                            var idd = reader["Id"].ToString();
                            przedmiot = new Przedmiot(idd, nazwa);
                        }
                    }
                }
            }
            return przedmiot;
        }
    }
}
