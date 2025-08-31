using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class DAO
    {
        private static string conString = "Server=localhost;Database=@bbdd;Uid=@user;Pwd=@password";
        private static MySqlConnection Connection = null;
        private static DAO accessMysql;
        private static String Bbdd { get; set; } = null;
        private static String user { get; set; } = null;
        private static String password { get; set; } = null;
        public static DAO instance(String Bbdd, String user, String password)
        {
            try {
                if (accessMysql == null)
                {
                    if (DAO.Bbdd != Bbdd || DAO.user != user || DAO.password != password)
                    {
                        if (Connection != null)
                        {
                            Connection.Close();
                        }
                        createInstance(Bbdd, user, password);
                    }
                }
                else
                {
                    createInstance(Bbdd, user, password);
                }
            }
            catch (Exception ex) {
                createInstance(Bbdd, user, password);
            }
            return accessMysql;
        }

        private DAO(String bbdd, String user, String password)
        {
            try
            {
                var url = DAO.conString.Replace("@bbdd", bbdd);
                url = url.Replace("@user", user);
                url = url.Replace("@password", password);
                Connection = new MySql.Data.MySqlClient.MySqlConnection();
                Connection.ConnectionString = url;
            }
            catch (Exception e) {
                throw new Exception("Error al crear la connection" + e.Message);
            }
        }

        private static void createInstance(String bbdd, String user, String password)
        {
            accessMysql = new DAO(bbdd, user, password);
            DAO.user = user;
            DAO.password = password;
            DAO.Bbdd = bbdd;
        }
        public MySqlConnection getConnection() { return Connection; }
        public Boolean check()
        {
            if (Connection != null)
                return true;
            else 
                return false;
        }
    }
}