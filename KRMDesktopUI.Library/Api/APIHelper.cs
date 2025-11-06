using KRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KRMDesktopUI.Library.Api
{
    public class APIHelper : IAPIHelper
    {
        private ILoggedInUserModels _loggedInUser;

        public HttpClient _apiclient;

        public APIHelper(ILoggedInUserModels loggedInUser)
        {
            IntializeClient();
            _loggedInUser = loggedInUser;
        }
        public HttpClient ApiClient
        {
            get { return _apiclient; }
        }

        private void IntializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];
            _apiclient = new HttpClient();
            _apiclient.BaseAddress = new Uri(api);
            _apiclient.DefaultRequestHeaders.Accept.Clear();
            _apiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string,string>("grant_type", "password"),
                 new KeyValuePair<string,string>("username", username),
                 new KeyValuePair<string,string>("password", password),
            });

            using (HttpResponseMessage response = await _apiclient.PostAsync("/Token", data))
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

        public async Task GetLoggedInUserInfo(string Token)
        {
            _apiclient.DefaultRequestHeaders.Clear();
            _apiclient.DefaultRequestHeaders.Accept.Clear();
            _apiclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiclient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");

            using (HttpResponseMessage response = await _apiclient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsAsync<LoggedInUserModels>();
                    _loggedInUser.CreatedDate = res.CreatedDate;
                    _loggedInUser.Id = res.Id;
                    _loggedInUser.EmailAddress = res.EmailAddress;
                    _loggedInUser.FirstName = res.FirstName;
                    _loggedInUser.LastName = res.LastName;
                    _loggedInUser.Token = res.Token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
