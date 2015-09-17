using Librus.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// BazaDanych namespace.
/// </summary>
namespace Librus.DostepDoDanych.BazaDanych
{
    /// <summary>
    /// Class RepozytoriumPrzedmiotow.
    /// </summary>
    public class RepozytoriumPrzedmiotow : IRepozytoriumPrzedmiotow
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepozytoriumPrzedmiotow"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public RepozytoriumPrzedmiotow(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Metoda pobierająca z bazy wszystkie przedmioty.
        /// </summary>
        /// <returns>Task<IList<Przedmiot>>.</returns>
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

        /// <summary>
        /// Wyszukiwanie przedmiotu w bazie na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator</param>
        /// <returns>Przedmiot.</returns>
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
