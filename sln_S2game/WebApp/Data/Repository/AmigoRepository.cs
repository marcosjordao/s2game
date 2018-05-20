using Domain.Model;
using WebApp.Data.Repository.Interfaces;

namespace WebApp.Data.Repository
{
    public class AmigoRepository : RepositoryBase<Amigo>, IAmigoRepository
    {
        public AmigoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
