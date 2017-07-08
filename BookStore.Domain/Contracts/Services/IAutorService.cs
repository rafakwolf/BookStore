using System.Collections.Generic;
using BookStore.Domain.Model;

namespace BookStore.Domain.Contracts.Services
{
    public interface IAutorService
    {
        IList<Autor> GetAll();
        Autor GetById(int id);
        void Save(Autor entity);
        Autor Update(Autor entity);
        void Delete(int id);
    }
}