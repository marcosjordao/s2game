using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Produtora
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; }
    }
}