using MediatR;
using MyStore.Catalago.Application.Services;
using MyStore.Catalago.Data.Repository;
using MyStore.Catalago.Domain;
using MyStore.Catalago.Domain.Events;
using MyStore.Core.Bus;
using MyStore.Vendas.Application.Commands;
using MyStore.Vendas.Data.Repository;
using MyStore.Vendas.Domain;

namespace WebApi
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Domain Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            //Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            //Vendas
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
        }
    }
}
