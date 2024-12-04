
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Linkdev.Talabat.Persistence.Data.Interceptors
{
    internal class AuditInterceptor(ILoggedInUserService loggedInUserService) : SaveChangesInterceptor
    {

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			
			UpdateEntities(eventData.Context!);
			return base.SavingChanges(eventData, result);
		}

		public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			UpdateEntities(eventData.Context!);
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

       private void UpdateEntities(DbContext dbContext)
		{
			foreach (var entry in dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
				.Where(entry => entry.State is EntityState.Added or EntityState.Modified))
			{
				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedOn = entry.Entity.LastModifiedOn = DateTime.UtcNow;
					entry.Entity.CreatedBy = entry.Entity.LastModifiedBy = loggedInUserService.UserId!;
				}
				entry.Entity.LastModifiedOn = DateTime.UtcNow;
				entry.Entity.LastModifiedBy = loggedInUserService.UserId ?? "Stripe User";
				
			}
		}
    }
}
