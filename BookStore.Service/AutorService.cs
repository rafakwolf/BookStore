using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.Contracts.Services;
using BookStore.Domain.Model;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service
{
    public class AutorService: IAutorService
    {
        private readonly ApplicationDbContext _context;

        public AutorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Autor> GetAll()
        {
            return _context.Autores.AsNoTracking().OrderBy(x => x.Nome).ToList();
        }

        public Autor GetById(int id)
        {
            return _context.Autores.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Autor entity)
        {
            _context.Autores.Add(entity);
            _context.SaveChanges();
        }

        public Autor Update(Autor entity)
        {
            _context.Autores.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var autor = GetById(id);

            if (autor == null) return;

            _context.Autores.Remove(autor);
            _context.SaveChanges();
        }
    }
}