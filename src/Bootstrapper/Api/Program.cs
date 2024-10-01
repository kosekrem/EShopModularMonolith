var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, config) =>
        config.ReadFrom.Configuration(context.Configuration));
    
    // Add services to the container.
    
    // For common services
    var catalogAssembly = typeof(CatalogModule).Assembly;
    var basketAssembly = typeof(BasketModule).Assembly;
    var orderingAssembly = typeof(OrderingModule).Assembly;
    
    
    // common services: carter, mediatr, fluentvalidation
    builder.Services.AddMediatRWithAssemblies(catalogAssembly, basketAssembly);
    builder.Services.AddCarterWithAssemblies(catalogAssembly, basketAssembly);
    builder.Services.AddValidatorsFromAssemblies([catalogAssembly, basketAssembly]);
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    });
    builder.Services.AddMassTransitWithAssemblies(builder.Configuration, basketAssembly, catalogAssembly);
    
    // module services: catalog, basket, ordering
    builder.Services
        .AddCatalogModule(builder.Configuration)
        .AddBasketModule(builder.Configuration)
        .AddOrderingModule(builder.Configuration);

    builder.Services
        .AddExceptionHandler<CustomExceptionHandler>();
}

var app = builder.Build();
{
    // Configure the middleware.
    
    app.MapCarter();
    app.UseSerilogRequestLogging();
    app.UseExceptionHandler(options => {});

    
    app
        .UseCatalogModule()
        .UseBasketModule()
        .UseOrderingModule();
    
    app.Run();
}



