using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class RouterFactory
    {
        static List<Tuple<Func<string, bool>, Func<string, Router>>> _routerFactories;

        static RouterFactory()
        {
            // Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);

            // Set up router factories
            _routerFactories = new List<Tuple<Func<string, bool>, Func<string, Router>>>();
            _routerFactories.Add(new Tuple<Func<string, bool>, Func<string, Router>>(
                (html) => html.Contains("Actiontec M1000"),
                (ipaddr) => new RouterActiontecM1000(ipaddr)));
        }

        public static Router GetRouter(IPAddress address)
        {
            return GetRouterHttps(address) ?? GetRouterHttp(address);
        }

        private static Router GetRouterHttp(IPAddress address)
        {
            return GetRouter(string.Format("http://{0}", address.ToString()));
        }

        private static Router GetRouterHttps(IPAddress address)
        {
            return GetRouter(string.Format("https://{0}", address.ToString()));
        }

        private static Router GetRouter(string address)
        {
            HttpWebRequest req = WebRequest.CreateHttp(address);

            WebResponse resp = req.GetResponse();
            Stream respStream = resp.GetResponseStream();
            string respText = (new StreamReader(respStream)).ReadToEnd();

            // Get first router factory that works
            var routerfactory = _routerFactories.FirstOrDefault((routerfact) => routerfact.Item1(respText));

            // None found
            if (routerfactory == null)
                return null;

            // Router found - create model
            return routerfactory.Item2(address);
        }
    }
}
