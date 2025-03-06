using Bookify.Domain.Reviews;

namespace Bookify.Infrastructure.Repository;

internal sealed class ReviewRepository : Repository<Review>
{
    public ReviewRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
