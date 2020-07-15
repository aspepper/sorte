using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sorte.console
{
    public class Megasena
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Concurso { get; set; }
        
        [Required]
        public DateTime DataConcurso { get; set; }

        public double ArredacaoTotal { get; set; }

        public int Ganhadores { get; set; }

        public double Rateio { get; set; }

        public int GanhadoresQuina { get; set; }

        public double RateioQuina { get; set; }

        public int GanhadoresQuadra { get; set; }

        public double RateioQuadra { get; set; }

        [Column(TypeName = "char(3)")]
        public string Acumulado { get; set; }

        public double ValorAcumulado { get; set; }

        public double EstimativaPremio { get; set; }

        public double AcumuladoVirada { get; set; }

        public DateTime DataInclusao { get; set; } = DateTime.Now;

        public IList<Sorteados> NumerosSorteados { get; set; } = new List<Sorteados>();
        public IList<MegasenaCidade> Cidades { get; set; } = new List<MegasenaCidade>();

    }
}
