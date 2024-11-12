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
    internal class Ctr_Empresa
    {
        public bool saveEmpresa(M_Empresa data)
        {
            bool resp=false;
            string query = "INSERT INTO empresas (nombre,alias,direccion,telefono,id_usuario) VALUES (@nombre,@alias,@direccion,@telefono,@id_usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@nombre",data.em_nombre);
                    cmd.Parameters.AddWithValue("@alias",data.em_alias);
                    cmd.Parameters.AddWithValue("@direccion",data.em_direccion);
                    cmd.Parameters.AddWithValue("@telefono",data.em_telefono);
                    cmd.Parameters.AddWithValue("@id_usuario",M_login.id_usuario);
                    resp=cmd.ExecuteNonQuery()>=1?true:false;
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
                return resp;
        }

        public List<M_Empresa> getEmpresa()
        {
            List<M_Empresa> lista= new List<M_Empresa> ();
            string query = "SELECT * FROM empresas";
            using(SQLiteConnection connection= new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd= new SQLiteCommand(query, connection);
                try {
                    connection.Open();
                    using (SQLiteDataReader reader= cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            M_Empresa empresa = new M_Empresa
                            {
                                id_empresa = reader.GetInt32(0),
                                em_nombre = reader.GetString(1),
                                em_alias = reader.GetString(2),
                                em_direccion = reader.GetString(3),
                                em_telefono = reader.GetString(4),
                            };
                            lista.Add(empresa);
                        }
                    }
                }catch(Exception ex) { Console.WriteLine(ex); }
            }
            return lista;
        }

        public bool updateEmpresa(M_Empresa data, int _id)
        {
            bool resp = false;
            string query = "UPDATE empresas SET nombre=@nombre,alias=@alias,direccion=@direccion,telefono=@telefono,id_usuario=@id_usuario WHERE id_empresa=@id_empresa";
            using(SQLiteConnection connection= new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd= new SQLiteCommand(query, connection);
                try {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@nombre",data.em_nombre);
                    cmd.Parameters.AddWithValue("@alias",data.em_alias);
                    cmd.Parameters.AddWithValue("@direccion",data.em_direccion);
                    cmd.Parameters.AddWithValue("@telefono",data.em_telefono);
                    cmd.Parameters.AddWithValue("@id_usuario",M_login.id_usuario);
                    cmd.Parameters.AddWithValue("@id_empresa",_id);
                    resp=cmd.ExecuteNonQuery()>=1?true:false;
                }catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }

        public bool deleteEmpresa(int _id)
        {
            bool resp = false;
            string query = "DELETE FROM empresas WHERE id_empresa=@id_empresa";
            using(SQLiteConnection connection= new SQLiteConnection(Conectiondb.Instancia().conexion())) {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                try {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id_empresa", _id);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }catch (Exception ex) { Console.WriteLine(ex); }
            }
            return resp;
        }
    }
}
