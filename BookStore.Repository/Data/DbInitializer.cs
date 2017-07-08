using System;
using BookStore.Domain.Model;

namespace BookStore.Repository.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();


            // Seed para autores

            var autores = new Autor[]
            {
                new Autor {Nome = "Paulo Coelho", Nacionalidade = "Brasileiro"},
                new Autor {Nome = "Machado de Assis", Nacionalidade = "Brasileiro"}
            };

            foreach (var autor in autores)
            {
                context.Autores.Add(autor);
            }

            context.SaveChanges();



            // Seed para generos

            var generos = new Genero[]
            {
                new Genero {Nome = "Literatura"},
                new Genero {Nome = "Religioso"},
                new Genero {Nome = "Romance"}
            };

            foreach (var genero in generos)
            {
                context.Generos.Add(genero);
            }

            context.SaveChanges();

            // Seed para livros

            var livros = new Livro[]
            {
                new Livro
                {
                    Nome = "Dom Casmurro",
                    NumeroPaginas = 200,
                    Autor = autores[0],
                    Genero = generos[0],
                    Corredor = 1,
                    DataLancado = new DateTime(1899, 1, 1),
                    Prateleira = 10
                },
                new Livro
                {
                    Nome = "O Alquimista",
                    NumeroPaginas = 200,
                    Autor = autores[1],
                    Genero = generos[2],
                    Corredor = 1,
                    DataLancado = new DateTime(1988, 1, 1),
                    Prateleira = 15
                }
            };

            foreach (var livro in livros)
            {
                context.Livros.Add(livro);
            }

            context.SaveChanges();
        }
    }
}