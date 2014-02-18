using EasyRouter.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Tests
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public void GetNetworkAdapters()
        {
            NetworkInfo info = new NetworkInfo();
            var networkAdapters = info.GetNetworkAdapters();

        }
    }
}
