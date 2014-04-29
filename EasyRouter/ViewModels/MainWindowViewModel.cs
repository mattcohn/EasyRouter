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
                    IEnumerable<NetworkAdapter> netAdapters = info.GetNetworkAdapters();
                    IEnumerable<NetworkInterface> altNetAdapters = NetworkInterface.GetAllNetworkInterfaces();
                    var netAdaptersWorking = from adapter in altNetAdapters 
                                                where adapter.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up 
                                                && adapter.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback 
                                                && adapter.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Tunnel
                                                select adapter;
                    
                    GatewayIPAddressInformation g = null;

                    try
                    {
                        g = netAdaptersWorking.First().GetIPProperties().GatewayAddresses.Last();
                    }
                    catch (Exception ) 
                    { 
                        //don't crash! 
                    }
                    

                    if (netAdapters.Count() > 0 || g != null)
                    {
                        IPAddress ipAddr = null;
                        Router router = null;
                        if (g != null)
                        {
                            ipAddr = g.Address;
                            router = RouterFactory.GetRouter(ipAddr);
                        }

                        if (
                            router != null &&
                            (ipAddr != _currentConnectedIPAddress ||
                             router.GetType() != _currentConnectedRouter.GetType()))
                        {
                            CurrentViewModel = new RouterConfigViewModel(router);

                            _currentConnectedIPAddress = ipAddr;
                            _currentConnectedRouter = router;
                        }

                        else
                        {
                            foreach (NetworkAdapter adapter in netAdapters)
                            {
                                ipAddr = adapter.GetGatewayAddress();

                                router = RouterFactory.GetRouter(ipAddr);

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
