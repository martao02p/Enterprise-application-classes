using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.RespectBrowserAcceptHeader = true;
//}).AddXmlSerializerFormatters();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CompanyDbContext")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CompanyDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();
