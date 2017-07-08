using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Domain.Contracts.Services;
using BookStore.Domain.Model;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedParameter.Local
namespace BookStore.Service
{
    public class LivroService: ILivroService
    {
        private readonly ApplicationDbContext _context;

        public LivroService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Livro> GetAll(string search, int filterType)
        {
            var livros = _context.Livros.
                Include(x => x.Autor).
                Include(a => a.Genero).
                AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                switch (filterType)
                {
                    case 0: // Nome
                        livros = livros.Where(x => x.Nome.Contains(search));
                        break;
                    case 1: // Autor
                        livros = livros.Where(x => x.Autor.Nome.Contains(search));
                        break;
                    case 2: // Genero
                        livros = livros.Where(x => x.Genero.Nome.Contains(search));
                        break;
                    case 3: // Corredor
                        livros = livros.Where(x => x.Corredor.Equals(int.Parse(search)));
                        break;
                    case 4: // Prateleira
                        livros = livros.Where(x => x.Prateleira.Equals(int.Parse(search)));
                        break;
                }
            }

            return livros.OrderBy(x => x.Nome).ToList();
        }

        public Livro GetById(int id)
        {
            return _context.Livros.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Livro entity)
        {
            Validate(entity);

            _context.Livros.Add(entity);
            _context.SaveChanges();
        }

        private static void Validate(Livro entity)
        {
            if (entity.Autor == null)
                throw new Exception("Autor do livro deve ser informado.");

            if (entity.Genero == null)
                throw new Exception("Gênero do livro deve ser informado.");
        }

        public Livro Update(Livro entity)
        {
            Validate(entity);

            _context.Livros.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var livro = GetById(id);
            if (livro == null) return;

            _context.Remove(livro);
            _context.SaveChanges();
        }
    }
}