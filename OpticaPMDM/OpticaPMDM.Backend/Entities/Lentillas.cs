using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpticaPMDM.Backend.Entities
{
    public class Lentillas
    {
        [PrimaryKey, AutoIncrement]
        public long IdLentillas { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        [NotNull]
        public int Duracion { get; set; }

        [NotNull]
        public int Num_Pares { get; set; }

        public int Dias_Restantes { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaFinal { get; set; }
    }
}
