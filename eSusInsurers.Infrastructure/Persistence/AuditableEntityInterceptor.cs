using eSusInsurers.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace eSusInsurers.Infrastructure.Persistence
{
    public class AuditableEntityInterceptor(IDateTime dateTime, IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            _ = ApplyAuditFields(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await ApplyAuditFields(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task ApplyAuditFields(DbContext? context)
        {
            if (context == null)
            {
                return;
            }

            var modifiedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            string emailId = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true ? httpContextAccessor.HttpContext?.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value.ToString() ?? "SYSTEM" : string.Empty;

            foreach (var entry in modifiedEntries)
            {
                var now = dateTime.Now;

                if (entry.State == EntityState.Added)
                {

                    var insertDateTimeProperty = entry.Entity.GetType().GetProperty("CreatedDate");

                    if (insertDateTimeProperty != null)
                    {
                        entry.Property("CreatedDate").CurrentValue = now;
                    }

                    if (!string.IsNullOrEmpty(emailId))
                    {
                        var insertCreatedByProperty = entry.Entity.GetType().GetProperty("CreatedBy");

                        if (insertCreatedByProperty != null)
                        {
                            entry.Property("CreatedBy").CurrentValue = emailId;
                        }
                    }
                }

                var modifiedDateProperty = entry.Entity.GetType().GetProperty("ModifiedDate");

                if (modifiedDateProperty != null)
                {
                    entry.Property("ModifiedDate").CurrentValue = now;
                }

                if (!string.IsNullOrEmpty(emailId))
                {
                    var modifiedByProperty = entry.Entity.GetType().GetProperty("ModifiedBy");

                    if (modifiedByProperty != null)
                    {
                        entry.Property("ModifiedBy").CurrentValue = emailId;
                    }
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
