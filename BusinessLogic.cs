using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ReachOutVeevaPromoMats.Configurations;
using Microsoft.VisualBasic.FileIO;
using ReachOutAuth.Models.Products;
using Microsoft.VisualBasic.ApplicationServices;
using ReachOutVeevaPromoMats.API;
using ReachOutVeevaPromoMats.Models.API;
using ReachOutAuth.Models.Messages;
using System.Runtime.CompilerServices;
using RestSharp;
using System.Reflection;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ReachOutAuth.Models.Authentication;
using ReachOutAuth.Models.VeevaPromoMats;
using ReachOutAuth.Models.VeevaPromoMats.Data.PropertyMappings;

/// <summary>
/// The ReachOutVeevaPromoMats namespace.
/// </summary>
namespace ReachOutVeevaPromoMats
{
    /// <summary>
    /// The buisness logic of the service
    /// </summary>
    public class BusinessLogic
    {
        /// <summary>
        /// Parses the CSV.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>ProductRequest.</returns>

        public static VeevaPromoMatsRequest ParseCSV(string path)
        {
            VeevaPromoMatsRequest request = new VeevaPromoMatsRequest()
            {
                ClientID = TenantConfiguration.ReachOutClientID,
                Data = new JArray()
            };

            try
            {
                using (TextFieldParser csvParser = new TextFieldParser(path))
                {
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    string[] headerfields = csvParser.ReadFields();

                    while (!csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();

                        JObject product = new JObject();
                        for(int i = 0; i < headerfields.Length; i++)
                        {
                            product.Add(headerfields[i], fields[i]);
                        }

                        request.Data.Add(product);
                    }

                    csvParser.Close();
                }
            }
            catch (Exception e)
            {
                TenantConfiguration.Logger.Error(e, e.Message);
            }

            return request;
        }
        
        public static void SweepAPIForProducts()
        {
            VeevaPromoMatsRequest request = new VeevaPromoMatsRequest()
            {
                ClientID = TenantConfiguration.ReachOutClientID
            };

            try
            {
                TenantConfiguration.Logger.Info($"Sweeping API for proudct updates since {DateTime.Now.AddHours(-TenantConfiguration.VeevaSweepTimeOverlapHours - 24).ToString()}.");

                VeevaAPIResponse response = VeevaAPI.Query(GenerateQuery());
                
                List<VeevaAPIResponseData> responses = new List<VeevaAPIResponseData>();
                if(response.Data != null)
                {
                    foreach (JObject data in response.Data)
                    {
                        request.Data.Add(data);
                    }

                    while (!string.IsNullOrEmpty(response.ResponseDetails.NextPage))
                    {
                        response = VeevaAPI.NextPage(response.ResponseDetails.NextPage);

                        foreach (JObject data in response.Data)
                        {
                            request.Data.Add(data);
                        }
                    }

                    TenantConfiguration.Logger.Info($"Found {request.Data.Count()} products updates.");

                    if (!TenantConfiguration.Debug)
                    {
                        if (request.Data.Count() > 0)
                        {
                            string r = SendProductRequest(request);
                            string formatted;
                            try
                            {
                                JObject obj = JsonConvert.DeserializeObject<JObject>(r);
                                formatted = JsonConvert.SerializeObject(obj, Formatting.Indented);
                            }
                            catch (Exception ex)
                            {
                                formatted = r;
                            }

                            TenantConfiguration.Logger.Info(formatted);
                        }
                    }
                    else
                    {
                        TenantConfiguration.Logger.Info(JsonConvert.SerializeObject(responses, Formatting.Indented));
                    }
                }
                else
                {
                    TenantConfiguration.Logger.Info(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
            }
            catch (Exception e)
            {
                TenantConfiguration.Logger.Error(e, e.Message);
            }
        }

        /// <summary>
        /// Generates the query.
        /// </summary>
        /// <returns>System.String.</returns>
        private static string GenerateQuery()
        {
            string query = "select ";
            Dictionary<string, String> mappings = VeevaPromoMatsDocumentMapper.GetClientDocumentMapping(TenantConfiguration.ReachOutClientID);

            // Modified Date is UTC so we need to go back 24 hours from now in UTC time
            DateTimeOffset timeSinceLastSweepUtc = DateTime.UtcNow.AddHours(-TenantConfiguration.VeevaSweepTimeOverlapHours - 24);

            for(int i = 0; i < mappings.Count(); i++)
            {
                query += $"{mappings.ElementAt(i).Key}";

                if(i == mappings.Count() - 1)
                {
                    query += " ";
                }
                else
                {
                    query += ", ";
                }
            }

            query += $"from documents where minor_version_number__v = 0 and version_modified_date__v > '{timeSinceLastSweepUtc.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'")}' pagesize {TenantConfiguration.VeevaResultPageSize}";

            return query;
        }

