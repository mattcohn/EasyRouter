using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class RouterLinksysWRT54G2 : Router
    {
        public RouterLinksysWRT54G2(string requestUriString)
            : base(requestUriString)
        {
        }

        public override void Logon()
        {
            GETToRouter("",

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic YWRtaW46am9wZW5zb3VyY2U0") },
                        
                new List<Tuple<string, string>> {}
            );
        }

        public override void ChangeSSID(string ssid)
        {
            POSTToRouter("/WBasic.tri",

                new List<Tuple<string, string>> {
                    //new Tuple<string, string>("Authorization", "Basic YWRtaW46am9wZW5zb3VyY2U0"),
                    //new Tuple<string, string>("Accept", "text/html, application/xhtml+xml"),
                    //new Tuple<string, string>("Referer", "192.168.1.1/wireless.html"),
                    new Tuple<string, string>("Authorization", "Basic YWRtaW46am9wZW5zb3VyY2U0")
                 },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("submit_type", ""),
                    new Tuple<string, string>("action", "apply"),
                    new Tuple<string, string>("SSID", ssid),
                    new Tuple<string, string>("wsc_smode", "1"),
                    new Tuple<string, string>("channelno", "11"),
                    new Tuple<string, string>("OldWirelessMode", "2"),
                    new Tuple<string, string>("Mode", "2"),
                    new Tuple<string, string>("channel", "6"),
                    new Tuple<string, string>("Freq", "6"),
                    new Tuple<string, string>("wl_closed", "1"),
                    new Tuple<string, string>("layout", "en") }
            );
        }
    }
}
