using System;
using System.ComponentModel.DataAnnotations;

namespace sorte.console
{
    public class MegasenaCounter
    {

        [Key]
        public int Id { get; set; }

        public DateTime? DataConcurso { get; set; }

        public int? Numero { get; set; }

        public int? Quantidade { get; set; }

    }
}
