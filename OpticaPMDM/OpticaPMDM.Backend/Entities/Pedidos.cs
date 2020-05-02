using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpticaPMDM.Backend.Entities
{
    public class Pedidos
    {
        [PrimaryKey, AutoIncrement]
        public long IdPedido { get; set; }

        [NotNull]
        public string Descripcion { get; set; }

        public string Nombre_Cliente { get; set; }

        public DateTime FechaPedido { get; set; }
    }
}
