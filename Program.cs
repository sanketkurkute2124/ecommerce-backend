using ECommerceApp.Data;
using ECommerceApp.Services;

//using ECommerceApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.WebHost.ConfigureKestrel(options =>
            //{
            //    options.ListenAnyIP(80); // listen on HTTP port 80
            //});

            var portEnv = Environment.GetEnvironmentVariable("PORT");
            if(int.TryParse(portEnv,out var portNumber))
            {
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(portNumber);
                }

                );
            }
            // Add services to the container.

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // This will use the property names as defined in the C# model
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000") // React app origin
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure EF Core with SQL Server
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("EFCoreDBConnection")))


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("EFCoreDBConnection")));
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<AddressService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ShoppingCartService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<PaymentService>();
            builder.Services.AddScoped<CancellationService>();
            builder.Services.AddScoped<FeedbackService>();
            builder.Services.AddScoped<RefundService>();
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            //  app.UseHttpsRedirection();
            app.UseCors("AllowReactApp");
            app.UseAuthorization();

            app.MapControllers();
                
            app.Run();
            //for the deployment of the application on Azure, we need to use the following code instead of app.Run();
            //var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            //app.Run($"http://0.0.0.0:{port}");
        }
    }
}