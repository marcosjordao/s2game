using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Repository.Interfaces;

namespace WebApp.Data.Repository
{
    public class JogoRepository : RepositoryBase<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        public new Jogo Get(int id)
        {
            return _dbContext.Jogos
                             .Include(f => f.Emprestimos)
                                .ThenInclude(e => e.Amigo)
                             .SingleOrDefault(f => f.Id == id);
        }

        public async new Task<Jogo> GetAsync(int id)
        {
            return await _dbContext.Jogos
                                   .Include(f => f.Emprestimos)
                                       .ThenInclude(e => e.Amigo)
                                   .SingleOrDefaultAsync(f => f.Id == id);
        }

        public IEnumerable<Jogo> GetDisponiveis()
        {
            return _dbContext.Jogos
                             .Include(f => f.Emprestimos)
                             .Where(f => f.Ativo &&
                                    !f.Emprestimos.Any(e => e.DataDevolucao == null))
                             .OrderBy(f => f.Nome)
                             .ToList();
        }

        public async Task<IEnumerable<Jogo>> GetDisponiveisAsync()
        {
            return await _dbContext.Jogos
                                   .Include(f => f.Emprestimos)
                                   .Where(f => f.Ativo &&
                                          !f.Emprestimos.Any(e => e.DataDevolucao == null))
                                   .OrderBy(f => f.Nome)
                                   .ToListAsync();
        }
    }
}
