using EasyRouter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyRouter.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private object _currentViewModel;

        private IPAddress _currentConnectedIPAddress;
        private Router _currentConnectedRouter;

        public MainWindowViewModel()
        {
            StartUp();
        }

        private void StartUp()
        {
            CurrentViewModel = new LoadingViewModel();

            NetworkInfo info = new NetworkInfo();
            info.NetworkAdaptersChanged += info_NetworkAdaptersChanged;
            info_NetworkAdaptersChanged(info, new EventArgs());
        }

        private void info_NetworkAdaptersChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    NetworkInfo info = (NetworkInfo)sender;
                    //IEnumerable<NetworkAdapter> netAdapters = info.GetNetworkAdapters();
                    IEnumerable<NetworkInterface> netAdapters = NetworkInterface.GetAllNetworkInterfaces();

                    GatewayIPAddressInformation g = netAdapters.First().GetIPProperties().GatewayAddresses.Last();
                    

                    if (netAdapters.Count() > 0)
                    {
                        IPAddress ipAddr = g.Address; //netAdapters.First().GetGatewayAddress();
                        Router router = RouterFactory.GetRouter(ipAddr);

                        if (
                            router != null &&
                            (ipAddr != _currentConnectedIPAddress ||
                             router.GetType() != _currentConnectedRouter.GetType()))
                        {
                            CurrentViewModel = new RouterConfigViewModel(router);

                            _currentConnectedIPAddress = ipAddr;
                            _currentConnectedRouter = router;
                        }
                    }
                    else
                    {
                        CurrentViewModel = new LoadingViewModel();
                    }
                }), null);
        }


        public object CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }

            private set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged("CurrentViewModel");
                }
            }
        }
    }
}
