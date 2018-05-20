using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Data.Repository;
using WebApp.Data.Repository.Interfaces;

namespace WebApp.Controllers
{
    [Authorize]
    public class AmigoController : Controller
    {

        private readonly IAmigoRepository _amigoRepository;

        public AmigoController(IAmigoRepository AmigoRepository)
        {
            _amigoRepository = AmigoRepository;
        }

        // GET: Amigo
        public async Task<IActionResult> Index()
        {
            return View(await _amigoRepository.GetAllAsync());
        }

        // GET: Amigo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigo = await _amigoRepository.GetAsync(id.Value);
            if (amigo == null)
            {
                return NotFound();
            }

            //TODO: exibir histórico de empréstimos no Details
            return View(amigo);
        }

        // GET: Amigo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Fone")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                await _amigoRepository.AddAsync(amigo);
                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        // GET: Amigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigo = await _amigoRepository.GetAsync(id.Value);
            if (amigo == null)
            {
                return NotFound();
            }
            return View(amigo);
        }

        // POST: Amigo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Fone")] Amigo amigo)
        {
            if (id != amigo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _amigoRepository.UpdateAsync(amigo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmigoExists(amigo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        // GET: Amigo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigo = await _amigoRepository.GetAsync(id.Value);
            if (amigo == null)
            {
                return NotFound();
            }

            return View(amigo);
        }

        // POST: Amigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amigo = await _amigoRepository.GetAsync(id);
            await _amigoRepository.DeleteAsync(amigo);
            return RedirectToAction(nameof(Index));
        }

        private bool AmigoExists(int id)
        {
            return (_amigoRepository.Get(id) != null);
        }
    }
}
