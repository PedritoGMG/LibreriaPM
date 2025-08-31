using PruebaLibreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class DTO_Edicion : GetConnectionDAO
    {
        public DTO_Edicion() { }
        public List<Models.Edicion> getEdiciones()
        {
            var listaEdiciones = new List<Models.Edicion>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getEdiciones()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Edicion();
                    t.id = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);

                    listaEdiciones.Add(t);
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
            return listaEdiciones;
        }
        public Models.Response putEdicion(Models.Request request)
        {
            var reso = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call putEdicion('@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                reso.OK = sql+"Edicion añadido correctamente";
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
        public Models.Response postEdicion(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call postEdicion(@id,'@nombre')";
                sql = sql.Replace("@nombre", request.Nombre);
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;

                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = sql+"Edicion actualizado con exito";

                var tema = new Models.Edicion();
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
        public Models.Response deleteEdicion(Models.Request request) { 
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call deleteEdicion(@id)";
                sql = sql.Replace("@id", request.Id.ToString());
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Edicion eliminado con exito";
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