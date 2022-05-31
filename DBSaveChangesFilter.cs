using Microsoft.AspNetCore.Mvc.Filters;

namespace rest_api_test;

public class DBSaveChangesFilter : IAsyncActionFilter
{
    private readonly ItemContext _dbContext;

    public DBSaveChangesFilter(ItemContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next) 
    {
        var result = await next();
        if (result.Exception == null || result.ExceptionHandled)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}