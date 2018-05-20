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
    public class EmprestimoController : Controller
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IAmigoRepository _amigoRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public EmprestimoController(IEmprestimoRepository emprestimoRepository, IJogoRepository jogoRepository, IAmigoRepository amigoRepository)
        {
            _jogoRepository = jogoRepository;
            _amigoRepository = amigoRepository;
            _emprestimoRepository = emprestimoRepository;
        }

        // GET: Emprestimo
        public async Task<IActionResult> Index()
        {
            return View(await _emprestimoRepository.GetEmprestimosVigentesAsync());
        }

        // GET: Emprestimo/Listar
        public async Task<IActionResult> Listar()
        {
            return View(await _emprestimoRepository.GetAllAsync());
        }

        // GET: Emprestimo/Create
        public IActionResult Create()
        {
            CarregarListJogo();
            CarregarListAmigo();

            Emprestimo emprestimo = new Emprestimo();
            emprestimo.DataEmprestimo = DateTime.Now.Date;
            return View(emprestimo);
        }

        // POST: Emprestimo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEmprestimo,DataDevolucao,JogoId,AmigoId")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                await _emprestimoRepository.AddAsync(emprestimo);
                return RedirectToAction(nameof(Index));
            }
            CarregarListJogo(emprestimo.JogoId);
            CarregarListAmigo(emprestimo.AmigoId);
            return View(emprestimo);
        }

        // GET: Emprestimo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _emprestimoRepository.GetAsync(id.Value);
            if (emprestimo == null)
            {
                return NotFound();
            }
            CarregarListJogo(emprestimo.JogoId);
            CarregarListAmigo(emprestimo.AmigoId);
            return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataEmprestimo,DataDevolucao,JogoId,AmigoId")] Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _emprestimoRepository.UpdateAsync(emprestimo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimoExists(emprestimo.Id))
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
            return View(emprestimo);
        }


        // GET: Emprestimo/Devolver/5
        public async Task<IActionResult> Devolver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _emprestimoRepository.GetAsync(id.Value);
            if (emprestimo == null)
            {
                return NotFound();
            }
            emprestimo.DataDevolucao = DateTime.Now.Date;
            return View(emprestimo);
        }

        // POST: Emprestimo/Devolver/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver(int id, [Bind("Id,DataDevolucao,JogoId,AmigoId")] Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _emprestimoRepository.Devolver(emprestimo,
                                                         emprestimo.DataDevolucao.Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimoExists(emprestimo.Id))
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
            return View(emprestimo);
        }


        // GET: Emprestimo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _emprestimoRepository.GetAsync(id.Value);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _emprestimoRepository.GetAsync(id);
            await _emprestimoRepository.DeleteAsync(emprestimo);
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimoExists(int id)
        {
            return (_emprestimoRepository.Get(id) != null);
        }

        private void CarregarListJogo(object selectedJogo = null)
        {
            var lstJogosAtivos = _jogoRepository.GetAtivos();

            ViewBag.JogoId = new SelectList(lstJogosAtivos, "Id", "Nome", selectedJogo);
        }

        private void CarregarListAmigo(object selectedAmigo = null)
        {
            var lstAmigos = _amigoRepository.GetAll();

            ViewBag.AmigoId = new SelectList(lstAmigos, "Id", "Nome", selectedAmigo);
        }

    }
}
