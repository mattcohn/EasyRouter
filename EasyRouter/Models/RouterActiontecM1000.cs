using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class RouterActiontecM1000 : Router
    {
        private string _ssid;
        private string _password;
        public RouterActiontecM1000(string requestUriString)
            : base(requestUriString)
        {
            _ssid = "";
            _password = "";
        }

        public override string Model
        {
            get { return "Actiontec M1000"; }
        }

        public override string ImageFilename
        {
            get { return "C1000A-12.jpg"; }
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        public override void Logon()
        {
            SendToRouter("login.cgi",
                
                new List<Tuple<string, string>> {
                    new Tuple<string, string>("Cookie", "CenturyLink=sdk4_12_2_3g; showLoginPass=null") },

                
                new List<Tuple<string, string>> {
                    new Tuple<string, string>("adminUserName", "admin"),
                    new Tuple<string, string>("adminPassword", "2063849059"),
                    new Tuple<string, string>("sessionKey", "123"),
                    new Tuple<string, string>("nothankyou", "1")}
            );
        }

        public override void ChangeSSID(string ssid)
        {
            SendToRouter("wirelesssetup_basicsettings.wl",

                new List<Tuple<string, string>> { },
                
                new List<Tuple<string, string>> {
                    new Tuple<string, string>("wlRadio", "1"),
                    new Tuple<string, string>("wlSsid_wl0v0", ssid),
                    new Tuple<string, string>("needthankyou", "0") }
            );

            _ssid = ssid;
        }

        public override void ChangeWifiPassword(string password)
        {
            SendToRouter("wirelesssetup_security.wl",

                new List<Tuple<string, string>> { },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("wlDefaultKeyFlagWep64Bit", "1"),
                    new Tuple<string, string>("wlDefaultKeyFlagWeb128Bit", "0"),
                    new Tuple<string, string>("wlDefaultKeyWeb64Bit", "A8DCFFA8DD"),
                    new Tuple<string, string>("wlDefaultKeyWep128Bit", "ff8cfcbeffd6b35ffb94affb4e"),
                    new Tuple<string, string>("wlDefaultKeyWep128Bit", "ff8cfcbeffd6b35ffb94affb4e"),
                    new Tuple<string, string>("wlDefaultKeyWep128Bit", "ff8cfcbeffd6b35ffb94affb4e"),
                    new Tuple<string, string>("wlKeyBit_sl0v0", "0"),
                    new Tuple<string, string>("wlAuthMode_wl0v0", "psk2"),
                    new Tuple<string, string>("wlWep_wl0v0", "disabled"),
                    new Tuple<string, string>("wlWpaPsk_wl0v0", password),
                    new Tuple<string, string>("wlWpa_wl0v0", "aes"),
                    new Tuple<string, string>("wlDefaultKeyFlagPsk", "14"),
                    new Tuple<string, string>("wlDefaultKeyPsk1", "22gcn6ye2ans72"),
                    new Tuple<string, string>("wlDefaultKeyPsk2", "22gcn6ye2ans72"),
                    new Tuple<string, string>("wlDefaultKeyPsk3", "22gcn6ye2ans72"),
                    new Tuple<string, string>("needthankyou", "0")
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
