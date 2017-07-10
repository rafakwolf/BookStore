using System.Collections.Generic;
using BookStore.Domain.Model;

namespace BookStore.Domain.Contracts.Services
{
    public interface ILivroService
    {
        IList<LivroViewModel> GetAll(string search, int filterType);
        LivroViewModel GetById(int id);
        void Save(LivroViewModel entity);
        LivroViewModel Update(LivroViewModel entity);
        void Delete(int id);
    }
}