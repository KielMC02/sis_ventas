using Sistema.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Entidades;

namespace Sistema.Negocio
{
    class NArticulo
    {

        //Creamos las funciones acordes a los metodos de la Clase Dcategoria
        public static DataTable Listar()
        {

            //Llamamos el metodo Listar de la clase Dcategorias
            DArticulo Datos = new DArticulo();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            //Llamamos el metodo buscar de la clase Dcategorias y le enviamos el parametro que espera
            DArticulo Datos = new DArticulo();
            return Datos.Buscar(valor);
        }

        public static String Insertar(int Idcategoria,string Codigo,string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string imagen)
        {
            //Llamamos el metodo Insertar de la clase Dcategorias y le enviamos los parametros que espera
            DArticulo Datos = new DArticulo();
            //Llamamos el Metodo Existe de la Clase DCategoria, se encarga de evitar que no existan categorias con el mismo nombre
            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La Articulo ya existe";
            }
            else
            {
                Articulo Obj = new Articulo();
                Obj.Idcategoria = Idcategoria;
                Obj.Codigo = Codigo;
                Obj.nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.imagen = imagen;
                return Datos.Insertar(Obj);
            }

        }

        public static String Actualizar(int id, int Idcategoria, string Codigo, string NombreAnt,  string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string imagen)
        {
            //Llamamos el metodo Actualizar de la clase Dcategorias y le enviamos los parametros que espera
            DArticulo Datos = new DArticulo();
            Articulo Obj = new Articulo();

            //Si la variable nombre anterior es igual al string nombre, entonces el usuario modifico los otros campos menos el nombre
            if (NombreAnt.Equals(Nombre))
            {
                Obj.Idarticulo = id;
                Obj.Idcategoria = Idcategoria;
                Obj.Codigo = Codigo;
                Obj.nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.imagen = imagen;
                return Datos.Actualizar(Obj);

            }
            else
            {
                //Validamos que no exista una categoria con el mismo nombre
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "Ya existe un Articulo con este nombre";
                }
                else
                {

                    Obj.Idcategoria = Idcategoria;
                    Obj.Codigo = Codigo;
                    Obj.nombre = Nombre;
                    Obj.PrecioVenta = PrecioVenta;
                    Obj.Stock = Stock;
                    Obj.Descripcion = Descripcion;
                    Obj.imagen = imagen;
                    return Datos.Actualizar(Obj);
                }

            }

        }

        public static String Eliminar(int id)
        {
            //Llamamos el metodo Eliminar de la clase Dcategorias y le enviamos los parametros que espera
            DArticulo Datos = new DArticulo();
            return Datos.Eliminar(id);

        }
        public static String Activar(int id)
        {
            //Llamamos el metodo Activar de la clase Dcategorias y le enviamos los parametros que espera
            DArticulo Datos = new DArticulo();
            return Datos.Activar(id);

        }

        public static String Desactivar(int id)
        {
            //Llamamos el metodo Desactivar de la clase Dcategorias y le enviamos los parametros que espera
            DArticulo Datos = new DArticulo();
            return Datos.Desactivar(id);

        }
    }
}
