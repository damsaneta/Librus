using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.BazaDanych
{
    public class RepozytoriumKlasB : IRepozytoriumKlas
    {
        private readonly string connectionString;

        public RepozytoriumKlasB(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Dodaj(Model.Klasa klasa)
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

        public IList<Model.Klasa> PobierzWszystkie()
        {
            throw new NotImplementedException();
        }

        public Model.Klasa ZnajdzKlase(string id)
        {
            throw new NotImplementedException();
        }
    }
}
