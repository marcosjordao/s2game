using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Emprestimo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Jogo Jogo { get; set; }

        [Required]
        public Amigo Amigo { get; set; }

        [Required]
        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }
    }
}
