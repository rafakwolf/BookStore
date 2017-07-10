using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

// ReSharper disable Mvc.ViewNotResolved

namespace BookStore.Web.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivroService _service;
        private readonly IGeneroService _generoService;
        private readonly IAutorService _autorService;

        public LivrosController(ILivroService service, 
            IGeneroService generoService, 
            IAutorService autorService)
        {
            _service = service;
            _generoService = generoService;
            _autorService = autorService;
        }

        // GET: Livros
        public async Task<IActionResult> Index(string search, int filterType)
        {
            return View(await Task.FromResult(_service.GetAll(search, filterType)));
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewBag.Autores = _autorService.GetAll();
            ViewBag.Generos = _generoService.GetAll();

            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nome,DataLancado,AutorId,GeneroId,NumeroPaginas,Corredor,Prateleira,Id")] LivroViewModel livro)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _service.Save(livro));
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Autores = _autorService.GetAll();
            ViewBag.Generos = _generoService.GetAll();

            var livro = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Nome,DataLancado,AutorId,GeneroId,NumeroPaginas,Corredor,Prateleira,Id")] LivroViewModel livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _service.Update(livro));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => _service.Delete(id));
            return RedirectToAction("Index");
        }

        private bool LivroExists(int id)
        {
            return _service.GetById(id) != null;
        }
    }
}
