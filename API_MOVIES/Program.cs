using API_MOVIES.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;


namespace API_MOVIES
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MovieContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var serviceScope = app.Services.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<MovieContext>();

            //context.Database.ExecuteSqlRaw("SQL");
            Console.WriteLine(" ########## " + context.Database.ExecuteSqlRaw("Select * from Movies").ToString());

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
