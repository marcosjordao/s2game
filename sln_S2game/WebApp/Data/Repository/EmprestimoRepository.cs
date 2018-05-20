using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Repository.Interfaces;

namespace WebApp.Data.Repository
{
    public class EmprestimoRepository : RepositoryBase<Emprestimo>, IEmprestimoRepository
    {
        public EmprestimoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        public new Emprestimo Get(int id)
        {
            return _dbContext.Emprestimos
                             .Include(f => f.Jogo)
                             .Include(f => f.Amigo)
                             .SingleOrDefault(f => f.Id == id);
        }

        public async new Task<Emprestimo> GetAsync(int id)
        {
            return await _dbContext.Emprestimos
                                   .Include(f => f.Jogo)
                                   .Include(f => f.Amigo)
                                   .SingleOrDefaultAsync(f => f.Id == id);
        }

        public new IEnumerable<Emprestimo> GetAll()
        {
            return _dbContext.Emprestimos
                             .Include(f => f.Jogo)
                             .Include(f => f.Amigo)
                             .OrderBy(f => f.DataEmprestimo)
                             .ToList();
        }

        public async new Task<IEnumerable<Emprestimo>> GetAllAsync()
        {
            return await _dbContext.Emprestimos
                                   .Include(f => f.Jogo)
                                   .Include(f => f.Amigo)
                                   .OrderBy(f => f.DataEmprestimo)
                                   .ToListAsync();
        }

        public IEnumerable<Emprestimo> GetEmprestimosVigentes()
        {
            return _dbContext.Emprestimos
                             .Include(f => f.Jogo)
                             .Include(f => f.Amigo)
                             .Where(f => !f.DataDevolucao.HasValue)
                             .ToList();
        }

        public async Task<IEnumerable<Emprestimo>> GetEmprestimosVigentesAsync()
        {
            return await _dbContext.Emprestimos
                                   .Include(f => f.Jogo)
                                   .Include(f => f.Amigo)
                                   .Where(f => !f.DataDevolucao.HasValue)
                                   .ToListAsync();
        }

        public async Task Devolver(Emprestimo emprestimo, DateTime dataDevolucao)
        {
            Emprestimo emprestimoDevolver = await GetAsync(emprestimo.Id);
            emprestimoDevolver.DataDevolucao = dataDevolucao;
            await UpdateAsync(emprestimoDevolver);
        }
    }
}
