using MyStore.Catalago.Application.Services;
using MyStore.Catalago.Data;
using MyStore.Catalago.Data.Repository;
using MyStore.Catalago.Domain;
using MyStore.Core.Bus;

namespace MyStore.WebApp.MVC2.Setup
{
    public static class DepencencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus(Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();
            // Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<CatalagoContext> ();
        }
    }
}
