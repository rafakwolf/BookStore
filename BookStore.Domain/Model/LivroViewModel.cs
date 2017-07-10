using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model
{
    public class LivroViewModel: Entity
    {
        [Required(ErrorMessage = "Informe o nome do livro.")]
        public string Nome { get; set; }

        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        [DisplayName("Data de Lançamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataLancado { get; set; }

        [DisplayName("Número de páginas")]
        public int NumeroPaginas { get; set; }

        public int GeneroId { get; set; }
        [DisplayName("Gênero")]
        public Genero Genero { get; set; }

        public int Corredor { get; set; }

        public int Prateleira { get; set; }
    }
}