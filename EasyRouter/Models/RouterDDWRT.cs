using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class RouterDDWRT : Router
    {

        private string _ssid;
        private string _password;

        public RouterDDWRT(string requestUriString)
            : base(requestUriString)
        {
            _ssid = "";
            _password = "";
        }

        public override string Model
        {
            get { return "DD-WRT"; }
        }

        public override string ImageFilename
        {
            get { return "WRT54G.jpg"; }
        }


        public override void Logon()
        {
            SendToRouter("",

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic OmFkbWlu") },

                new List<Tuple<string, string>> { }
            );
        }

        public override void Reset()
        {
            SendToRouter("/apply.cgi",

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic cm9vdDphZG1pbg==")
                 },

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("submit_button", "Management"),
                    new Tuple<string, string>("action", "Reboot")
                });

        }
        /*
        protected void SendToRouter(string path, string httpVerb, IEnumerable<Tuple<string, string>> headers, IEnumerable<Tuple<string, string>> formData)
        {
            if (path.Length > 1 && !path.Substring(0, 1).Equals("/")) path = "/" + path; // prepend leading / in url path

            HttpWebRequest hwr = WebRequest.CreateHttp(_requestUriString + path);
            hwr.Method = httpVerb;
            hwr.ContentType = "application/x-www-form-urlencoded";
            //hwr.KeepAlive = true;
            hwr.Headers["Authorization"] = "Basic OmFkbWlu";
            hwr.Accept = "text/html, application/xhtml+xml";
            hwr.Referer = "192.168.1.1/wireless.html";
            hwr.CachePolicy = WebRequest.DefaultCachePolicy;
            hwr.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";

            foreach (Tuple<string, string> header in headers)
            {
                hwr.Headers[header.Item1] = header.Item2;
            }

            if (hwr.Method == "POST")
            {
                byte[] formBytes = GetFormData(formData);
                hwr.ContentLength = formBytes.Length;

                using (Stream stream = hwr.GetRequestStream())
                {
                    stream.Write(formBytes, 0, formBytes.Length);
                }
            }

                WebResponse response = hwr.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    string responseString = reader.ReadToEnd();
                }


        }
        */

        public void SetSSIDAndPassword(string ssid, string password)
        {
            SendToRouter("/apply.cgi",

    new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic cm9vdDphZG1pbg==")
                 },

    new List<Tuple<string, string>> {
                    new Tuple<string, string>("submit_button", "WL_WPATable"),
                    new Tuple<string, string>("action", "ApplyTake"),
                    new Tuple<string, string>("change_action", "gozila_cgi"),
                    new Tuple<string, string>("submit_type", "save"),
                    new Tuple<string, string>("security_varname", ""),
                    new Tuple<string, string>("security_mode_last", ""),
                    new Tuple<string, string>("wl_wep_last", ""),
                    new Tuple<string, string>("filter_mac_value", ""),
                    new Tuple<string, string>("wl0_security_mode", "psk2"),
                    new Tuple<string, string>("wl0_crypto", "tkip"),
                    new Tuple<string, string>("wl0_wpa_psk", password),
                    new Tuple<string, string>("wl0_wl_unmask", "0"),
                    new Tuple<string, string>("wl0_wpa_gtk_rekey", "3600")
                });


            SendToRouter("/apply.cgi",

                            new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic cm9vdDphZG1pbg==")
                 },

                            new List<Tuple<string, string>> {
                    new Tuple<string, string>("submit_button", "Wireless_Basic"),
                    new Tuple<string, string>("action", "ApplyTake"),
                    new Tuple<string, string>("change_action", "gozila_cgi"),
                    new Tuple<string, string>("submit_type", "save"),
                    new Tuple<string, string>("wl0_nctrlsb", ""),
                    new Tuple<string, string>("wl1_nctrlsb", ""),
                    new Tuple<string, string>("iface", ""),
                    new Tuple<string, string>("wl0_mode", "ap"),
                    new Tuple<string, string>("wl0_net_mode", "mixed"), 
                    new Tuple<string, string>("wl0_ssid", ssid),
                    new Tuple<string, string>("wl0_channel", "6"),

                    new Tuple<string, string>("wl0_closed", "0")
                }
                        );


        }

        public override void ChangeSSID(string ssid)
        {
            _ssid = ssid;
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
