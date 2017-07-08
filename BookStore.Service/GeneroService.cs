using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.Contracts.Services;
using BookStore.Domain.Model;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service
{
    public class GeneroService: IGeneroService
    {
        private readonly ApplicationDbContext _context;

        public GeneroService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Genero> GetAll()
        {
            return _context.Generos.AsNoTracking().OrderBy(x => x.Nome).ToList();
        }

        public Genero GetById(int id)
        {
            return _context.Generos.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Genero entity)
        {
            _context.Generos.Add(entity);
            _context.SaveChanges();
        }

        public Genero Update(Genero entity)
        {
            _context.Generos.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var genero = GetById(id);
            if (genero == null) return;

            _context.Remove(genero);
            _context.SaveChanges();
        }
    }
}