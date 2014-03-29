using EasyRouter.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EasyRouter.Tests
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public void GetNetworkAdapters()
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            if (adapters.Length == 0) throw new Exception("no adapters");
            GatewayIPAddressInformation g = adapters.First().GetIPProperties().GatewayAddresses.Last();
            if (g.Address.ToString() != "192.168.1.1") throw new Exception("unexpected gateway IP");
        }
    }
}
