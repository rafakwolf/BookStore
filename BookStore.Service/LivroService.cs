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

        private LivroViewModel ToViewModel(Livro livro)
        {
            return new LivroViewModel
            {
                Id = livro.Id,
                Nome = livro.Nome,
                Autor = livro.Autor,
                AutorId = livro.Autor.Id,
                Genero = livro.Genero,
                GeneroId = livro.Genero.Id,
                Corredor = livro.Corredor,
                Prateleira = livro.Prateleira,
                DataLancado = livro.DataLancado,
                NumeroPaginas = livro.NumeroPaginas
            };
        }

        private Livro FromViewModel(LivroViewModel livro)
        {
            var model = new Livro
            {
                Nome = livro.Nome,
                Corredor = livro.Corredor,
                Prateleira = livro.Prateleira,
                DataLancado = livro.DataLancado,
                NumeroPaginas = livro.NumeroPaginas,
                Autor = _context.Autores.FirstOrDefault(x => x.Id == livro.AutorId),
                Genero = _context.Generos.FirstOrDefault(x => x.Id == livro.GeneroId)
            };

            return model;
        }

        public IList<LivroViewModel> GetAll(string search, int filterType)
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

            var viewModelList = new List<LivroViewModel>();

            livros.ToList().ForEach(item=>
            {
                viewModelList.Add(ToViewModel(item));
            });

            return viewModelList.OrderBy(x => x.Nome).ToList();
        }

        public LivroViewModel GetById(int id)
        {
            var livro = _context.Livros.
                Include(x => x.Autor).
                Include(a => a.Genero).
                FirstOrDefault(x => x.Id == id);

            return ToViewModel(livro);
        }

        public void Save(LivroViewModel entity)
        {
            var model = FromViewModel(entity);

            Validate(model);

            _context.Livros.Add(model);
            _context.SaveChanges();
        }

        private static void Validate(Livro entity)
        {
            if (entity.Autor == null)
                throw new Exception("Autor do livro deve ser informado.");

            if (entity.Genero == null)
                throw new Exception("Gênero do livro deve ser informado.");
        }

        public LivroViewModel Update(LivroViewModel entity)
        {
            var dbModel = new Livro();

            if (dbModel.Autor == null) dbModel.Autor = new Autor();
            dbModel.Autor.Id = entity.AutorId;



            if (dbModel.Genero == null) dbModel.Genero = new Genero();
            dbModel.Genero.Id = entity.GeneroId;
            

            dbModel.Nome = entity.Nome;
            dbModel.Corredor = entity.Corredor;
            dbModel.NumeroPaginas = entity.NumeroPaginas;
            dbModel.Prateleira = entity.Prateleira;

            dbModel.Id = entity.Id;

            Validate(dbModel);

            _context.ChangeTracker.TrackGraph(dbModel, e =>
            {
                e.Entry.State = EntityState.Unchanged;

                if ((e.Entry.Entity as Livro) == null) return;

                _context.Entry((Livro) e.Entry.Entity).Property("AutorId").IsModified = true;
                _context.Entry((Livro) e.Entry.Entity).Property("GeneroId").IsModified = true;
                _context.Entry((Livro) e.Entry.Entity).Property("Id").IsModified = false;
            });

            _context.Livros.Update(dbModel);
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