﻿using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
   public class DUsuario
    {
        //Esta funcion nos va devolver un objeto de tipo datatable ya que mostrara los datos de una tabla
        public DataTable Listar()
        {
            //DataReader nos ayuda a leer una secuencias de filas en Sql SERVER
            SqlDataReader Resultado;
            //La calse datatable representa una tabla en memoria
            DataTable Tabla = new DataTable();
            SqlConnection Sqlcon = new SqlConnection();

            //Se ejecuta el codigo que queremos
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso es un StoredProcedure(Procediiento de lamacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_listar", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que ba ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Almacenamos Dentro de la variable Resultado el resultado del procedimiento de almacenado
                Resultado = Comando.ExecuteReader();
                //La variable Tabla se carga con resultado
                Tabla.Load(Resultado);
                //Retornamos la tabla
                return Tabla;

            }
            //Controlamos el error producido
            catch (Exception ex)
            {
                //Muestra la excepcion
                throw ex;
            }
            //Este codigo siempre se ejecuta
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();
            }

        }
        //Esta funcion nos va devolver un objeto de tipo datatable ya que mostrata los datos de una tabla, revibe un string para realizar la busqueda
        public DataTable Buscar(string valor)
        {
            //Data reader nos ayuda a leer una secuencias de filas en Sql SERVER
            SqlDataReader Resultado;
            //La calse datatable representa una tabla en memoria
            DataTable Tabla = new DataTable();
            SqlConnection Sqlcon = new SqlConnection();

            //Se ejecuta el codigo que queremos
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso es un StoredProcedure(Procediiento de lamacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_buscar", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que ba ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos el valor que esta esperando el procedimiento de almacenado.... Utilizamos Parameters para indicar que es un parametro,"@valor" el nombre con el que recibira ese Parametro, el tipo de dato y de donde lo va recibir "valor"
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Almacenamos Dentro de la variable Resultado el resultado del procedimiento de almacenado
                Resultado = Comando.ExecuteReader();
                //La variable Tabla se carga con resultado
                Tabla.Load(Resultado);
                //Retornamos la tabla
                return Tabla;

            }
            //Controlamos el error producido
            catch (Exception ex)
            {
                //Muestra la excepcion
                throw ex;

            }
            //Este codigo siempre se ejecuta
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();
            }
        }
        //Funcion Login
        public DataTable Login(string email, string clave)
        {
            //Data reader nos ayuda a leer una secuencias de filas en Sql SERVER
            SqlDataReader Resultado;
            //La calse datatable representa una tabla en memoria
            DataTable Tabla = new DataTable();
            SqlConnection Sqlcon = new SqlConnection();

            //Se ejecuta el codigo que queremos
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso es un StoredProcedure(Procediiento de lamacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_Login", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que ba ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos el valor que esta esperando el procedimiento de almacenado.... Utilizamos Parameters para indicar que es un parametro,"@valor" el nombre con el que recibira ese Parametro, el tipo de dato y de donde lo va recibir "valor"
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = clave;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Almacenamos Dentro de la variable Resultado el resultado del procedimiento de almacenado
                Resultado = Comando.ExecuteReader();
                //La variable Tabla se carga con resultado
                Tabla.Load(Resultado);
                //Retornamos la tabla
                return Tabla;

            }
            //Controlamos el error producido
            catch (Exception ex)
            {
                //Muestra la excepcion
                return null;
                throw ex;

            }
            //Este codigo siempre se ejecuta
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();
            }
        }

        //El Metodo Existe de la Clase DArticulo, se encarga de evitar que no existan categorias con el mismo nombre
        public string Existe(string valor)
        {
            //Declaramos una variable de tipo String Vacia
            string Rpta = "";
            //Declaramos una variable de tipo Sqlconnection
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso  es un StoredProcedure(Procediiento de almacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_existe", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que va ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos los valores que esta esperando el Procedimiento de almacenado en la base de datos.
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                SqlParameter ParExiste = new SqlParameter();
                ParExiste.ParameterName = "@existe";
                ParExiste.SqlDbType = SqlDbType.Int;
                ParExiste.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(ParExiste);
                //Abrimos la Conexion
                Sqlcon.Open();
                //Si todo se ejecuta de forma correcta devuelve un OK en caso de que no "No se pudo guardar el registro"
                Comando.ExecuteNonQuery();
                Rpta = Convert.ToString(ParExiste.Value);
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();

            }
            return Rpta;
        }

        //
        public string Insertar(Usuario Obj)
        {
            //Declaramos una variable de tipo String Vacia
            string Rpta = "";
            //Declaramos una variable de tipo Sqlconnection
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso  es un StoredProcedure(Procediiento de almacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_insertar", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que va ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos los valores que esta esperando el Procedimiento de almacenado en la base de datos.
                Comando.Parameters.Add("@idrol", SqlDbType.Int).Value = Obj.idrol;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = Obj.tipo_documento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = Obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = Obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Obj.Email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = Obj.Clave;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Si todo se ejecuta de forma correcta devuelve un OK en caso de que no "No se pudo guardar el registro"
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo guardar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();

            }
            return Rpta;
        }

        public string Actualizar(Usuario Obj)
        {
            //Declaramos una variable de tipo String Vacia
            string Rpta = "";
            //Declaramos una variable de tipo Sqlconnection
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso  es un StoredProcedure(Procediiento de almacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_actualizar", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que va ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos los valores que esta esperando el Procedimiento de almacenado en la base de datos.
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = Obj.idusuario;
                Comando.Parameters.Add("@idrol", SqlDbType.Int).Value = Obj.idrol;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = Obj.tipo_documento;
                Comando.Parameters.Add("@num_documento", SqlDbType.VarChar).Value = Obj.NumDocumento;
                Comando.Parameters.Add("@direccion", SqlDbType.VarChar).Value = Obj.Direccion;
                Comando.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Obj.Telefono;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = Obj.Email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = Obj.Clave;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Si todo se ejecuta de forma correcta devuelve un OK en caso de que no "No se pudo guardar el registro"
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Actulizar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();

            }
            return Rpta;

        }

        public string Eliminar(int id)
        {
            //Declaramos una variable de tipo String Vacia
            string Rpta = "";
            //Declaramos una variable de tipo Sqlconnection
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso  es un StoredProcedure(Procediiento de almacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_eliminar", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que ba ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos los valores que esta esperando el Procedimiento de almacenado en la base de datos.
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = id;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Si todo se ejecuta de forma correcta devuelve un OK en caso de que no "No se pudo guardar el registro"
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Eliminar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();

            }
            return Rpta;

        }

        public string Activar(int id)
        {
            //Declaramos una variable de tipo String Vacia
            string Rpta = "";
            //Declaramos una variable de tipo Sqlconnection
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso  es un StoredProcedure(Procediiento de almacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_activar", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que ba ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos los valores que esta esperando el Procedimiento de almacenado en la base de datos.
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = id;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Si todo se ejecuta de forma correcta devuelve un OK en caso de que no "No se pudo guardar el registro"
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Activar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();

            }
            return Rpta;


        }

        public string Desactivar(int id)
        {
            //Declaramos una variable de tipo String Vacia
            string Rpta = "";
            //Declaramos una variable de tipo Sqlconnection
            SqlConnection Sqlcon = new SqlConnection();
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso  es un StoredProcedure(Procediiento de almacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("usuario_desactivar", Sqlcon);
                //Ejecutamos el metodo ComandType de la variable comando para indicarle que ba ejecutar un Procedimiento de Almacenado
                Comando.CommandType = CommandType.StoredProcedure;
                //Le enviamos los valores que esta esperando el Procedimiento de almacenado en la base de datos.
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = id;
                //Abrimos la Conexion
                Sqlcon.Open();
                //Si todo se ejecuta de forma correcta devuelve un OK en caso de que no "No se pudo guardar el registro"
                Rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo Desactivar el registro";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                //En caso de que la conexion este abierta sera cerrada
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();

            }
            return Rpta;


        }
    }
}
