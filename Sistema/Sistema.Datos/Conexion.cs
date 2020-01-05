using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    class Conexion
    {
        //Declaramos las variables que almacenaran cada componente de la conexion
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private bool Seguridad;
        private static Conexion Con = null;

        private Conexion()
        {
            //Inicializamos todos los Atributos de la Clase
            this.Base = "dbsistema";
            this.Servidor = "DKPR8Z1";
            this.Usuario = "";
            this.Clave = "";
            this.Seguridad = true;
        }

        public SqlConnection CrearConexion()
        {
            //Creamos la cadena de conexion
            SqlConnection Cadena = new SqlConnection();
            //Utilizamos un Try catch.
            try
            {
                //Establecemos los parametros del String de conexion
                Cadena.ConnectionString = "Server=" + this.Servidor + "; Database=" + this.Base + ";";
                //Si utilizamos la seguridad de Windows, le agregamos "Integrated Security = SSPI"
                if (this.Seguridad)
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                //Si utilizamos la seguridad de SQL Server le agregamos "User Id=" +  "Password="
                else
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id=" + this.Usuario+"Password="+this.Clave;
                }
            }
            catch (Exception ex)
            {
                //Si hay algun error el contenido de Cadena sera null
                Cadena = null;
                throw ex;
                
            }
            //Retornar una variable que sea de tipo Sqlconnection
            return Cadena;
        }
        //Un objeto de la misma clase
        public static Conexion getInstance()
        {
            if (Con == null)
            {
                Con = new Conexion();

            }
            return Con;

        }
    }


}
