using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpticaPMDM.Backend.Entities
{
    public class Usadas
    {
        [PrimaryKey]
        public long IdUsadas { get; set; }

        [NotNull]
        public DateTime FechaEstreno { get; set; }

        [NotNull]
        public DateTime FechaCaducidad { get; set; }
    }
}
