using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
using PruebaLibreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class DTO_User : GetConnectionDAO
    {
        public DTO_User() { }

        private string cryptoPW(string str) {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public Models.Response postUser(Models.Request request) {

            var response = new Models.Response();
            try {

                var crypt = cryptoPW(request.PW);

                var cmd = connection.CreateCommand();
                var sql = @"call postUser('@name','@apellidos','@cp','@direccion','@poblacion','@dni','@email','@pw')";
                sql = sql.Replace("@name", request.Nombre);
                sql = sql.Replace("@apellidos", request.Apellidos);
                sql = sql.Replace("@cp", request.CP);
                sql = sql.Replace("@direccion", request.Direccion);
                sql = sql.Replace("@poblacion", request.Poblacion);
                sql = sql.Replace("@dni", request.DNI);
                sql = sql.Replace("@email", request.Email);
                sql = sql.Replace("@pw", crypt);
                cmd.CommandText = sql;

                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Usuario creado con exito";
            }
            catch (Exception ex) {
                response.Error = "Error interno en insertar usuario: " + ex.Message;
            } finally {
                connection.Close();
            }
            return response;
        }
        public Response postBank(Models.Request request)
        {
            var response = new Response();

            try
            {
                using (var cmd = connection.CreateCommand())
                {
                    // Usamos DNI como referencia para la relación con los datos bancarios
                    cmd.CommandText = "CALL postBank(@dni, @code, @cvv, @titular)";
                    cmd.Parameters.AddWithValue("@dni", request.DNI); // Pasamos el DNI como identificador
                    cmd.Parameters.AddWithValue("@code", request.NumberCard ?? "");
                    cmd.Parameters.AddWithValue("@cvv", request.CVV ?? "");
                    cmd.Parameters.AddWithValue("@titular", request.NombreTitular ?? "");

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    response.OK = "Datos bancarios registrados";
                }
            }
            catch (Exception ex)
            {
                response.Error = "Error interno al insertar datos bancarios: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return response;
        }

        public Models.Response logIn(Models.Request request) {

            var response = new Models.Response();
            try
            {

                var crypt = cryptoPW(request.PW);

                var cmd = connection.CreateCommand();
                var sql = @"call isUserRegister('@email','@pw')";
                sql = sql.Replace("@email", request.Email);
                sql = sql.Replace("@pw", crypt);
                cmd.CommandText = sql;

                connection.Open();
                var reader = cmd.ExecuteReader();
                var cliente = new Cliente();
                while (reader.Read())
                {
                    cliente.nombre = reader.GetFieldValue<string>(0);
                    cliente.apellidos = reader.GetFieldValue<string>(1);
                    cliente.cp = reader.GetFieldValue<string>(2);
                    cliente.direccion = reader.GetFieldValue<string>(3);
                    cliente.poblacion = reader.GetFieldValue<string>(4);
                    cliente.dni = reader.GetFieldValue<string>(5);
                    cliente.email = reader.GetFieldValue<string>(6);
                    //cliente.id = reader.GetFieldValue<int>(8);
                }

                response.OK = "Usuario existe";
                response.Data = cliente.ToString();

            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

        public int isAdmin(Models.Request request)
        {

            int response = 0;
            try
            {

                var cmd = connection.CreateCommand();
                var sql = @"call isAdmin('@p_email')";
                sql = sql.Replace("@p_email", request.Email);
                cmd.CommandText = sql;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        response = reader.GetInt32("resultado");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return response;
        }
    }
}