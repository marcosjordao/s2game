using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class JogoCategoria
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Descricao { get; set; }
    }
}