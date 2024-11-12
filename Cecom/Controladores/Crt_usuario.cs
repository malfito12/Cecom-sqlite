using Cecom.Conexion;
using Cecom.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cecom.Controladores
{
    internal class Crt_usuario
    {
        public bool guardar_usuario(Iusuario data)
        {
            bool resp = true;
            string consulta = "INSERT INTO usuarios (name,password,email,rol) VALUES (@name,@password,@email,@rol)";
            using (SQLiteConnection con = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand comand = new SQLiteCommand(consulta, con);
                try
                {
                    con.Open();
                    comand.Parameters.AddWithValue("@name", data.name);
                    comand.Parameters.AddWithValue("@password", data.password);
                    comand.Parameters.AddWithValue("@email", data.email);
                    comand.Parameters.AddWithValue("@rol", data.rol);
                    //comand.CommandType=System.Data.CommandType.Text;
                    resp = comand.ExecuteNonQuery() >= 1 ? true : false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;

        }
        public DataTable ListaUsuarios(DataTable data)
        {
            string consulta = "SELECT * FROM usuarios;";
            DataTable l_usuarios = new DataTable();
            using (SQLiteConnection con = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                try
                {
                    con.Open();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(consulta, con);
                    adapter.Fill(data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return l_usuarios;
        }

        public bool editar_usuario (M_usuario data)
        {

            bool resp = true;
            string consulta = "UPDATE usuarios SET name=@name,password=@password,email=@email,rol=@rol WHERE id_usuario=@id_usuario";
            using (SQLiteConnection con = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand comand = new SQLiteCommand(consulta, con);
                try
                {
                    con.Open();
                    comand.Parameters.AddWithValue("@name", data.name);
                    comand.Parameters.AddWithValue("@password", data.password);
                    comand.Parameters.AddWithValue("@email", data.email);
                    comand.Parameters.AddWithValue("@rol", data.rol);
                    comand.Parameters.AddWithValue("@id_usuario", data.id_usuario);
                    //comand.CommandType=System.Data.CommandType.Text;
                    resp = comand.ExecuteNonQuery() >= 1 ? true : false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;
        }
        

        public bool eliminar_usuario(string id)
        {
            bool resp = true;
            string consulta = "DELETE FROM usuarios WHERE id_usuario=@id_usuario";
            using(SQLiteConnection con = new SQLiteConnection(Conectiondb.Instancia().conexion()))
            {
                SQLiteCommand command= new SQLiteCommand(consulta, con);
                try
                {
                    con.Open();
                    command.Parameters.AddWithValue("id_usuario",Int32.Parse(id));
                    resp=command.ExecuteNonQuery() >= 1 ? true:false;

                }catch (Exception ex) {
                    MessageBox.Show(ex.ToString());
                }
            }
            return resp;
        }
    }
}
