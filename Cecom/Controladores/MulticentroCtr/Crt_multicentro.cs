using Cecom.Conexion;
using Cecom.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using Cecom.Modelos.login;
using System.Data;



namespace Cecom.Controladores
{
    internal class Crt_multicentro
    {
        /*public string register_multicentro(M_multicentro data)
        {
            string resp = "";
            string consulta = "INSERT INTO multicentros (cuenta,nombre,departamento) VALUES ('"+data.cuenta+"','"+data.nombre+"','"+data.departamento+"')";
            using (SQLiteConnection conn = new SQLiteConnection(Conectiondb.getInstancia().CrearConexion()))
            {
                SQLiteCommand command= new SQLiteCommand(consulta,conn);
                conn.Open();
                resp=command.ExecuteNonQuery()>1?"OK":"No se pudo realizar la accion";
            }
            return resp;
        }*/
        public bool Save_multicentro(M_multicentro data)
        {
            bool resp = true;
            string consulta = "INSERT INTO multicentros (nombre, cuenta, departamento, id_usuario) VALUES (@nombre, @cuenta, @departamento, @id_usuario)";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@nombre", data.nombre.Trim());
                    cmd.Parameters.AddWithValue("@cuenta", Convert.ToInt32(data.cuenta));
                    cmd.Parameters.AddWithValue("@departamento", data.departamento.Trim());
                    cmd.Parameters.AddWithValue("@id_usuario", M_login.id_usuario);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;
        }

        public bool Lista_multicentro(DataTable lista)
        {
            bool resp = true;
            string consulta = "SELECT * FROM multicentros";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                try
                {
                    connection.Open();
                    SQLiteDataAdapter data = new SQLiteDataAdapter(consulta, connection);
                    data.Fill(lista);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    resp = false;
                }
            }
            return resp;
        }

        public bool Update_multicentro(M_multicentro data)
        {
            bool resp = false;
            string consulta = "UPDATE multicentros SET nombre=@nombre,cuenta=@cuenta,departamento=@departamento,id_usuario=@id_usuario " +
                            "WHERE id_multicentro=@id_multicentro";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta, connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@nombre", data.nombre.Trim());
                    cmd.Parameters.AddWithValue("@cuenta", Convert.ToInt32(data.cuenta));
                    cmd.Parameters.AddWithValue("@departamento", data.departamento.Trim());
                    cmd.Parameters.AddWithValue("@id_usuario", Convert.ToInt32(M_login.id_usuario));
                    cmd.Parameters.AddWithValue("@id_multicentro", data.id_multicentro);
                    resp = cmd.ExecuteNonQuery() >= 1 ? true : false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;
        }
        
        public bool delete_multicentro(string id)
        {
            bool resp=false;
            string consulta = "DELETE FROM multicentros WHERE id_multicentro=@id_multicentro";
            using (SQLiteConnection connection = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand cmd = new SQLiteCommand(consulta,connection);
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@id_multicentro", Int32.Parse(id));
                    resp = cmd.ExecuteNonQuery() >= 1 ? true:false ;

                }catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;
        }
    }
}
