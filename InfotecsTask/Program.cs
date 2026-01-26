using InfotecsTask.Data;
using InfotecsTask.Repositories.Results;
using InfotecsTask.Repositories.ValuesRepository;
using InfotecsTask.Services.FacadeValuesResults;
using InfotecsTask.Services.ResultsService;
using InfotecsTask.Services.ValuesService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IValuesRepository, ValuesRepository>();
builder.Services.AddScoped<IResultsRepository, ResultsRepository>();
builder.Services.AddScoped<IValuesService, ValuesService>();
builder.Services.AddScoped<IResultsService, ResultsService>();
builder.Services.AddScoped<IFacadeService, FacadeService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
);

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
