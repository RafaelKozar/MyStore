using MyStore.Catalago.Application.Services;
using MyStore.Catalago.Data;
using MyStore.Catalago.Data.Repository;
using MyStore.Catalago.Domain;
using MyStore.Core.Comunication.Mediator;
using RestSharp;

namespace MyStore.WebApp.MVC2.Setup
{
    public static class DepencencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus(Mediator)
            services.AddScoped<IMediatorHandler, MediatrHandler>();
            // Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddSingleton(new RestClient("https://localhost:7139/"));
            //services.AddScoped<CatalagoContext> ();
        }
    }
}
