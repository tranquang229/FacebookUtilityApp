using FM.Application.Interfaces.Repositories;
using FM.Domain.Entities.Facebook.Uid;
using FM.Infrastructure.Contexts;

namespace FM.Infrastructure.Repositories
{

    public class FbRepository : BaseRepository<FbDetailRoot, long>, IFbRepository
    {
        public FbRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
