 
var builder = WebApplication.CreateBuilder(args);

// Register services Here

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});
 
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
builder.Services.AddCarterWithModules(catalogAssembly, basketAssembly);

builder.Services.AddMediatRWithAssemblies(catalogAssembly, basketAssembly);
 
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Register Application Pipelines
app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(opt => { });

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule(); 

app.Run();
