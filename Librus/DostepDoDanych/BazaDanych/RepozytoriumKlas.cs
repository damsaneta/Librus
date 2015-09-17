using Librus.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.BazaDanych
{
    public class RepozytoriumKlas : IRepozytoriumKlas
    {
        private readonly string connectionString;

        public RepozytoriumKlas(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Pobiera wszystkie klasy z bazy.
        /// </summary>
        /// <returns>Task<IList<Klasa>>.</returns>
        public Task<IList<Klasa>> PobierzWszystkie()
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Klasa> wynik = new List<Klasa>();
                using (var connection = new SqlConnection(this.connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Id, Nazwa FROM Klasy";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader["Id"].ToString();
                                var nazwa = reader["Nazwa"].ToString();
                                var klasa = new Klasa(id, nazwa);
                                wynik.Add(klasa);

                            }
                        }
                    }
                }
                return wynik;
            });
        }

        /// <summary>
        /// Wyszukiwanie klasy w bazie na podstawie identyfikatora klasy. 
        /// </summary>
        /// <param name="id">Identyfikator.</param>
        /// <returns>Klasa.</returns>
        public Klasa ZnajdzKlase(string id)
        {
            Klasa klasa = null;
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Klasy WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nazwa = reader["Nazwa"].ToString();
                            var idd = reader["Id"].ToString();
                            klasa = new Klasa(idd, nazwa);
                        }
                    }
                }
            }
            return klasa;
        }
    }
}
