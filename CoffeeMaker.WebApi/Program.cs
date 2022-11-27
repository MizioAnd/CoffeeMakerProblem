using CoffeeMaker.Adapters;
using CoffeeMaker.Adapters.StateMachines;

public class Program
{
    static public void Main(String[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register
        RegisterServices(builder.Services);
        
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

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<BrewButton>();
        services.AddSingleton<Warmer>();
        services.AddSingleton<Boiler>();
        services.AddSingleton<CoffeeMakerStateMachine>();
    }
}