        /// <summary>
        /// Sends the veeva request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Authentication failed for user " + auth.UserName</exception>
        public static string SendVeevaRequest(VeevaPromoMatsRequest veevaRequest)
        {
            if (!TenantConfiguration.Debug)
            {
                Authenticator auth = new Authenticator(TenantConfiguration.AbleAuthUser, TenantConfiguration.AbleAuthSecret, TenantConfiguration.AbleAuthUrl, TenantConfiguration.AbleAuthAction);
                if (!auth.IsAuthenticated)
                {
                    throw new Exception("Authentication failed for user " + auth.UserName);
                }

                var options = new RestClientOptions(TenantConfiguration.AbleAuthUrl)
                {
                    MaxTimeout = int.MaxValue
                };

                RestClient client = new RestClient(options);

                string requestBody = JsonConvert.SerializeObject(veevaRequest);

                RestRequest request = new RestRequest(TenantConfiguration.AbleVeevaAction, Method.Post);
                request.AddHeader("Authorization", "Bearer " + auth.Token);
                request.AddParameter("application/json", JsonConvert.SerializeObject(veevaRequest), ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                return client.Execute(request).Content;
            }

            return string.Empty;
        }

        /// <summary>
        /// Sends the product request.
        /// </summary>
        /// <param name="productRequest">The product request.</param>
        /// <exception cref="System.Exception">Authentication failed for user " + auth.UserName</exception>
        public static string SendProductRequest(VeevaPromoMatsRequest productRequest)
        {
            if (!TenantConfiguration.Debug)
            {
                Authenticator auth = new Authenticator(TenantConfiguration.AbleAuthUser, TenantConfiguration.AbleAuthSecret, TenantConfiguration.AbleAuthUrl, TenantConfiguration.AbleAuthAction);
                if (!auth.IsAuthenticated)
                {
                    throw new Exception("Authentication failed for user " + auth.UserName);
                }

                var options = new RestClientOptions(TenantConfiguration.AbleAuthUrl)
                {
                    MaxTimeout = int.MaxValue
                };

                RestClient client = new RestClient(options);

                RestRequest request = new RestRequest(TenantConfiguration.AbleVeevaAction, Method.Post);
                request.AddHeader("Authorization", "Bearer " + auth.Token);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(JsonConvert.SerializeObject(productRequest));

                return client.Execute(request).Content;
            }

            return string.Empty;
        }

        /// <summary>
        /// Processes the dropped files.
        /// </summary>
        public static void ProcessDroppedFiles()
        {
            try
            {
                foreach (string file in Directory.GetFiles(TenantConfiguration.DropFolder))
                {
                    try
                    {
                        VeevaPromoMatsRequest productRequest = ParseCSV(file);                        
                        string response = SendVeevaRequest(productRequest);

                        if (TenantConfiguration.ArchiveDropFiles)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file);
                            string directory = Path.GetDirectoryName(file);

                            if (!Directory.Exists(Path.Combine(directory, "Archive")))
                            {
                                Directory.CreateDirectory(Path.Combine(directory, "Archive"));
                            }

                            string archiveFile = Path.Combine(directory, "Archive", fileName + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss.fff") + ".csv");

                            File.Copy(file, Path.Combine(directory, "Archive", archiveFile));
                        }

                        File.Delete(file);

                        try
                        {
                            TenantConfiguration.Logger.Info($"Response: {JObject.Parse(response).ToString()}");
                        }
                        catch
                        {
                            TenantConfiguration.Logger.Info($"Response: {response}");
                        }
                    }
                    catch(Exception e)
                    {
                        TenantConfiguration.Logger.Error(e, "Error processing drop file: {0} - {1}", file, e.Message + ":" + e.StackTrace);
                    }
                }
            }
            catch (Exception e)
            {
                TenantConfiguration.Logger.Error(e, e.Message);
            }
        }

        /// <summary>
        /// Gets the data from column.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="headerfields">The headerfields.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The field value</returns>
        private static string GetDataFromColumn(string field, string[] fields, string[] headerfields, string defaultValue)
        {
            string value = string.Empty;
            if(fields != null && headerfields != null && fields.Length == headerfields.Length) 
            {
                int index = Array.IndexOf(headerfields, field);
                if (index > -1)
                {
                    value = fields[index];
                }
            }

            if(string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            return value.Replace("$", "");
        }
    }
}
