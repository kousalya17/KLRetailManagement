using Caliburn.Micro;
using KRMDesktopUI.EventModedls;
using System.Threading;
using System.Threading.Tasks; 

namespace KRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _saleVM;

        public ShellViewModel(IEventAggregator events, SalesViewModel saleVM, SimpleContainer container)
        {
            _events = events;
            _saleVM = saleVM;

            _events.Subscribe(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }

        public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(_saleVM);
        }
    }
}