using Newtonsoft.Json;
using NLog;
using ReachOutVeevaPromoMats.Configurations;
using ReachOutVeevaPromoMats.Models.API;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ReachOutVeevaPromoMats.API
{
    /// <summary>
    ///   Class to encapsulate calls to the Veeva API
    /// </summary>
    public static class VeevaAPI
    {
        /// <summary>The session identifier</summary>
        private static string SessionID;

        /// <summary>Queries the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   The response object
        /// </returns>
        public static VeevaAPIResponse Query(string query)
        {
            VeevaAPIResponse response = new VeevaAPIResponse();

            try
            {
                RestClient client = new RestClient(TenantConfiguration.GetVeevaAPIEndpoint());

                RestRequest request = CreateRequest("/query", Method.Post);

                request.AddParameter("q", query);


                response = ExecuteRequest(client, request);
            }
            catch (Exception e) 
            {
                TenantConfiguration.Logger.Error(e, "Error occurred communicating with Veeva API: {0}.", e.Message);
            }

            return response;
        }

        public static VeevaAPIResponse NextPage(string nextPageUrl)
        {
            VeevaAPIResponse response = new VeevaAPIResponse();

            try
            {
                RestClient client = new RestClient(TenantConfiguration.GetVeevaAPIBaseAddress());

                RestRequest request = CreateRequest(nextPageUrl, Method.Post);

                response = ExecuteRequest(client, request);
            }
            catch (Exception e)
            {
                TenantConfiguration.Logger.Error(e, "Error occurred communicating with Veeva API: {0}.", e.Message);
            }

            return response;
        }

        /// <summary>Authorizes this instance.</summary>
        /// <returns>
        ///   Whether we're authorized
        /// </returns>
        private static bool Authorize()
        {
            bool authorized = false;

            try
            {
                RestClient client = new RestClient(TenantConfiguration.GetVeevaAPIEndpoint());

                RestRequest request = CreateRequest("/auth", Method.Post);
                request.AddParameter("username", TenantConfiguration.VeevaAPIUser);
                request.AddParameter("password", TenantConfiguration.VeevaAPIPassword);

                RestResponse apiResponse = client.Execute(request);
                if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    VeevaAPIResponse response = JsonConvert.DeserializeObject<VeevaAPIResponse>(apiResponse.Content);
                    if (response.ResponseStatus != "FAILURE")
                    {
                        authorized = true;
                        SessionID = response.SessionID;
                    }
                }
            }
            catch (Exception e) 
            {
                TenantConfiguration.Logger.Error(e, "Error occurred communicating with Veeva API: {0}.", e.Message);
            }

            return authorized;
        }

        /// <summary>Creates the request.</summary>
        /// <returns>
        ///   The rest request
        /// </returns>
        private static RestRequest CreateRequest(string endpoint, RestSharp.Method method)
        {
            RestRequest request = new RestRequest(endpoint, method);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("X-VaultAPI-DescribeQuery", "true");
            request.AddHeader("Accept", "application/json");

            return request;
        }

        /// <summary>Executes the request.</summary>
        /// <param name="client">The client.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   The API response
        /// </returns>
        private static VeevaAPIResponse ExecuteRequest(RestClient client, RestRequest request, bool reauthorizeIfNeeded = true)
        {
            Authorize();

            request.AddHeader("Authorization", SessionID);

            VeevaAPIResponse response = new VeevaAPIResponse();

            RestResponse apiResponse = client.Post(request);
            if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<VeevaAPIResponse>(apiResponse.Content);
                if (response.ResponseStatus == "FAILURE")
                {
                    foreach(VeevaAPIResponseError error in response.Errors)
                    {
                        if(error.Type == "INVALID_SESSION_ID" && reauthorizeIfNeeded)
                        {
                            SessionID = string.Empty;
                            return ExecuteRequest(client, request, false);
                        }
                    }
                }
            }

            return response;
        }
    }
}
