using Core.ViewModels;
using Data;
using Data.Models;
using Data.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.Interfaces;

namespace NN.Cart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Enable the selflog output
            //SelfLog.Enable(Console.Error);
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.Console(theme: SystemConsoleTheme.Literate)
            //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration.GetConnectionString("elasticsearch"))) // for the docker-compose implementation
            //{
            //    AutoRegisterTemplate = true,
            //    OverwriteTemplate = true,
            //    DetectElasticsearchVersion = true,
            //    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            //    NumberOfReplicas = 1,
            //    NumberOfShards = 2,
            //    //BufferBaseFilename = "./buffer",
            //    // RegisterTemplateFailure = RegisterTemplateRecovery.FailSink,
            //    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
            //    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
            //                           EmitEventFailureHandling.WriteToFailureSink |
            //                           EmitEventFailureHandling.RaiseCallback,
            //    FailureSink = new FileSink("./fail-{Date}.txt", new JsonFormatter(), null, null)
            //})
            //    .CreateLogger();

            //Log.Information("Hello, world!");
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SCMSystemDBContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("CartConnection")
            ));
            //builder.Services.AddScoped<ISeedService, SeedService>();
            builder.Services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
            builder.Services.AddTransient<IValidator<PaymentMethodViewModel>, PaymentMethodViewModelValidator>();

            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();


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