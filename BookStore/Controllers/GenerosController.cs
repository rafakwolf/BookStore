using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Model;

// ReSharper disable Mvc.ViewNotResolved

namespace BookStore.Web.Controllers
{
    public class GenerosController : Controller
    {
        private readonly IGeneroService _service;


        public GenerosController(IGeneroService service)
        {
            _service = service;
        }

        // GET: Generos
        public async Task<IActionResult> Index(string search)
        {
            var generos = _service.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                generos = generos.Where(x => x.Nome.Contains(search)).ToList();
            }

            return View(await Task.FromResult(generos));
        }

        // GET: Generos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // GET: Generos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _service.Save(genero));
                return RedirectToAction("Index");
            }
            return View(genero);
        }

        // GET: Generos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        // POST: Generos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _service.Update(genero));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneroExists(genero.Id))
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
            return View(genero);
        }

        // GET: Generos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => _service.Delete(id));
            return RedirectToAction("Index");
        }

        private bool GeneroExists(int id)
        {
            return _service.GetById(id) != null;
        }
    }
}
