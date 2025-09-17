 
var builder = WebApplication.CreateBuilder(args);

// Register services Here

 
var catalogAssembly = typeof(CatalogModule).Assembly;
builder.Services.AddCarterWithModules(catalogAssembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);


var app = builder.Build();

// Register Application Pipelines
app.MapCarter();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.Run();
