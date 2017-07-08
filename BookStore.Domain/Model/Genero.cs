using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model
{
    public class Genero: Entity
    {
        [Required(ErrorMessage = "Nome do gênero deve ser informado.")]
        public string Nome { get; set; }
    }
}
