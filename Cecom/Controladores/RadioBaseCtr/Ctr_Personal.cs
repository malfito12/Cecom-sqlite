using Cecom.Conexion;
using Cecom.Modelos.login;
using Cecom.Modelos.RadioBaseMod;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Controladores.RadioBaseCtr
{
    internal class Ctr_Personal
    {

        public bool savePersonal(M_Personal data)
        {
            bool resp = false;
            string query = "INSERT INTO personalTecnicos (nombre,ci,telefono,id_empresa,id_usuario) VALUES (@nombre,@ci,@telefono,@id_empresa,@id_usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@nombre", data.pt_nombre);
                    cmd.Parameters.AddWithValue("@ci", data.pt_ci);
                    cmd.Parameters.AddWithValue("@telefono", Convert.ToInt32(data.pt_telefono));
                    cmd.Parameters.AddWithValue("@id_empresa", data.id_empresa);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public List<M_Personal> getPersonal()
        {
            List<M_Personal> lista = new List<M_Personal>();
            string query = "SELECT p.id_personal,p.nombre,p.ci,p.telefono,e.nombre FROM personalTecnicos as p INNER JOIN empresas as e ON p.id_empresa=e.id_empresa";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            M_Personal data = new M_Personal
                            {
                                id_personal = reader.GetInt32(0),
                                pt_nombre = reader.GetString(1),
                                pt_ci = reader.GetString(2),
                                pt_telefono = reader.GetInt32(3),
                                em_nombre = reader.GetString(4),
                            };
                            lista.Add(data);
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }

            return lista;
        }

        public bool updatePersonal(M_Personal data, int id)
        {
            bool resp = false;
            string query = "UPDATE personalTecnicos SET nombre=@nombre,ci=@ci,telefono=@telefono,id_empresa=@id_empresa,id_usuario=@id_usuario WHERE id_personal=@id_personal";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@nombre", data.pt_nombre);
                    cmd.Parameters.AddWithValue("@ci", data.pt_ci);
                    cmd.Parameters.AddWithValue("@telefono", data.pt_telefono);
                    cmd.Parameters.AddWithValue("@id_empresa", data.id_empresa);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    cmd.Parameters.AddWithValue("@id_personal", id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public bool deletePersonal(int id)
        {
            bool resp = false;
            string query = "DELETE FROM personalTecnicos WHERE id_personal=@id_personal";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id_personal", id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }
        public M_Personal getPersonalId(string ci)
        {
            M_Personal data=null;
            string query = "SELECT p.id_personal,p.nombre,p.ci,p.telefono,e.nombre, e.id_empresa FROM personalTecnicos as p INNER JOIN empresas as e ON p.id_empresa=e.id_empresa " +
                "WHERE p.ci=@ci";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@ci", ci);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data = new M_Personal
                            {
                                id_personal = reader.GetInt32(0),
                                pt_nombre = reader.GetString(1),
                                pt_ci = reader.GetString(2),
                                pt_telefono = reader.GetInt32(3),
                                em_nombre = reader.GetString(4),
                                id_empresa=reader.GetInt32(5),
                            };
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }

            return data;
        }

    }
}
