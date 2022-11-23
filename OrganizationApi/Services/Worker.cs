using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using OrganizationApi.Consumers;

namespace OrganizationApi.Services
{
    public class Worker : BackgroundService
    {
        private readonly IBusControl _busControl;
        private readonly IServiceProvider _serviceProvider;

        public Worker(IBusControl busControl, IServiceProvider serviceProvider)
        {
            _busControl = busControl;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken _)
        {
            var productChangeHandler = _busControl.ConnectReceiveEndpoint(
                "UserQueue", 
                x => x.Consumer<UserMessageConsumer>(_serviceProvider));

            await productChangeHandler.Ready;
        }
    }
}
