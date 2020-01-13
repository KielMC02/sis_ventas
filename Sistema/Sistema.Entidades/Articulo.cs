

namespace Sistema.Entidades
{
    //Esta clase representa a la tabla Articulo de la Base de Datos
    public class Articulo
    {
        public int Idarticulo { get; set; }
        public int Idcategoria { get; set; }
        public string Codigo { get; set; }
        public string nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string imagen { get; set; }
        public bool Estado { get; set; }
    }
}
