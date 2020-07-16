using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace sorte.console
{
    public class MegasenaCidade
    {

        public int Id { get; set; }

        public int MegasenaId { get; set; }
        public Megasena MegasenaConcurso { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Cidade { get; set; }

        [Column(TypeName = "char(2)")]
        public string? UF { get; set; }

    }
}
