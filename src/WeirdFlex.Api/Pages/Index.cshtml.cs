using AutoMapper;
using EasyNetQ;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Tieto.Lama.Business.UseCases;
using Tieto.Lama.Data.Unbound;
using Tieto.Lama.PrintApi.HostedServices;
using Tieto.Lama.PrintApi.Interfaces;

namespace Tieto.Lama.PrintApi.Pages
{
    [AllowAnonymous]
    public class IndexModel : LamaPageModel
    {
        readonly IBus _bus;

        public IndexModel(IMediator mediator, IMapper mapper, IBus bus, IQueueReceiverMonitor<PrintJobReceiver> pjR, IQueueReceiverMonitor<StockDataReceiver> sdR, PrintJobHandlerMonitor printJobHandlerMonitor)
            : base(mediator, mapper)
        {
            _bus = bus;
            PrintJobReceiverMonitor = pjR;
            StockDataReceiverMonitor = sdR;
            PrintJobHandlerMonitor = printJobHandlerMonitor;
        }

        public HealthStatusModel ViewModel { get; private set; } = new HealthStatusModel();

        public bool IsBusConnected { get; private set; }

        public IQueueReceiverMonitor<PrintJobReceiver> PrintJobReceiverMonitor { get; private set; }
        public IQueueReceiverMonitor<StockDataReceiver> StockDataReceiverMonitor { get; private set; }
        public PrintJobHandlerMonitor PrintJobHandlerMonitor { get; private set; }

        public async Task OnGetAsync()
        {
            ViewModel = await HandleGetRequest(new GetHealthStatus.Request(),
                x => Mapper.Map<HealthStatusModel>(x.Result)) ?? new HealthStatusModel();

            IsBusConnected = _bus.IsConnected;
        }
    }
}
