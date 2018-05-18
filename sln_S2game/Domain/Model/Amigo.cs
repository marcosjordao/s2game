using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Amigo
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(70)]
        public string Nome { get; set; }

        [EmailAddress, MaxLength(60)]
        public string Email { get; set; }

        [Phone, MaxLength(15)]
        public string Fone { get; set; }

    }
}
