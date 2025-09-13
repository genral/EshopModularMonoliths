

var builder = WebApplication.CreateBuilder(args);

// Register services Here
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);


var app = builder.Build();

// Register Application Pipelines
app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.Run();
