using System;

namespace BookStore.Domain.Model
{
    public class Livro: Entity
    {
        public string Nome { get; set; }

        public virtual Autor Autor { get; set; }

        public DateTime DataLancado { get; set; }

        public int NumeroPaginas { get; set; }

        public virtual Genero Genero { get; set; }

        public int Corredor { get; set; }

        public int Prateleira { get; set; }
    }
}
