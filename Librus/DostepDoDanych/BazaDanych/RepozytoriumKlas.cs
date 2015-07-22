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

        public void Dodaj(Klasa klasa)
        {
            using(var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Klasy (Id, Nazwa) VALUES (@Id,@Nazwa)";
                    cmd.Parameters.AddWithValue("@Id", klasa.Id);
                    cmd.Parameters.AddWithValue("@Nazwa", klasa.Nazwa);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Klasa> PobierzWszystkie()
        {
            IList<Klasa> wynik = new List<Klasa>();
            using(var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Nazwa FROM Klasy";
                  
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            var id = reader["Id"].ToString();
                            var nazwa =reader["Nazwa"].ToString();
                            var klasa = new Klasa(id, nazwa);
                            wynik.Add(klasa);
                            
                        }
                    }
                }
            }
            return wynik;
        }

        public Klasa ZnajdzKlase(string id)
        {
            using(var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using(var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Nazwa WHERE Id LIKE @id";
                    cmd.Parameters.AddWithValue("@id", id);

                }
            }
            return null;
        }
    }
}
