﻿using Librus.Model;
using Librus.Widoki;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librus.DostepDoDanych.BazaDanych
{
    public class RepozytoriumObecnosci : IRepozytoriumObecnosci
    {
        private readonly string connectionString;

        private readonly IRepozytoriumUzytkownikow repozytoriumUzytkownikow;
        private readonly IRepozytoriumKlas repozytoriumKlas;
        public RepozytoriumObecnosci(string connectionString)
        {
            this.connectionString = connectionString;
            repozytoriumUzytkownikow = new RepozytoriumUzytkownikow(connectionString);
            repozytoriumKlas = new RepozytoriumKlas(connectionString);
        }

        public IList<ObecnoscUcznia> PobierzPoKlasieIDacie(string klasaId, DateTime data)
        {
            
            IList<ObecnoscUcznia> obecnosci = new List<ObecnoscUcznia>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT o.Id, Data, Godzina8, Godzina9, Godzina10, Godzina11, Godzina12, Godzina13, Godzina14, Godzina15
                    FROM Obecnosci o INNER JOIN Uzytkownicy u
                    ON o.Id = u.Id WHERE u.KlasaId = @klasaId AND o.Data = @data";
                    cmd.Parameters.AddWithValue("@klasaId", klasaId);
                    cmd.Parameters.AddWithValue("@data", data);

                    obecnosci= this.WykonajKomende(cmd);
                }
            }
            return obecnosci;
        }

        public IList<ObecnoscUcznia> PobierzObecnosciPoUczniu(Uczen uczen)
        {
           
            IList<ObecnoscUcznia> obecnosci = new List<ObecnoscUcznia>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Data, Godzina8, Godzina9, Godzina10, Godzina11, Godzina12, Godzina13, Godzina14, Godzina15
                    FROM Obecnosci WHERE Id = @KlasaId ";
                    cmd.Parameters.AddWithValue("@klasaId", uczen.Id);
                    obecnosci = this.WykonajKomende(cmd);
                }
            }
            return obecnosci;
        }
        public IList<ObecnoscUcznia> PobierzObecnoscPoUczniuIDacie(Uczen uczen, DateTime data)
        {
            IList<ObecnoscUcznia> obecnosci = new List<ObecnoscUcznia>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Data, Godzina8, Godzina9, Godzina10, Godzina11, Godzina12, Godzina13, Godzina14, Godzina15
                    FROM Obecnosci WHERE Id = @KlasaId AND Data = @data ";
                    cmd.Parameters.AddWithValue("@klasaId", uczen.Id);
                    cmd.Parameters.AddWithValue("@data", data);
                    obecnosci = this.WykonajKomende(cmd);
                }
            }
            return obecnosci;
        }

        public void Zapisz(IList<ObecnoscUcznia> obecnosci)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Obecnosci (Id, Data, Godzina8, Godzina9, Godzina10, Godzina11, Godzina12, Godzina13, Godzina14, Godzina15)
                            VALUES (@Id, @Data, @Godzina8, @Godzina9, @Godzina10,  @Godzina11, @Godzina12, @Godzina13, @Godzina14, @Godzina15)";
                    foreach (ObecnoscUcznia obe in obecnosci)
                    {
                        var c = PobierzObecnoscPoUczniuIDacie(obe.Uczen, obe.Data);
                        if (c == null || c.Count == 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", obe.Uczen.Id);
                            cmd.Parameters.AddWithValue("@Data", obe.Data);
                            cmd.Parameters.AddWithValue("@Godzina8", obe.Godzina8);
                            cmd.Parameters.AddWithValue("@Godzina9", obe.Godzina9);
                            cmd.Parameters.AddWithValue("@Godzina10", obe.Godzina10);
                            cmd.Parameters.AddWithValue("@Godzina11", obe.Godzina11);
                            cmd.Parameters.AddWithValue("@Godzina12", obe.Godzina12);
                            cmd.Parameters.AddWithValue("@Godzina13", obe.Godzina13);
                            cmd.Parameters.AddWithValue("@Godzina14", obe.Godzina14);
                            cmd.Parameters.AddWithValue("@Godzina15", obe.Godzina15);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                }
            }
        }

        private IList<ObecnoscUcznia> WykonajKomende(SqlCommand cmd)
        {
            IList<ObecnoscUcznia> obecnosci = new List<ObecnoscUcznia>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var dataNieobecnosci = (DateTime)reader["Data"];
                    var godzina8 = (bool)reader["Godzina8"];
                    var godzina9 = (bool)reader["Godzina9"];
                    var godzina10 = (bool)reader["Godzina10"];
                    var godzina11 = (bool)reader["Godzina11"];
                    var godzina12 = (bool)reader["Godzina12"];
                    var godzina13 = (bool)reader["Godzina13"];
                    var godzina14 = (bool)reader["Godzina14"];
                    var godzina15 = (bool)reader["Godzina15"];
                    var uczen = (Uczen)this.repozytoriumUzytkownikow.PobierzUzytkownikaPoId(id);
                    
                    
                    ObecnoscUcznia obecnosc = new ObecnoscUcznia(uczen, dataNieobecnosci);
                    obecnosc.Godzina10 = godzina10;
                    obecnosc.Godzina9 = godzina9;
                    obecnosc.Godzina11 = godzina11;
                    obecnosc.Godzina12 = godzina12;
                    obecnosc.Godzina13 = godzina13;
                    obecnosc.Godzina14 = godzina14;
                    obecnosc.Godzina15 = godzina15;
                    obecnosci.Add(obecnosc);
                }
                return obecnosci;
            }
        }
    }
}
