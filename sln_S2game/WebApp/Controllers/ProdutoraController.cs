using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProdutoraController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoraController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produtora
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produtoras.ToListAsync());
        }

        // GET: Produtora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtoras
                .SingleOrDefaultAsync(m => m.Id == id);
            if (produtora == null)
            {
                return NotFound();
            }

            return View(produtora);
        }

        // GET: Produtora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Produtora produtora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtora);
        }

        // GET: Produtora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtoras.SingleOrDefaultAsync(m => m.Id == id);
            if (produtora == null)
            {
                return NotFound();
            }
            return View(produtora);
        }

        // POST: Produtora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Produtora produtora)
        {
            if (id != produtora.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoraExists(produtora.Id))
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
            return View(produtora);
        }

        // GET: Produtora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtora = await _context.Produtoras
                .SingleOrDefaultAsync(m => m.Id == id);
            if (produtora == null)
            {
                return NotFound();
            }

            return View(produtora);
        }

        // POST: Produtora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtora = await _context.Produtoras.SingleOrDefaultAsync(m => m.Id == id);
            _context.Produtoras.Remove(produtora);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoraExists(int id)
        {
            return _context.Produtoras.Any(e => e.Id == id);
        }
    }
}
