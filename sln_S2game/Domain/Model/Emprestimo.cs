using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Emprestimo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JogoId { get; set; }
        public Jogo Jogo { get; set; }

        [Required]
        public int AmigoId { get; set; }
        public Amigo Amigo { get; set; }

        [Required]
        [Display(Name = "Emprestado em")]
        public DateTime DataEmprestimo { get; set; }

        [Display(Name = "Devolvido em")]
        public DateTime? DataDevolucao { get; set; }
    }
}
