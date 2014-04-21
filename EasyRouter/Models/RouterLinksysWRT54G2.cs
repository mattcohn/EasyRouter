using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public class RouterLinksysWRT54G2 : Router
    {

        private string _ssid;
        private string _password;

        public RouterLinksysWRT54G2(string requestUriString)
            : base(requestUriString)
        {
            _ssid = "";
            _password = "";
        }

        public override string Model
        {
            get { return "Linksys WRT-54-G2"; }
        }

        public override string ImageFilename
        {
            get { return "WRT54G2.jpg"; }
        }


        public override void Logon()
        {
            SendToRouter("", "GET",

                new List<Tuple<string, string>> {
                    new Tuple<string, string>("Authorization", "Basic OmFkbWlu") },
                        
                new List<Tuple<string, string>> {}
            );
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        protected void SendToRouter(string path, string httpVerb, IEnumerable<Tuple<string, string>> headers, IEnumerable<Tuple<string, string>> formData)
        {
            if (path.Length > 1 && !path.Substring(0, 1).Equals("/") ) path = "/" + path; // prepend leading / in url path

            HttpWebRequest hwr = WebRequest.CreateHttp(_requestUriString + path);
            hwr.Method = httpVerb;
            hwr.ContentType = "application/x-www-form-urlencode";
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

            if (hwr.Method == "POST") { 
               byte[] formBytes = GetFormData(formData);
                hwr.ContentLength = formBytes.Length;

                using (Stream stream = hwr.GetRequestStream())
                {
                    stream.Write(formBytes, 0, formBytes.Length);
                }
            }
            try
            {
                WebResponse response = hwr.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    string responseString = reader.ReadToEnd();
                }
            }
            catch(WebException)
            {

            }


        }

        public override void ChangeSSID(string ssid)
        {
            SendToRouter("WBasic.tri", "POST",

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
            SendToRouter("Security.tri", "POST",

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
