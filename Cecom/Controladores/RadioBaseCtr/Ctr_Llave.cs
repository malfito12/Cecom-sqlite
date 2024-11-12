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
    internal class Ctr_Llave
    {
        public bool saveLlave(M_Llave data)
        {
            bool resp = false;
            string query = "INSERT INTO llaves (codigo,fechaI,sitio,departamento,responsable,motivo,estado,id_personal,id_empresa,id_usuario) " +
                "VALUES (@codigo,@fechaI,@sitio,@departamento,@responsable,@motivo,@estado,@id_personal,@id_empresa,@id_usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@codigo", data.l_codigo);
                    cmd.Parameters.AddWithValue("@fechaI", DateTime.Now);
                    cmd.Parameters.AddWithValue("@sitio", data.l_sitio);
                    cmd.Parameters.AddWithValue("@departamento", data.l_departamento);
                    cmd.Parameters.AddWithValue("@responsable", data.l_responsable);
                    cmd.Parameters.AddWithValue("@motivo", data.l_motivo);
                    cmd.Parameters.AddWithValue("@estado", 1);
                    cmd.Parameters.AddWithValue("@id_personal", data.id_personal);
                    cmd.Parameters.AddWithValue("@id_empresa", data.id_empresa);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public List<M_Llave> getLLave()
        {
            List<M_Llave> lista = new List<M_Llave>();
            string query = "SELECT l.codigo,l.fechaI,l.sitio,l.departamento,e.alias,p.nombre,p.telefono,p.ci,l.responsable,l.motivo,u.name,l.fechaS,l.estado,l.id_llave,p.id_personal,e.id_empresa " +
                "FROM llaves as l " +
                "INNER JOIN personalTecnicos as p ON l.id_personal=p.id_personal " +
                "INNER JOIN empresas as e ON l.id_empresa= e.id_empresa " +
                "INNER JOIN usuarios as u ON l.id_usuario= u.id_usuario " +
                "ORDER BY l.codigo DESC";
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
                            //DateTime time= DateTime.Now;
                            M_Llave llave = new M_Llave
                            {
                                l_codigo = reader.GetInt32(0),
                                l_fechaI = reader.GetDateTime(1).ToString("yyyy-MM-dd"),
                                l_hora = reader.GetDateTime(1).ToString("HH:mm:ss"),
                                l_sitio = reader.GetString(2),
                                l_departamento = reader.GetString(3),
                                em_nombre = reader.GetString(4),
                                pt_nombre = reader.GetString(5),
                                pt_telefono = reader.GetInt32(6),
                                pt_ci = reader.GetString(7),
                                l_responsable = reader.GetString(8),
                                l_motivo = reader.GetString(9),
                                name = reader.GetString(10),
                                l_fechaS = reader.IsDBNull(11)? "": reader.GetString(11),
                                l_estado = reader.GetInt32(12),
                                id_llave = reader.GetInt32(13),
                                id_personal = reader.GetInt32(14),
                                id_empresa = reader.GetInt32(15),
                            };
                            lista.Add(llave);
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return lista;
        }

        public bool updateLlave(M_Llave data, int _id)
        {
            bool resp = false;
            string query = "UPDATE llaves SET sitio=@sitio,departamento=@departamento," +
                "responsable=@responsable,motivo=@motivo,id_personal=@id_personal,id_empresa=@id_empresa," +
                "id_usuario=@id_usuario WHERE id_llave=@id_llave";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@sitio", data.l_sitio);
                    cmd.Parameters.AddWithValue("@departamento", data.l_departamento);
                    cmd.Parameters.AddWithValue("@responsable", data.l_responsable);
                    cmd.Parameters.AddWithValue("@motivo", data.l_motivo);
                    cmd.Parameters.AddWithValue("@id_personal", data.id_personal);
                    cmd.Parameters.AddWithValue("@id_empresa", data.id_empresa);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    cmd.Parameters.AddWithValue("@id_llave", _id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;

                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public bool deleteLlave(int _id)
        {
            bool resp = false;
            string query = "DELETE FROM llaves WHERE id_llave=@id_llave";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("id_llave", _id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public int codigo()
        {
            int llave = 0;
            string query = "SELECT codigo FROM llaves ORDER BY id_llave DESC LIMIT 1";
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
                            llave = reader.GetInt32(0);
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return llave;
        }

        public bool CerrarLlave(int estado,int _id)
        {
            bool resp = false;
            string query = "UPDATE llaves SET estado=@estado WHERE id_llave=@id_llave";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    cmd.Parameters.AddWithValue("@id_llave", _id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;

                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }
    }
}
