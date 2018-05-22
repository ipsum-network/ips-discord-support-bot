using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordSupportBot.Common
{
    public class JsonRpc
    {
        public JsonRpc(string url, NetworkCredential credentials)
        {
            this.Url = url;
            this.Credentials = credentials;
        }

        public string Url { get; set; }
        public NetworkCredential Credentials { get; set; }

        public string InvokeMethod(string a_sMethod, params object[] a_params)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(this.Url);
            webRequest.Credentials = this.Credentials;

            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            JObject joe = new JObject
            {
                ["jsonrpc"] = "1.0",
                ["id"] = "1",
                ["method"] = a_sMethod
            };

            if (a_params != null && a_params.Length > 0)
            {
                joe.Add(new JProperty("params", new JArray { a_params }));
            }

            string s = JsonConvert.SerializeObject(joe);
            // serialize json for the request
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (WebException we)
            {
                Console.WriteLine(we.Message);
                throw;
            }

            WebResponse webResponse = null;

            try
            {
                using (webResponse = webRequest.GetResponse())
                {
                    using (Stream str = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                using (Stream str = webex.Response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        return sr.ReadToEnd();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}