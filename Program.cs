using Microsoft.EntityFrameworkCore;
using lab09webAPp.Data;
using lab09webAPp.Models;
using lab09webAPp.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

app.MapGet("/", () => new
{
    Message = "Welcome to Products API",
    ApplicationName = appSettings?.ApplicationName,
    Version = appSettings?.Version,
    Environment = app.Environment.EnvironmentName
})
.WithName("GetRoot");

app.MapGet("/api/products", async (ApplicationDbContext db) =>
{
    var products = await db.Products.ToListAsync();
    return Results.Ok(products);
})
.WithName("GetAllProducts");

app.MapGet("/api/products/{id}", async (int id, ApplicationDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
})
.WithName("GetProductById");

app.MapPost("/api/products", async (Product product, ApplicationDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
})
.WithName("CreateProduct");

app.MapPut("/api/products/{id}", async (int id, Product updatedProduct, ApplicationDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = updatedProduct.Name;
    product.Description = updatedProduct.Description;
    product.Price = updatedProduct.Price;
    product.StockQuantity = updatedProduct.StockQuantity;

    await db.SaveChangesAsync();
    return Results.Ok(product);
})
.WithName("UpdateProduct");

app.MapDelete("/api/products/{id}", async (int id, ApplicationDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("DeleteProduct");

app.MapGet("/health", async (ApplicationDbContext db) =>
{
    try
    {
        await db.Database.CanConnectAsync();
        return Results.Ok(new { Status = "Healthy", Database = "Connected" });
    }
    catch (Exception ex)
    {
        return Results.Ok(new { Status = "Unhealthy", Error = ex.Message });
    }
})
.WithName("HealthCheck");

app.MapGet("/api/config", () => new
{
    AppSettings = appSettings,
    ConnectionStringPreview = builder.Configuration.GetConnectionString("DefaultConnection")?.Substring(0, Math.Min(30, builder.Configuration.GetConnectionString("DefaultConnection")?.Length ?? 0)) + "..."
})
.WithName("GetConfiguration");

app.Run();
