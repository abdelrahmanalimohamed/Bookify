using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repository
{
    internal abstract class Repository<T>
        where T : Entity
    {
        protected readonly ApplicationDbContext applicationDbContext;
		protected Repository(ApplicationDbContext applicationDbContext)
		{
			this.applicationDbContext = applicationDbContext;
		}
		public async Task<T?> GetByIdAsync(Guid id , CancellationToken cancellationToken = default)
		{
			return await applicationDbContext
				.Set<T>()
				.Where(x => x.Id == id).FirstOrDefaultAsync();
		}

		public void Add(T entity)
		{
			applicationDbContext.Add(entity);
		}
	}
}