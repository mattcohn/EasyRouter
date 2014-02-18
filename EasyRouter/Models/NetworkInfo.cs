using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class NetworkInfo
    {
        public event EventHandler NetworkAdaptersChanged;

        public NetworkInfo()
        {
            NetworkChange.NetworkAddressChanged += OnChange;
            NetworkChange.NetworkAvailabilityChanged += OnChange;
        }

        private void OnChange(object sender, EventArgs e)
        {
            if (NetworkAdaptersChanged != null)
            {
                NetworkAdaptersChanged(this, new EventArgs());
            }
        }

        public NetworkAdapter[] GetNetworkAdapters()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            var adapters =
                from netInterface in interfaces
                let netAdapter = new NetworkAdapter(netInterface)
                where netAdapter.IsConnectedLocalNetwork()
                select netAdapter;

            return adapters.ToArray();
        }
    }
}
