using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Web2.API.BusinessLogic;
using Web2.API.Data;
using Web2.API.Data.Repositories;
using Web2.API.Extenstions;
using Web2.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("EventDB");
builder.Services.AddDbContext<EventPlatformDbContext>(options => options.UseNpgsql(connectionString));


builder.Services.AddScoped<ICategoryBL, CategoryBL>();
builder.Services.AddScoped<IVilleBL, VillesBL>();
builder.Services.AddScoped<IEvenementBL, EvenementBL>();
builder.Services.AddScoped<IParticipationBL, ParticipationBL>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEvenementRepository, EvenementRepository>();
builder.Services.AddScoped<IVilleRepository, VilleRepository>();
builder.Services.AddScoped<IParticipationRepository, ParticipationRepository>();

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddControllers(o =>
{
    o.AllowEmptyInputInBodyModelBinding = true;
    o.Filters.Add<HtppResponseExceptionFilter>();
})
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true)
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterAuthentication();

builder.Services.RegisterSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt => opt.OAuthClientId("swagger_ui"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExists();

app.Run();
