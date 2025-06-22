using PizzeriaApi.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PizzeriaContext>(options =>
    options.UseSqlite("Data Source=pizzeria.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzeriaContext>();
    db.Database.EnsureCreated(); // creating pizzeria.db
    var cheese = db.Ingredients.FirstOrDefault(i => i.Name == "cheese");
    if (cheese == null)
    {
        db.Ingredients.Add(new Ingredient { Name = "cheese", IsSystem = true });
    }

    var sauce = db.Ingredients.FirstOrDefault(i => i.Name == "sauce");
    if (sauce == null)
    {
        db.Ingredients.Add(new Ingredient { Name = "sauce", IsSystem = true });
    }

    db.SaveChanges();
}

app.Run();
