using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using Store.Menna.API.Middlewares;

namespace Store.Menna.API.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddBuiltInServices();
            services.AddSwaggerServices();

            services.ConfigureServices();


            return services;

        }


        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {

            services.AddControllers();


            return services;

        }


        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();



            return services;

        }


        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                              .Select(m => new ValidationError()
                                              {
                                                  Field = m.Key,
                                                  Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                              });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);

                };
            });


            return services;

        }



        public static async Task<WebApplication> ConfigureMiddleware(this WebApplication app)
        {

            await app.InitializeDatabaseAsync();
            
            app.UseGlobalErrorHandling();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }



        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            #region Seeding

            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // ASK CLR Create Object From DbInitializer
            await dbInitializer.InitializAsync();
            #endregion


            return app;
        }


        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;

        }

    }
}
