using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
{
    public class Usuario
    {
        public int id {get; set;}
        public string nombre { get; set; }
        string tipo_documento { get; set; }
        public string NumDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public bool Estado { get; set; }
    }
}
