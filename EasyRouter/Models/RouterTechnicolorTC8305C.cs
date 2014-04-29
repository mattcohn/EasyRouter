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
            _ssid = "";
            _password = "";
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
                },


                new List<Tuple<string, string>> {
                    new Tuple<string, string>("loginUsername", "admin"),
                    new Tuple<string, string>("loginPassword", "password")}
            );
        }

        public override void Reset()
        {
            SendToRouter("/goform/restore_reboot",

                new List<Tuple<string, string>> {
                },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("resetbt", "1")
                });
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

        public void SetChannel(string channel)
        {
            this.Logon();
            SendToRouter("/goform/wireless_network_configuration_edit",

                new List<Tuple<string, string>> { },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("restore_factory_settings", "false"),
                    new Tuple<string, string>("ssid", _ssid),
                    new Tuple<string, string>("type80211", "gn"),
                    new Tuple<string, string>("security", "wpawpa2"),
                    new Tuple<string, string>("chan_sel", "manual"),
                    new Tuple<string, string>("ChannelNumber", channel),
                    new Tuple<string, string>("netPassword", _password),
                    new Tuple<string, string>("WifiBroadcast", "on"),
                    new Tuple<string, string>("save_settings", "Save+Settings")
                }
            );

        }

        public override void ChangeSSID(string ssid)
        {
            _ssid = ssid;
            //SetSSIDAndPassword(_ssid, _password);
        }

        public override void ChangeWifiPassword(string password)
        {
            _password = password;
            SetSSIDAndPassword(_ssid, _password);
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
