using System.Collections.Generic;
using BookStore.Domain.Model;

namespace BookStore.Domain.Contracts.Services
{
    public interface ILivroService
    {
        IList<Livro> GetAll(string search, int filterType);
        Livro GetById(int id);
        void Save(Livro entity);
        Livro Update(Livro entity);
        void Delete(int id);
    }
}