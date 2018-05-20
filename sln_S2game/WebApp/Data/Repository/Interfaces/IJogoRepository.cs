using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Data.Repository.Interfaces
{
    public interface IJogoRepository : IRepositoryBase<Jogo>
    {
        IEnumerable<Jogo> GetAtivos();
        Task<IEnumerable<Jogo>> GetAtivosAsync();
    }
}
