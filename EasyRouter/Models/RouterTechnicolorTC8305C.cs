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
    public class RouterTechnicolorTC8305C : Router
    {
        private string _ssid;
        private string _password;
        public RouterTechnicolorTC8305C(string requestUriString)
            : base(requestUriString)
        {
            _ssid = "trestle";
            _password = "password";
        }

        public override string Model
        {
            get { return "TechnicolorTC8305C"; }
        }

        public override string ImageFilename
        {
            get { return "TechnicolorTC8305C.jpg"; }
        }

        public override void Logon()
        {
            SendToRouter("/goform/home_loggedout",

                new List<Tuple<string, string>> {
                //    new Tuple<string, string>("Cookie", "CenturyLink=sdk4_12_2_3g; showLoginPass=null") 
                },


                new List<Tuple<string, string>> {
                    new Tuple<string, string>("loginUsername", "admin"),
                    new Tuple<string, string>("loginPassword", "password")}
            );
        }

        public void SetSSIDAndPassword(string ssid, string password)
        {
            this.Logon();
            SendToRouter("/goform/wireless_network_configuration_edit",

                new List<Tuple<string, string>> { },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("restore_factory_settings", "false"),
                    new Tuple<string, string>("ssid", ssid),
                    new Tuple<string, string>("type80211", "gn"),
                    new Tuple<string, string>("security", "wpawpa2"),
                    new Tuple<string, string>("chan_sel", "auto"),
                    new Tuple<string, string>("netPassword", password),
                    new Tuple<string, string>("WifiBroadcast", "on"),
                    new Tuple<string, string>("save_settings", "Save+Settings")
                }
            );

        }

        public override void ChangeSSID(string ssid)
        {
            SetSSIDAndPassword(ssid, _password);
            _ssid = ssid;
        }

        public override void ChangeWifiPassword(string password)
        {
            SetSSIDAndPassword(_ssid, password);
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
