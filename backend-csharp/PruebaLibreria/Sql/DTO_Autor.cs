using PruebaLibreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class DTO_Autor : GetConnectionDAO
    {
        public DTO_Autor() { }
        public List<Models.Autor> getAutores()
        {
            var listaAutores = new List<Models.Autor>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getAutores()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Autor();
                    t.id = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);

                    listaAutores.Add(t);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return listaAutores;
        }
        public Models.Response putAutor(Models.Request request)
        {
            var reso = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call putAutor('@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                reso.OK = sql+"Autor añadido correctamente";
            }
            catch (Exception e)
            {
                reso.Error = "Error interno en insertar: " + e.Message;
            }
            finally { 
                connection.Close();
            }
            return reso;
        }
        public Models.Response postAutor(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call postAutor(@id,'@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;

                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = sql+"Autor actualizado con exito";

                var autor = new Models.Autor();
                autor.id = request.Id;
                autor.nombre = request.Nombre;
                response.Data = autor.ToString();
            }
            catch (Exception e)
            {
                response.Error = "Error interno en actualizar: " + e.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }
        public Models.Response deleteAutor(Models.Request request) { 
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call deleteAutor(@id)";
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Autor eliminado con exito";
            }
            catch(Exception e) { 
                response.Error = "Error interno en borrar: "+e.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

    }
}