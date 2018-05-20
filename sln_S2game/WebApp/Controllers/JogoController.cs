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
using WebApp.Data.Repository.Interfaces;

namespace WebApp.Controllers
{
    [Authorize]
    public class JogoController : Controller
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoController(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        // GET: Jogo
        public async Task<IActionResult> Index()
        {
            return View(await _jogoRepository.GetAllAsync());
        }

        // GET: Jogo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _jogoRepository.GetAsync(id.Value);
            if (jogo == null)
            {
                return NotFound();
            }
            //TODO: exibir histórico de empréstimos no Details
            return View(jogo);
        }

        // GET: Jogo/Create
        public IActionResult Create()
        {
            Jogo jogo = new Jogo();
            jogo.Ativo = true;
            return View(jogo);
        }

        // POST: Jogo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataDeCompra,Ativo,Produtora")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                await _jogoRepository.AddAsync(jogo);
                return RedirectToAction(nameof(Index));
            }
            return View(jogo);
        }

        // GET: Jogo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _jogoRepository.GetAsync(id.Value);
            if (jogo == null)
            {
                return NotFound();
            }
            return View(jogo);
        }

        // POST: Jogo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataDeCompra,Ativo,Produtora")] Jogo jogo)
        {
            if (id != jogo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _jogoRepository.UpdateAsync(jogo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogoExists(jogo.Id))
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
            return View(jogo);
        }

        // GET: Jogo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _jogoRepository.GetAsync(id.Value);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }

        // POST: Jogo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogo = await _jogoRepository.GetAsync(id);
            await _jogoRepository.DeleteAsync(jogo);
            return RedirectToAction(nameof(Index));
        }

        private bool JogoExists(int id)
        {
            return (_jogoRepository.Get(id) != null);
        }
    }
}
