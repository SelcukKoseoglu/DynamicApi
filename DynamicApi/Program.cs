using DynamicAPI.Business.DynamicObjectMethods;
using DynamicAPI.Business.Helper;
using DynamicAPI.DAL.Context;
using DynamicAPI.DAL.Repository.Abstract;
using DynamicAPI.DAL.Repository.Concrete;
using Microsoft.EntityFrameworkCore;
using System;

namespace DynamicAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContextPool<DynamicApiDbContext>(
                o => o.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));
            builder.Services.AddPooledDbContextFactory<DynamicApiDbContext>(ob =>
                ob.UseSqlServer("ConStr").EnableServiceProviderCaching(true), poolSize: 32);


            builder.Services.AddScoped<IDynamicObjectRepository, DynamicObjectRepository>();
            builder.Services.AddScoped<IObjectMethods, ObjectMethods>();
            builder.Services.AddScoped<ObjectHelper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
