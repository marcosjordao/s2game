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

        public IEnumerable<Jogo> GetAtivos()
        {
            return _dbContext.Jogos
                             .Where(f => f.Ativo)
                             .OrderBy(f => f.Nome)
                             .ToList();
        }

        public async Task<IEnumerable<Jogo>> GetAtivosAsync()
        {
            return await _dbContext.Jogos
                                   .Where(f => f.Ativo)
                                   .OrderBy(f => f.Nome)
                                   .ToListAsync();
        }
    }
}
