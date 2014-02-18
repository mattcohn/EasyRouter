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
    public class RouterTests
    {
        [TestMethod]
        public void ConnectTo_192_168_0_1()
        {
            Router rout = RouterFactory.GetRouter(new System.Net.IPAddress(new byte[] { 192, 168, 0, 1 }));
        }

        [TestMethod]
        public void LogOnTo_192_168_0_1()
        {
            Router router = GetRouter();

            router.Logon();
        }

        [TestMethod]
        public void ChangeSSID_mc2()
        {
            Router router = GetRouter();

            router.Logon();
            router.ChangeSSID("mc2");
        }

        [TestMethod]
        public void ChangeSSID_pony()
        {
            Router router = GetRouter();

            router.Logon();
            router.ChangeSSID("pony");
        }

        [TestMethod]
        public void ChangePassword_password()
        {
            Router router = GetRouter();

            router.Logon();
            router.ChangeWifiPassword("password");
        }

        [TestMethod]
        public void ChangePassword_2063849059()
        {
            Router router = GetRouter();

            router.Logon();
            router.ChangeWifiPassword("2063849059");
        }

        private Router GetRouter()
        {
            return new RouterActiontecM1000("http://192.168.0.1/");
        }
    }
}
