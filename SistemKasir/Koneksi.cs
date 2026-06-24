using System;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemKasir
{
    public class Koneksi
    {
        public static SqlConnection GetConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["SistemKasirWarung"].ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}