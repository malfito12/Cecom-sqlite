using Cecom.Conexion;
using Cecom.Modelos.login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Cecom.Controladores.login
{
    internal class Crt_login
    {
        public bool Login_data(string name, string pass)
        {
            bool resp = true;
            string consulta = "SELECT * FROM usuarios WHERE name=@name and password=@password";
            /*using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", pass);
                    cmd.CommandType = CommandType.Text;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)// devuleve true o false
                    {
                        while (reader.Read())
                        {
                            // los numeros son de la base de datos enumerados por casilla
                            M_login.id_usuario = reader.GetInt32(0);
                            M_login.name = reader.GetString(1);
                            M_login.email = reader.GetString(3);
                            M_login.rol = reader.GetString(4);
                        }
                    }
                    else resp = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }*/
            int numero = 23;
            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Access\\escuela1.accdb;"))
            {
                OleDbCommand cmd = new OleDbCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", pass);
                    cmd.CommandType = CommandType.Text;
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)// devuleve true o false
                    {
                        while (reader.Read())
                        {
                            // los numeros son de la base de datos enumerados por casilla
                            M_login.id_usuario = reader.GetInt32(0);
                            M_login.name = reader.GetString(1);
                            M_login.email = reader.GetString(3);
                            M_login.rol = reader.GetString(4);
                        }
                    }
                    else resp = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;
        }
    }
}
