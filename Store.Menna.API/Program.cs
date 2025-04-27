
using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using ServicesAbstractions;
using Shared.ErrorModels;
using Store.Menna.API.Extensions;
using Store.Menna.API.Middlewares;
namespace Store.Menna.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

    

            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddApplicationServices();


            builder.Services.RegisterServices(builder.Configuration);



            var app = builder.Build();

           await app.ConfigureMiddleware();

            app.Run();

        }
    }
}
