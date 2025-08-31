using PruebaLibreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class DTO_Tema : GetConnectionDAO
    {
        public DTO_Tema() { }
        public List<Models.Tema> getTemas()
        {
            var listaTemas = new List<Models.Tema>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getTemas()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Tema();
                    t.id = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);

                    listaTemas.Add(t);
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
            return listaTemas;
        }
        public Models.Response putTema(Models.Request request)
        {
            var reso = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call putTema('@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                reso.OK = sql+"Tema añadido correctamente";
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
        public Models.Response postTema(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call postTema(@id,'@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;

                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = sql+"Tema actualizado con exito";

                var tema = new Models.Tema();
                tema.id = request.Id;
                tema.nombre = request.Nombre;
                response.Data = tema.ToString();
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
        public Models.Response deleteTema(Models.Request request) { 
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call deleteTema(@id)";
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Tema eliminado con exito";
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