using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NCategoria
    {
        //Creamos las funciones acordes a los metodos de la Clase Dcategoria
        public static DataTable Listar()
        {

            //Llamamos el metodo Listar de la clase Dcategorias
            DCaregoria Datos = new DCaregoria();
            return Datos.Listar(); 
        }

        public  static DataTable Buscar(string valor)
        {
            //Llamamos el metodo buscar de la clase Dcategorias y le enviamos el parametro que espera
            DCaregoria Datos = new DCaregoria();
            return Datos.Buscar(valor);
        }

        public static String Insertar (string Nombre, string Descripcion)
        {
            //Llamamos el metodo Insertar de la clase Dcategorias y le enviamos los parametros que espera
            DCaregoria Datos = new DCaregoria();
            //Llamamos el Metodo Existe de la Clase DCategoria, se encarga de evitar que no existan categorias con el mismo nombre
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La Categoria ya existe";
            }
            else
            {
                Categoria Obj = new Categoria();
                Obj.nombre = Nombre;
                Obj.descripcion = Descripcion;
                return Datos.Insertar(Obj);
            }

        }

        public static String Actualizar(int id,string NombreAnt, string Nombre, string Descripcion)
        {
            //Llamamos el metodo Actualizar de la clase Dcategorias y le enviamos los parametros que espera
            DCaregoria Datos = new DCaregoria();
            Categoria Obj = new Categoria();

            //Si la variable nombre anterior es igual al string nombre, entonces el usuario modifico los otros campos menos el nombre
            if(NombreAnt.Equals(Nombre))
            {
                Obj.idcategoria = id;
                Obj.nombre = Nombre;
                Obj.descripcion = Descripcion;
                return Datos.Actualizar(Obj);

            }
            else
            {
                //Validamos que no exista una categoria con el mismo nombre
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "Ya existe una Categoria con este nombre";
                }
                else
                {

                    Obj.idcategoria = id;
                    Obj.nombre = Nombre;
                    Obj.descripcion = Descripcion;
                    return Datos.Actualizar(Obj);
                }

            }

        }

        public static String Eliminar(int id)
        {
            //Llamamos el metodo Eliminar de la clase Dcategorias y le enviamos los parametros que espera
            DCaregoria Datos = new DCaregoria();
            return Datos.Eliminar(id);

        }
        public static String Activar(int id)
        {
            //Llamamos el metodo Activar de la clase Dcategorias y le enviamos los parametros que espera
            DCaregoria Datos = new DCaregoria();
            return Datos.Activar(id);

        }

        public static String Desactivar(int id)
        {
            //Llamamos el metodo Desactivar de la clase Dcategorias y le enviamos los parametros que espera
            DCaregoria Datos = new DCaregoria();
            return Datos.Desactivar(id);

        }

    }
}
