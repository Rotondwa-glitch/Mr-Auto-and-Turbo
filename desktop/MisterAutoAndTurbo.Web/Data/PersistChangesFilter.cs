using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MisterAutoAndTurbo.Web.Data
{
    /// <summary>
    /// Action filter that refreshes in-memory data from Supabase before actions
    /// and saves changes after successful non-GET requests.
    /// </summary>
    public class PersistChangesFilter : IActionFilter
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PersistChangesFilter> _log;

        public PersistChangesFilter(ApplicationDbContext db, ILogger<PersistChangesFilter> log)
        {
            _db = db;
            _log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString() ?? "";
            if (string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                AppDataStore.RefreshFromDatabase(_db);
                if (AppDataStore.ApplyCustomerAppUpdates())
                    AppDataStore.SaveToDatabase(_db);
            }
            catch (Exception ex)
            {
                _log.LogWarning(ex, "Could not refresh data from Supabase before the action.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) return;
            if (HttpMethods.IsGet(context.HttpContext.Request.Method) ||
                HttpMethods.IsHead(context.HttpContext.Request.Method)) return;

            var controller = context.RouteData.Values["controller"]?.ToString() ?? "";
            if (string.Equals(controller, "Account", StringComparison.OrdinalIgnoreCase))
                return;

            try
            {
                AppDataStore.SaveToDatabase(_db);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Failed to save changes to the database.");
                throw;
            }
        }
    }
}
