using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sorte.Lib
{
    public class Megasena
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Concurso { get; set; }
        
        [Required]
        public DateTime DataConcurso { get; set; }

        public DateTime DataInclusao { get; set; } = DateTime.Now;

        public IList<Sorteados> NumerosSorteados { get; set; } = new List<Sorteados>();

    }
}
