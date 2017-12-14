using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PIWebAPI_Demo
{
    public class PIWebAPIClient
    {
        private HttpClient client;

        public PIWebAPIClient(string userName, string password)
        {
            client = new HttpClient();
            string authInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password)));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);

            // Just to show we can control cache access, lets add a Cache-Control header. It will slow things down a little though...
            //client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            client.Timeout = new TimeSpan(0, 0, 20);

            /* This next line is needed to handle errors returned when using a self-signed certificate
             * Wouldn't use this in production - should use CA signed certificates instead */
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, SslPolicyErrors) => true;
        }

        public async Task<JObject> GetAsync(string uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = "Response status code does not indicate success: " + (int)response.StatusCode;
                throw new HttpRequestException(response + Environment.NewLine + content);
            }
            return JObject.Parse(content);
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent httpContent)
        {
            HttpResponseMessage response = await client.PostAsync(uri, httpContent);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = "Response status code does not indicate success: " + (int)response.StatusCode;
                throw new HttpRequestException(response + Environment.NewLine + content);
            }
            return response;
        }

        public async Task<JObject> PutAsync(string uri, StringContent httpContent)
        {
            HttpResponseMessage response = await client.PutAsync(uri, httpContent);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var responseMessage = "Response status code does not indicate success: " + (int)response.StatusCode;
                throw new HttpRequestException(response + Environment.NewLine + content);
            }
            return JObject.Parse(content);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
