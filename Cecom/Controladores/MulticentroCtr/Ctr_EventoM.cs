using Cecom.Conexion;
using Cecom.Modelos;
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
    internal class Ctr_EventoM
    {
        public List<M_EventoM> lista_eventosM(int id)
        {
            List<M_EventoM> _lista = new List<M_EventoM>();
            string consulta = "SELECT m.nombre, m.cuenta,e.hora,e.fecha,m.departamento,e.tipo,u.name,e.observaciones,e.id_evento,m.id_multicentro,a.id_alarma " +
                "FROM eventos_m as e " +
                "INNER JOIN multicentros as m ON e.id_multicentro=m.id_multicentro " +
                "INNER JOIN alarmas_m as a ON e.id_alarma= a.id_alarma " +
                "INNER JOIN usuarios as u ON e.id_usuario= u.id_usuario " +
                "WHERE e.id_alarma=@id_alarma";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id_alarma", Convert.ToInt32(id));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            M_EventoM evento = new M_EventoM
                            {
                                nombre = reader.GetString(0),
                                cuenta = reader.GetInt32(1),
                                hora_e = reader.GetString(2),
                                fecha_e = reader.GetString(3),
                                departamento = reader.GetString(4),
                                tipo = reader.GetString(5),
                                name = reader.GetString(6),
                                observaciones = reader.GetString(7),
                                id_evento = reader.GetInt32(8),
                                id_multicentro = reader.GetInt32(9),
                                id_alarma = reader.GetInt32(10),
                            };
                            _lista.Add(evento);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                return _lista;
            }
        }
        public bool save_eventoM(M_EventoM data)
        {
            bool resp = false;
            string consulta = "INSERT INTO eventos_m (hora,fecha,tipo,observaciones,id_alarma,id_multicentro,id_usuario)" +
                            " VALUES (@hora,@fecha,@tipo,@observaciones,@id_alarma,@id_multicentro,@id_usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@hora", data.hora_e);
                    cmd.Parameters.AddWithValue("@fecha", data.fecha_e);
                    cmd.Parameters.AddWithValue("@tipo", data.tipo);
                    cmd.Parameters.AddWithValue("@observaciones", data.observaciones);
                    cmd.Parameters.AddWithValue("@id_alarma", data.id_alarma);
                    cmd.Parameters.AddWithValue("@id_multicentro", data.id_multicentro);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;

                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public bool update_eventoM(M_EventoM data)
        {
            bool resp = false;
            string consulta = "UPDATE eventos_m SET " +
                            "tipo=@tipo,observaciones=@observaciones,id_alarma=@id_alarma,id_multicentro=@id_multicentro,id_usuario=@id_usuario " +
                            "WHERE id_evento=@id_evento";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    //cmd.Parameters.AddWithValue("@hora", data.hora_e);
                    //cmd.Parameters.AddWithValue("@fecha", data.fecha_e);
                    cmd.Parameters.AddWithValue("@tipo", data.tipo);
                    cmd.Parameters.AddWithValue("@observaciones", data.observaciones);
                    cmd.Parameters.AddWithValue("@id_alarma", data.id_alarma);
                    cmd.Parameters.AddWithValue("@id_multicentro", data.id_multicentro);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    cmd.Parameters.AddWithValue("@id_evento", data.id_evento);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            return resp;
        }

        public bool delete_eventoM(string id)
        {
            bool resp = false;
            string consulta = "DELETE FROM eventos_m WHERE id_evento=@id_evento";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id_evento", Convert.ToInt32(id));
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public M_multicentro search_m(string cuenta)
        {
            M_multicentro m = null;
            string consulta = "SELECT cuenta,nombre,departamento,id_multicentro FROM multicentros  WHERE cuenta=@cuenta";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@cuenta", Convert.ToInt32(cuenta));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            m = new M_multicentro();
                            m.cuenta = reader.GetInt32(0);
                            m.nombre = reader.GetString(1);
                            m.departamento = reader.GetString(2);
                            m.id_multicentro = reader.GetInt32(3);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ssageBox.Show(ex.ToString());
                    //throw ex;
                    Console.WriteLine(ex);
                    m = null;
                }
            }
            return m;
        }

        public bool successReporte(int id)
        {
            bool resp = false;
            string query = "DELETE FROM eventos_m WHERE id_alarma=@id_alarma";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id_alarma",id);
                    resp=cmd.ExecuteNonQuery()>=1?true:false;
                    
                }catch (Exception ex) { throw ex; }
            }
            return resp;
        }
    }

}
