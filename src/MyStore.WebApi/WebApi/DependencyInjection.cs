using MediatR;
using MyStore.Catalago.Application.Services;
using MyStore.Catalago.Data.Repository;
using MyStore.Catalago.Domain;
using MyStore.Catalago.Domain.Events;
using MyStore.Core.Comunication.Mediator;
using MyStore.Core.Messages.CommonMessages.IntegrationEvents;
using MyStore.Core.Messages.CommonMessages.Notifications;
using MyStore.Pagamentos.AntiCorruption;
using MyStore.Pagamentos.Bussiness;
using MyStore.Pagamentos.Data.Repository;
using MyStore.Vendas.Application.Commands;
using MyStore.Vendas.Application.Events;
using MyStore.Vendas.Application.Queries;
using MyStore.Vendas.Data.Repository;
using MyStore.Vendas.Domain;

namespace WebApi
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Mediator (se fosse bus, seria a mesma implementação)
            services.AddScoped<IMediatorHandler, MediatrHandler>();

            //Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Catalogo
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IProdutoAppService, ProdutoAppService>();

            services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

            //Vendas            
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverItemPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AplicarVoucherPedidoCommand, bool>, PedidoCommandHandler>();

            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();

            // Pagamento
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, MyStore.Pagamentos.AntiCorruption.ConfigurationManager>();
            

            //services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoEvent>, PagamentoEventHandler>();
        }
    }
}
