using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Jogo
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }

        [MaxLength(50)]
        public string Produtora { get; set; }

        [Display(Name = "Data de Compra")]
        [DataType(DataType.Date)]
        public DateTime? DataDeCompra { get; set; }

        public bool Ativo { get; set; }
    }
}
