using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaLibreria.Sql
{
    public class DTO_Libro : GetConnectionDAO
    {
        public DTO_Libro() { }
        public List<Models.Libro> getLibros()
        {
            var libros = new List<Models.Libro>();
            try
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"call getLibros()";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Models.Libro();
                    t.id = reader.GetFieldValue<int>(0);
                    t.nombre = reader.GetFieldValue<string>(1);
                    t.isbn = reader.GetFieldValue<string>(2);
                    t.tema = reader.GetFieldValue<string>(3);
                    t.formato = reader.GetFieldValue<string>(4);
                    t.autor = reader.GetFieldValue<string>(5);
                    t.edicion = reader.GetFieldValue<string>(6);
                    t.precio = reader.GetFieldValue<float>(7);
                    t.cantidad = reader.GetFieldValue<int>(8);
                    t.imgname = reader.GetFieldValue<string>(9);

                    libros.Add(t);
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
            return libros;
        }
        public Models.Response putLibro(Models.Request request)
        {
            var reso = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call putLibro('@nombre','@autor','@tema','@precio','@edicion','@formato','@isbn',@cantidad,'@imgname')";
                sql = sql.Replace("@nombre", request.Nombre);
                sql = sql.Replace("@autor", request.Autor);
                sql = sql.Replace("@tema", request.Tema);
                sql = sql.Replace("@precio", request.Precio.ToString());
                sql = sql.Replace("@edicion", request.Edicion);
                sql = sql.Replace("@formato", request.Formato);
                sql = sql.Replace("@isbn", request.Isbn);
                if (request.Cantidad.ToString() is null)
                {
                    sql = sql.Replace("@cantidad", "1");
                }
                else
                {
                    sql = sql.Replace("@cantidad", request.Cantidad.ToString());
                }
                sql = sql.Replace("@imgname", request.ImgName);
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                reso.OK = sql + "Libro añadido correctamente";
            }
            catch (Exception e)
            {
                reso.Error = "Error interno en insertar: " + e.Message;
            }
            finally
            {
                connection.Close();
            }
            return reso;
        }
        public Models.Response postLibro(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call postLibro('@nombre','@autor','@tema','@precio','@edicion','@formato','@isbn','@imgname')";
                sql = sql.Replace("@nombre", request.Nombre);
                sql = sql.Replace("@autor", request.Autor);
                sql = sql.Replace("@tema", request.Tema);
                sql = sql.Replace("@precio", request.Precio.ToString());
                sql = sql.Replace("@edicion", request.Edicion);
                sql = sql.Replace("@formato", request.Formato);
                sql = sql.Replace("@isbn", request.Isbn);
                sql = sql.Replace("@imgname", request.ImgName);

                cmd.CommandText = sql;

                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = sql + "Libro actualizado con exito";

                var tema = new Models.Libro();
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
        public Models.Response deleteLibro(Models.Request request)
        {
            var response = new Models.Response();
            try
            {
                var cmd = connection.CreateCommand();
                var sql = @"call deleteLibro('@isbn')";
                sql = sql.Replace("@isbn", request.Isbn);
                cmd.CommandText = sql;
                connection.Open();
                cmd.ExecuteNonQuery();
                response.OK = "Libro eliminado con exito";
            }
            catch (Exception e)
            {
                response.Error = "Error interno en borrar: "+ request.Isbn + e.Message;
            }
            finally
            {
                connection.Close();
            }
            return response;
        }

    }
}