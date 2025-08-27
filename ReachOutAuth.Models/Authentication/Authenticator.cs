using Newtonsoft.Json;
using ReachOutAuth.Models.Users;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ReachOutAuth.Models.Authentication
{
    /// <summary>
    ///   The Authenticator class
    /// </summary>
    public class Authenticator
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        public string Token { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is authenticated.</summary>
        /// <value>
        ///   <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.</value>
        public bool IsAuthenticated { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Authenticator" /> class.</summary>
        public Authenticator(string user, string password, string authUrl, string authAction)
        {
            UserCred u = new UserCred()
            {
                Username = user,
                Password = password
            };

            string creds = JsonConvert.SerializeObject(u);
            this.IsAuthenticated = false;
            this.Token = string.Empty;

            var client = new RestClient(authUrl);
            var request = new RestRequest(authAction, Method.Post);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(u);

            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                this.Token = response.Content.Replace("\"", string.Empty);
                this.IsAuthenticated = true;
                this.UserName = u.Username;
            }
        }
    }
}
