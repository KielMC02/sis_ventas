using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class DRol
    {
        public DataTable Listar()
        {
            //Data reader nos ayuda a leer una secuencias de filas en Sql SERVER
            SqlDataReader Resultado;
            //La clase datatable representa una tabla en memoria
            DataTable Tabla = new DataTable();
            SqlConnection Sqlcon = new SqlConnection();

            //Se ejecuta el codigo que queremos
            try
            {
                //Como la clase y el metodo de la conexion son privados entonces hay que llamar primero al metodo publico "getInstance", junto con el metodo de la conexion  "CrearConexion".
                Sqlcon = Conexion.getInstance().CrearConexion();
                //Utilizamos una variable de Tipo SqlCommand "Comando" la cual recibe el objeto la que haremos referencia en nuestra base de datos en este caso es un StoredProcedure(Procediiento de lamacenado) y tambien recibe la conexiona a la base de datos en este caso esa en "Sqlcon".
                //La Clase SqlCommando representa una instruccion o una Transaccion SQL.
                SqlCommand Comando = new SqlCommand("Rol_Listar", Sqlcon);
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
    }
}
