﻿using Domain.Exceptions;
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
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandlingNotFoundEndPointAsync(context);
                }
            }
            catch (Exception ex)
            {
                // Log the exception

                _logger.LogError(ex, ex.Message);
                await HandlingErrorAsync(context, ex);

            }
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {

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
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = response.StatusCode;



            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End Point {context.Request.Path} is Not Found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
