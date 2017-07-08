using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model
{
    public class Livro: Entity
    {
        public Livro()
        {
            Autor = new Autor();
            Genero = new Genero();
        }

        [Required(ErrorMessage = "Informe o nome do livro.")]
        public string Nome { get; set; }

        public Autor Autor { get; set; }

        [DisplayName("Data de Lançamento")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime DataLancado { get; set; }

        [DisplayName("Número de páginas")]
        public int NumeroPaginas { get; set; }

        [DisplayName("Gênero")]
        public Genero Genero { get; set; }

        public int Corredor { get; set; }

        public int Prateleira { get; set; }
    }
}
