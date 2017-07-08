using System.Collections.Generic;
using BookStore.Domain.Model;

namespace BookStore.Domain.Contracts.Services
{
    public interface IGeneroService
    {
        IList<Genero> GetAll();
        Genero GetById(int id);
        void Save(Genero entity);
        Genero Update(Genero entity);
        void Delete(int id);
    }
}