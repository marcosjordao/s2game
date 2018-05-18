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

        public JogoCategoria Categoria { get; set; }

        public Produtora Produtora { get; set; }

        public DateTime? DataDeCompra { get; set; }

        public bool Ativo { get; set; }
    }
}
