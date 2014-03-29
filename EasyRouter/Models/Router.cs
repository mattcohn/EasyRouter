using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.Models
{
    public abstract class Router
    {
        protected string _requestUriString;

        public Router(string requestUriString)
        {
            _requestUriString = requestUriString;
        }

        public abstract string Model { get; }
        public abstract string ImageFilename { get; } 

        public abstract void Logon();
        public abstract void ChangeSSID(string ssid);
        public abstract string GetSSID();
        public abstract void ChangeWifiPassword(string password);
        public abstract string GetWifiPassword();

        protected void SendToRouter(string path, IEnumerable<Tuple<string, string>> headers, IEnumerable<Tuple<string, string>> formData)
        {
            HttpWebRequest hwr = WebRequest.CreateHttp(_requestUriString + path);
            hwr.Method = "POST";
            hwr.ContentType = "application/x-www-form-urlencode";

            foreach (Tuple<string, string> header in headers)
            {
                hwr.Headers[header.Item1] = header.Item2;
            }

            byte[] formBytes = GetFormData(formData);
            hwr.ContentLength = formBytes.Length;

            using (Stream stream = hwr.GetRequestStream())
            {
                stream.Write(formBytes, 0, formBytes.Length);
            }

            WebResponse response = hwr.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                string responseString = reader.ReadToEnd();
            }
        }

        protected byte[] GetFormData(IEnumerable<Tuple<string, string>> formData)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Tuple<string, string> singleFormData in formData)
            {
                if (builder.Length > 0)
                {
                    builder.Append("&");
                }
                builder.Append(singleFormData.Item1);
                builder.Append("=");
                builder.Append(singleFormData.Item2);
            }

            return Encoding.ASCII.GetBytes(builder.ToString());
        }
    }
}
