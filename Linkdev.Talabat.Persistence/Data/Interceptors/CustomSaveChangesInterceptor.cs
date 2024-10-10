
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Linkdev.Talabat.Persistence.Data.Interceptors
{
    internal class CustomSaveChangesInterceptor(StoreContext dbContext, ILoggedInUserService loggedInUserService) : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            foreach (var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>().Where(entry => entry.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedOn = entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = entry.Entity.LastModifiedBy = loggedInUserService.UserId!;
                }
                else
                {
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = loggedInUserService.UserId!;
                }
            }

            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            foreach(var entry in dbContext.ChangeTracker.Entries<BaseAuditableEntity<int>>().Where(entry => entry.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedOn = entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = entry.Entity.LastModifiedBy = loggedInUserService.UserId!;
                }
                else
                {
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = loggedInUserService.UserId!;
                }
            }


            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
