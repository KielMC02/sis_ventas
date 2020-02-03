using Sistema.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
   public class NRol
    {
        //Creamos las funciones acordes a los metodos de la Clase Dcategoria
        public static DataTable Listar()
        {

            //Llamamos el metodo Listar de la clase Dcategorias
            DRol Datos = new DRol();
            return Datos.Listar();
        }
    }
}
