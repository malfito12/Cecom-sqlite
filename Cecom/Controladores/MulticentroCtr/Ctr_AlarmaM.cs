using Cecom.Conexion;
using Cecom.Modelos.login;
using Cecom.Modelos.MulticentroMod;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Cecom.Controladores.MulticentroCtr
{
    internal class Ctr_AlarmaM
    {
        public List<M_AlarmaM> Lista_alarmaM()
        {
            List<M_AlarmaM> alarma = new List<M_AlarmaM>();
            string consulta = "SELECT * FROM alarmas_m ORDER BY id_alarma DESC";
            using(SQLiteConnection connection= new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd= new SQLiteCommand(consulta, connection);
                try { 
                    connection.Open();
                    using(SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            M_AlarmaM alarmaM = new M_AlarmaM
                            {
                                id_alarma = reader.GetInt32(0),
                                evento = reader.GetString(1),
                                fecha = reader.GetString(2),
                                estado=reader.GetInt32(3),
                            };
                            alarma.Add(alarmaM);
                        }
                    }

                }catch(Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
            }
            return alarma;
        }

        public bool save_alarmaM(M_AlarmaM data)
        {
            bool resp = false;
            string consulta = "INSERT INTO alarmas_m (evento,fecha,estado,id_usuario) VALUES (@evento,@fecha,@estado,@id_usuario)";
            using(SQLiteConnection connection= new SQLiteConnection(Conectiondb.Instancia().conexion()) )
            {
                SQLiteCommand cmd= new SQLiteCommand( consulta, connection);
                try {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@evento", data.evento);
                    cmd.Parameters.AddWithValue("@fecha",data.fecha);
                    cmd.Parameters.AddWithValue("@estado",data.estado);
                    cmd.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(M_login.id_usuario));
                    resp = cmd.ExecuteNonQuery() >= 1?true:false;
                    
                }catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            return resp;
        }
        public bool edit_alarmaM(M_AlarmaM data)
        {
            bool resp = false;
            string consulta = "UPDATE alarmas_m SET evento=@evento,fecha=@fecha,id_usuario=@id_usuario WHERE id_alarma=@id_alarma";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@evento", data.evento);
                    cmd.Parameters.AddWithValue("@fecha", data.fecha);
                    cmd.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(M_login.id_usuario));
                    cmd.Parameters.AddWithValue("@id_alarma", data.id_alarma);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            return resp;
        }
        public bool delete_alarmaM(int id)
        {
            bool resp = false;
            string consulta = "DELETE FROM alarmas_m WHERE id_alarma=@id_alarma";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id_alarma", id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;
        }
        
        public bool updateReportSuccess(int id)
        {
            bool resp = false;
            string consulta = "UPDATE alarmas_m SET estado=@estado WHERE id_alarma=@id_alarma";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@estado", 2);
                    cmd.Parameters.AddWithValue("@id_alarma", id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            return resp;
        }
    }

}
