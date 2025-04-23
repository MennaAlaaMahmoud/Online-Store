using Domain.Exaptions;
using Shared.ErrorModels;

namespace Store.Menna.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobalErrorHandlingMiddleware> logger)
        {
           _next = next;
           _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                // Log the exception

                _logger.LogError(ex, ex.Message);

                // 1. Set the response status code
                // 2. Set the response content type
                // 3. Set the response Object (body)
                // 4. Return the response
                //context.Response.StatusCode =StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message
                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = response.StatusCode;



                await context.Response.WriteAsJsonAsync(response);

            }



        }


    }
}
