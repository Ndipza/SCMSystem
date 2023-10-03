using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SCMSystem.Helper;
using SCMSystem.Helper.Interface;
using NLog;
using NLog.Web;
using AspNetCoreRateLimit;

namespace SCMSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // NLog: Setup NLog for Dependency injection
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                // Add services to the container.


                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                        };
                    });

                builder.Services.AddMvc();
                builder.Services.AddControllers().AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.EnableAnnotations();
                });

                //needed to load configuration from appsettings.json
                builder.Services.AddOptions();

                //needed to store rate limit counters and ip rules
                builder.Services.AddMemoryCache();

                // configure ip rate limiting middleware
                builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

                // inject counter and rules stores
                builder.Services.AddInMemoryRateLimiting();

                // configure the resolvers
                builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

                builder.Services.AddDbContext<SCMSystemDBContext>(options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("CartConnection")
                ));
                builder.Services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();

                builder.Services.AddScoped<ICartRepository, CartRepository>();
                builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
                builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
                builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
                builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
                builder.Services.AddScoped<IProductRepository, ProductRepository>();
                builder.Services.AddScoped<ICustomerStatusRepository, CustomerStatusRepository>();
                builder.Services.AddScoped<ICartStatusRepository, CartStatusRepository>();
                builder.Services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
                builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

                builder.Services.AddScoped<ICartService, CartService>();
                builder.Services.AddScoped<ICategoryService, CategoryService>();
                builder.Services.AddScoped<ICustomerService, CustomerService>();
                builder.Services.AddScoped<IPaymentService, PaymentService>();
                builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
                builder.Services.AddScoped<IProductService, ProductService>();
                builder.Services.AddScoped<ICustomerStatusService, CustomerStatusService>();
                builder.Services.AddScoped<ICartStatusService, CartStatusService>();
                builder.Services.AddScoped<IPaymentStatusService, PaymentStatusService>();
                builder.Services.AddScoped<ICartItemService, CartItemService>();
                builder.Services.AddScoped<IFileService, FileService>();                

                var app = builder.Build();

                app.UseIpRateLimiting();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapRazorPages();
                });

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
