using KRMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace KRMDesktopUI.Helpers
{
    public class APIHelper : IAPIHelper
    {
        public HttpClient apiclient { get; set; }

        public APIHelper()
        {
            IntializeClient();
        }

        private void IntializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];
            apiclient = new HttpClient();
            apiclient.BaseAddress = new Uri(api);
            apiclient.DefaultRequestHeaders.Accept.Clear();
            apiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string,string>("grant_type", "password"),
                 new KeyValuePair<string,string>("username", username),
                 new KeyValuePair<string,string>("password", password),
            });

            using (HttpResponseMessage response = await apiclient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
