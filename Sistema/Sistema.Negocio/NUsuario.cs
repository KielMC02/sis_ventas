using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;

namespace Sistema.Negocio
{
    //Creamos las funciones acordes a los metodos de la Clase Dcategoria
    public class NUsuario
    {

        public static DataTable Listar()
        {

            //Llamamos el metodo Listar de la clase Dcategorias
            DUsuario Datos = new DUsuario();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            //Llamamos el metodo buscar de la clase DUsario y le enviamos el parametro que espera
            DUsuario Datos = new DUsuario();
            return Datos.Buscar(valor);
        }

        public static DataTable Login(string email,string clave)
        {
            //Llamamos el metodo Login de la clase DUsuario y le enviamos el parametro que espera
            DUsuario Datos = new DUsuario();
            return Datos.Login(email,clave);
        }

        public static String Insertar(int Idrol,string Nombre, string tipo_documento, string num_documento, string direccion, string telefono, string email, string clave)
        {
            //Llamamos el metodo Insertar de la clase Dcategorias y le enviamos los parametros que espera
            DUsuario Datos = new DUsuario();
            //Llamamos el Metodo Existe de la Clase DCategoria, se encarga de evitar que no existan categorias con el mismo nombre
            string Existe = Datos.Existe(email);
            if (Existe.Equals("1"))
            {
                return "El usuario ya existe";
            }
            else
            {
                Usuario Obj = new Usuario();
                Obj.idrol = Idrol;
                Obj.nombre = Nombre;
                Obj.tipo_documento = tipo_documento;
                Obj.NumDocumento = num_documento;
                Obj.Direccion = direccion;
                Obj.Telefono = telefono;
                Obj.Email = email;
                Obj.Clave = clave;
            
                return Datos.Insertar(Obj);
            }

        }

        public static String Actualizar(int Idusuario, int Idrol, string Nombre, string tipo_documento, string num_documento, string direccion,string emailant, string telefono, string email, string clave)
        {
            //Llamamos el metodo Actualizar de la clase Dcategorias y le enviamos los parametros que espera
            DUsuario Datos = new DUsuario();
            Usuario Obj = new Usuario();

            //Si la variable nombre anterior es igual al string nombre, entonces el usuario modifico los otros campos menos el nombre
            if (emailant.Equals(email))
            {
                
                Obj.idusuario = Idusuario;
                Obj.idrol = Idrol;
                Obj.nombre = Nombre;
                Obj.tipo_documento = tipo_documento;
                Obj.NumDocumento = num_documento;
                Obj.Direccion = direccion;
                Obj.Telefono = telefono;
                Obj.Email = email;
                Obj.Clave = clave;
                return Datos.Actualizar(Obj);

            }
            else
            {
                //Validamos que no exista una categoria con el mismo nombre
                string Existe = Datos.Existe(email);
                if (Existe.Equals("1"))
                {
                    return "Ya existe un Usuario con este nombre";
                }
                else
                {

                    Obj.idusuario = Idusuario;
                    Obj.idrol = Idrol;
                    Obj.nombre = Nombre;
                    Obj.tipo_documento = tipo_documento;
                    Obj.NumDocumento = num_documento;
                    Obj.Direccion = direccion;
                    Obj.Telefono = telefono;
                    Obj.Email = email;
                    Obj.Clave = clave;
                    return Datos.Actualizar(Obj);
                }

            }

        }

        public static String Eliminar(int id)
        {
            //Llamamos el metodo Eliminar de la clase Dcategorias y le enviamos los parametros que espera
            DUsuario Datos = new DUsuario();
            return Datos.Eliminar(id);

        }
        public static String Activar(int id)
        {
            //Llamamos el metodo Activar de la clase Dcategorias y le enviamos los parametros que espera
            DUsuario Datos = new DUsuario();
            return Datos.Activar(id);

        }

        public static String Desactivar(int id)
        {
            //Llamamos el metodo Desactivar de la clase Dcategorias y le enviamos los parametros que espera
            DUsuario Datos = new DUsuario();
            return Datos.Desactivar(id);

        }
    }
}
