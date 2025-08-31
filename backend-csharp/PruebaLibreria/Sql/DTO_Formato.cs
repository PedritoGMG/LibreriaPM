using PruebaLibreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class DTO_Formato : GetConnectionDAO
    {
        public DTO_Formato() { }
        public List<Models.Formato> getFormatos()
        {
            var listaFormatos = new List<Models.Formato>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getFormatos()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Formato();
                    t.id = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);

                    listaFormatos.Add(t);
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
            return listaFormatos;
        }
        public Models.Response putFormato(Models.Request request)
        {
            var reso = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call putFormato('@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                reso.OK = sql+"Formato añadido correctamente";
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
        public Models.Response postFormato(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call postFormato(@id,'@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;

                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = sql+"Formato actualizado con exito";

                var tema = new Models.Formato();
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
        public Models.Response deleteFormato(Models.Request request) { 
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call deleteFormato(@id)";
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Formato eliminado con exito";
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