using Cecom.Conexion;
using Cecom.Modelos.login;
using Cecom.Modelos.MulticentroMod;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Cecom.Controladores.MulticentroCtr
{
    internal class Ctr_ReporteAM
    {
        public bool save_reporte_am(M_ReporteAM data)
        {
            bool resp = false;
            string query = "INSERT INTO reportes_eventos_m (datos,fecha,id_alarma,id_usuario) VALUES (@datos,@fecha,@id_alarma,@id_usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@datos", data.data);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("d"));
                    cmd.Parameters.AddWithValue("@id_alarma", data.id_alarma);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { throw ex; }
            }
            return resp;
        }

        public List<M_ReporteAM> getlistaAM(int id)
        {
            List<M_ReporteAM> alarma = new List<M_ReporteAM>();
            string consulta = "SELECT * FROM reportes_eventos_m WHERE id_alarma=@id_alarma";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                cmd.Parameters.AddWithValue("@id_alarma", id);
                try
                {
                    connection.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Lee cada fila del resultado
                        {
                            //var jsonData = reader.GetString(0);
                            //var reportes = JsonSerializer.Deserialize<M_ReporteAM>(jsonData);
                            M_ReporteAM re = new M_ReporteAM { 
                                id_alarma=reader.GetInt32(0),
                                data=reader.GetString(1),
                                fecha=reader.GetString(2),
                            };
                            alarma.Add(re);

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return alarma;
        }
    }
}
