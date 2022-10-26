using MediatR;
using Microsoft.EntityFrameworkCore;
using MyStore.Catalago.Application.AutoMapper;
using MyStore.Catalago.Application.Services;
using MyStore.Catalago.Data;
using MyStore.Catalago.Data.Repository;
using MyStore.Catalago.Domain;
using MyStore.Catalago.Domain.Events;
using MyStore.Core.Bus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));
builder.Services.AddMediatR(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalagoContext>(p =>
    p.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IEstoqueService, EstoqueService>();
//builder.Services.AddScoped<CatalagoContext>();
// Domain Bus(Mediator)
builder.Services.AddScoped<IMediatrHandler, MediatrHandler>();

builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();
builder.Services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

var app = builder.Build();
var tt = WebApplication.CreateBuilder();

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
