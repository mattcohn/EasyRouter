using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class NetworkAdapter
    {
        private NetworkInterface _netInterface;
        public NetworkAdapter(NetworkInterface netInterface)
        {
            _netInterface = netInterface;
        }

        public bool IsConnectedLocalNetwork()
        {
            if (_netInterface.OperationalStatus != OperationalStatus.Up)
                return false;

            if (_netInterface.GetIPProperties().GatewayAddresses.Count != 1)
                return false;

            IPAddress gatewayAddr = GetGatewayAddress();

            return IsLocalAddress(gatewayAddr);
        }

        public IPAddress GetGatewayAddress()
        {
            return
                _netInterface
                .GetIPProperties()
                .GatewayAddresses
                .Single()
                .Address;
        }

        private bool IsLocalAddress(IPAddress gatewayAddr)
        {
            byte[] addressBytes = gatewayAddr.GetAddressBytes();

            if (addressBytes[0] == 10)
                return true;

            if (addressBytes[0] == 192 && addressBytes[1] == 168)
                return true;

            if (addressBytes[0] == 172 && addressBytes[1] >= 16 && addressBytes[1] <= 31)
                return true;

            return false;
        }
    }
}
