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

        public void Dodaj(Przedmiot przedmiot)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Przedmioty (Id, Nazwa) VALUES (@id,@nazwa)";
                    cmd.Parameters.AddWithValue("@id", przedmiot.Id);
                    cmd.Parameters.AddWithValue("@nazwa", przedmiot.Nazwa);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Przedmiot> PobierzWszystkie()
        {
            throw new NotImplementedException();
        }

        public Przedmiot ZnajdzPrzedmiot(string id)
        {
            throw new NotImplementedException();
        }
    }
}
