using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sorte.Lib
{
    public class Sorteados
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int Ordem { get; set; }

        public Megasena MegaSena { get; set; }
        
    }
}
