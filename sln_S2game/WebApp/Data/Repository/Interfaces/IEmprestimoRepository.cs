using Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Data.Repository.Interfaces
{
    public interface IEmprestimoRepository : IRepositoryBase<Emprestimo>
    {
        IEnumerable<Emprestimo> GetEmprestimosVigentes();
        Task<IEnumerable<Emprestimo>> GetEmprestimosVigentesAsync();
        Task Devolver(Emprestimo emprestimo, DateTime dataDevolucao);
    }
}
