using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Model;

// ReSharper disable Mvc.ViewNotResolved
namespace BookStore.Web.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutorService _service;

        public AutoresController(IAutorService service)
        {
            _service = service;    
        }

        // GET: Autores
        public async Task<IActionResult> Index(string search)
        {
            var autores = _service.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                autores = autores.Where(x => x.Nome.Contains(search)).ToList();
            }

            return View(await Task.FromResult(autores));
        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await Task.FromResult( _service.GetById(id.GetValueOrDefault()) );
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Nacionalidade,Id")] Autor autor)
        {
            if (ModelState.IsValid)
            {

                await Task.Run(()=>_service.Save(autor));
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Nacionalidade,Id")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _service.Update(autor));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
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
            return View(autor);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await Task.FromResult(_service.GetById(id.GetValueOrDefault()));
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => _service.Delete(id));
            return RedirectToAction("Index");
        }

        private bool AutorExists(int id)
        {
            return (_service.GetById(id) != null);
        }
    }
}
