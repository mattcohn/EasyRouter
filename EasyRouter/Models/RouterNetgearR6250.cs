using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class RouterNetgearR6250 : Router
    {

        private string _ssid;
        private string _password;

        public RouterNetgearR6250(string requestUriString)
            : base(requestUriString)
        {
            _ssid = "";
            _password = "";
        }

        public override string Model
        {
            get { return "NETGEAR R6250/R6300"; }
        }

        public override string ImageFilename
        {
            get { return "NETGEAR_R6250.jpg"; }
        }


        public override void Logon()
        {
            SendToRouter("",

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic YWRtaW46cGFzc3dvcmQ=") },
                        
                new List<Tuple<string, string>> {}
            );
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void ChangeSSID(string ssid)
        {
            SendToRouter("WBasic.tri",

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic OmFkbWlu")
                 },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("submit_type", ""),
                    new Tuple<string, string>("action", "apply"),
                    new Tuple<string, string>("wsc_smode", "1"),
                    new Tuple<string, string>("channelno", "11"),
                    new Tuple<string, string>("OldWirelessMode", "2"),
                    new Tuple<string, string>("Mode", "2"),
                    new Tuple<string, string>("SSID", ssid),
                    new Tuple<string, string>("channel", "6"),
                    new Tuple<string, string>("Freq", "6"),
                    new Tuple<string, string>("wl_closed", "1"),
                    new Tuple<string, string>("layout", "en") }
            );

            _ssid = ssid;
        }


        public override void ChangeWifiPassword(string password)
        {
            SendToRouter("Security.tri",

                new List<Tuple<string, string>> { },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("SecurityMode", "3"),
                    new Tuple<string, string>("CipherType", "1"),
                    new Tuple<string, string>("PassPhrase", password),
                    new Tuple<string, string>("GkuInterval", "3600"),
                    new Tuple<string, string>("layout", "en")
                });

            _password = password;
        }

        public override string GetSSID()
        {
            return _ssid;
        }

        public override string GetWifiPassword()
        {
            return _password;
        }
    }
}
