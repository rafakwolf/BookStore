using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model
{
    public class Autor: Entity
    {
        [Required(ErrorMessage = "Nome do autor deve ser informado.")]
        public string Nome { get; set; }

        public string Nacionalidade { get; set; }
    }
}
