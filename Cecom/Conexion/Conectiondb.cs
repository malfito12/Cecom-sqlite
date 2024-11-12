using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecom.Conexion
{
    public class Conectiondb
    {
        //private static Conectiondb _instancia = null;
        //private static string data = "./cecomDB.db";
        //string conectionString = $"Data Source={data};Version=3;";
        private static Conectiondb _instancia = null;
        private static string data = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cecomDB.db");
        private string conectionString = $"Data Source={data};Version=3;Read Only=False;";
        private static string data2 = @"D:\\Access\\escuela1.accdb;";
        private string conectionStringAccess = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={data2}";

        public OleDbConnection accessConnection()
        {
            OleDbConnection con = new OleDbConnection(conectionStringAccess);
            return con;
        }

        public SQLiteConnection conexion()
        {
            SQLiteConnection con = new SQLiteConnection(conectionString);
            
            return con;

        }

        public static Conectiondb Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new Conectiondb();
            }
            return _instancia;
        }
    }
}
