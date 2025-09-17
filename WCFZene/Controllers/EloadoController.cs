using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFZene.Models;


namespace WCFZene.Controllers
{
	public class EloadoController
	{
        static MySqlConnection SQLConnection;

        private static void BuildConnection()
        {
            string connectionString = "SERVER = localhost;" +
                                      "DATABASE= zene;" +
                                      "UID = root;" +
                                      "PASSWORD =;" +
                                      "SSL MODE= none;";
            SQLConnection = new MySqlConnection();
            SQLConnection.ConnectionString = connectionString;
        }

        public string EloadoTorlese(int id)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "DELETE FROM eloado WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@id", id);
            int sorokSzama = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return sorokSzama > 0 ? "Sikeres törlés" : "Hiba a törlés során";
        }

        public string EloadoModositasa(Feladat modositando)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "UPDATE feladat SET  Szoveg = @szoveg, Valasz1 = @valasz1 Valasz2 = @valasz2 Valasz3 = @valasz3 Helyes = @helyes Pont = @pont WHERE Id = @id";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@id", modositando.Id);
            cmd.Parameters.AddWithValue("@szoveg", modositando.Szoveg);
            cmd.Parameters.AddWithValue("@valasz1", modositando.Valasz1);
            cmd.Parameters.AddWithValue("@valasz2", modositando.Valasz2);
            cmd.Parameters.AddWithValue("@valasz3", modositando.Valasz3);
            cmd.Parameters.AddWithValue("@helyes", modositando.Helyes);
            cmd.Parameters.AddWithValue("@pont", modositando.Pont);
            int sorokSzama = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return sorokSzama > 0 ? "Sikeres módosítás" : "Hiba a módosítás során";
        }

        public string EloadoFevitele(Feladat rogzitendo)
        {
            BuildConnection();
            SQLConnection.Open();
            string sql = "INSERT INTO eloado(Id, Szoveg, Valasz1, Valasz2, Valasz3, Helyes, Pont) VALUES (@id,@szoveg,@valasz1,@valasz2,@valasz3,@helyes, @pont)";
            MySqlCommand cmd = new MySqlCommand(sql, SQLConnection);
            cmd.Parameters.AddWithValue("@id", rogzitendo.Id);
            cmd.Parameters.AddWithValue("@szoveg", rogzitendo.Szoveg);
            cmd.Parameters.AddWithValue("@valasz1", rogzitendo.Valasz1);
            cmd.Parameters.AddWithValue("@valasz2", rogzitendo.Valasz2);
            cmd.Parameters.AddWithValue("@valasz3", rogzitendo.Valasz3);
            cmd.Parameters.AddWithValue("@helyes", rogzitendo.Helyes);
            cmd.Parameters.AddWithValue("@pont", rogzitendo.Pont);
            int sorokSzama = cmd.ExecuteNonQuery();
            SQLConnection.Close();
            return sorokSzama > 0 ? "Sikeres felvitel" : "Hiba a felvitel során";
        }

        public List<Eloado> EloadokListaja()
        {
            List<Eloado> eloadoLista = new List<Eloado>();
            try
            {
                BuildConnection();
                SQLConnection.Open();
                string sql = "SELECT * FROM eloado";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = SQLConnection;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Eloado eloado = new Eloado();
                    eloado.Id = reader.GetInt32("Id");
                    eloado.Nev = reader.GetString("Nev");
                    if (!reader.IsDBNull(2))
                        eloado.Nemzetiseg = reader.GetString("Nemzetiseg");
                    eloado.Szolo = reader.GetBoolean("Szolo");
                    eloadoLista.Add(eloado);
                }
                SQLConnection.Close();
                return eloadoLista;
            }
            catch (Exception ex)
            {
                Eloado hiba = new Eloado()
                {
                    Id = 0,
                    Nev = ex.Message
                };
                eloadoLista.Add(hiba);
                return eloadoLista;
            }

        }
    }