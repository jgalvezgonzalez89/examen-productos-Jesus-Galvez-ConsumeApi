using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace examen_productos_Jesus_Galvez_ConsumeApi.Models
{
    public class Productos
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string categoria { get; set; }
        public string precio { get; set; }
        public int cantidad { get; set; }

    }
}