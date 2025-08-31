using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class GetConnectionDAO
    {
        protected DAO accessMysql;
        protected MySqlConnection connection;
        protected GetConnectionDAO()
        {
            try
            {
                accessMysql = DAO.instance("libreriaapi", "root", "");
                connection = accessMysql.getConnection();
            }
            catch (Exception ex) {
                throw new Exception("Error al crear connection getConnection");
            }
        }
    }
}