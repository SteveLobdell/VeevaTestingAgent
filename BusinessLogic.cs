using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ReachOutVeevaPromoMats.Configurations;
using Microsoft.VisualBasic.FileIO;
using ReachOutAuth.Models.Products;
using ReachOutVeevaPromoMats.API;
using ReachOutVeevaPromoMats.Models.API;
using ReachOutAuth.Models.Messages;
using System.Runtime.CompilerServices;
using RestSharp;
using System.Reflection;
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
    /// The business logic of the service
    /// </summary>
    public class BusinessLogic
    {
        private readonly AppConfiguration _configuration;

        public BusinessLogic(AppConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Parses the CSV.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>ProductRequest.</returns>

        public VeevaPromoMatsRequest ParseCSV(string path)
        {
            VeevaPromoMatsRequest request = new VeevaPromoMatsRequest()
            {
                ClientID = _configuration.ReachOutClientID,
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
                _configuration.Logger.Error(e, e.Message);
            }

            return request;
        }
        
        public void SweepAPIForProducts()
        {
            VeevaPromoMatsRequest request = new VeevaPromoMatsRequest()
            {
                ClientID = _configuration.ReachOutClientID
            };

            try
            {
                _configuration.Logger.Info($"Sweeping API for product updates since {DateTime.Now.AddHours(-_configuration.VeevaSweepTimeOverlapHours - 24).ToString()}.");

                var veevaAPI = new VeevaAPI(_configuration);
                VeevaAPIResponse response = veevaAPI.Query(GenerateQuery());
                
                List<VeevaAPIResponseData> responses = new List<VeevaAPIResponseData>();
                if(response.Data != null)
                {
                    foreach (JObject data in response.Data)
                    {
                        request.Data.Add(data);
                    }

                    while (!string.IsNullOrEmpty(response.ResponseDetails.NextPage))
                    {
                        response = veevaAPI.NextPage(response.ResponseDetails.NextPage);

                        foreach (JObject data in response.Data)
                        {
                            request.Data.Add(data);
                        }
                    }

                    _configuration.Logger.Info($"Found {request.Data.Count()} products updates.");

                    if (!_configuration.Debug)
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

                            _configuration.Logger.Info(formatted);
                        }
                    }
                    else
                    {
                        _configuration.Logger.Info(JsonConvert.SerializeObject(responses, Formatting.Indented));
                    }
                }
                else
                {
                    _configuration.Logger.Info(JsonConvert.SerializeObject(response, Formatting.Indented));
                }
            }
            catch (Exception e)
            {
                _configuration.Logger.Error(e, e.Message);
            }
        }

        /// <summary>
        /// Generates the query.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GenerateQuery()
        {
            string query = "select ";
            Dictionary<string, String> mappings = VeevaPromoMatsDocumentMapper.GetClientDocumentMapping(_configuration.ReachOutClientID);

            // Modified Date is UTC so we need to go back 24 hours from now in UTC time
            DateTimeOffset timeSinceLastSweepUtc = DateTime.UtcNow.AddHours(-_configuration.VeevaSweepTimeOverlapHours - 24);

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

            query += $"from documents where minor_version_number__v = 0 and version_modified_date__v > '{timeSinceLastSweepUtc.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'")}' pagesize {_configuration.VeevaResultPageSize}";

            return query;
        }

        /// <summary>
        /// Sends the veeva request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">Authentication failed for user " + auth.UserName</exception>
        public string SendVeevaRequest(VeevaPromoMatsRequest veevaRequest)
        {
            if (!_configuration.Debug)
            {
                Authenticator auth = new Authenticator(_configuration.AbleAuthUser, _configuration.AbleAuthSecret, _configuration.AbleAuthUrl, _configuration.AbleAuthAction);
                if (!auth.IsAuthenticated)
                {
                    throw new Exception("Authentication failed for user " + auth.UserName);
                }

                var options = new RestClientOptions(_configuration.AbleAuthUrl)
                {
                    MaxTimeout = int.MaxValue
                };

                RestClient client = new RestClient(options);

                string requestBody = JsonConvert.SerializeObject(veevaRequest);

                RestRequest request = new RestRequest(_configuration.AbleVeevaAction, Method.Post);
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
        public string SendProductRequest(VeevaPromoMatsRequest productRequest)
        {
            if (!_configuration.Debug)
            {
                Authenticator auth = new Authenticator(_configuration.AbleAuthUser, _configuration.AbleAuthSecret, _configuration.AbleAuthUrl, _configuration.AbleAuthAction);
                if (!auth.IsAuthenticated)
                {
                    throw new Exception("Authentication failed for user " + auth.UserName);
                }

                var options = new RestClientOptions(_configuration.AbleAuthUrl)
                {
                    MaxTimeout = int.MaxValue
                };

                RestClient client = new RestClient(options);

                RestRequest request = new RestRequest(_configuration.AbleVeevaAction, Method.Post);
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
        public void ProcessDroppedFiles()
        {
            try
            {
                foreach (string file in Directory.GetFiles(_configuration.DropFolder))
                {
                    try
                    {
                        VeevaPromoMatsRequest productRequest = ParseCSV(file);                        
                        string response = SendVeevaRequest(productRequest);

                        if (_configuration.ArchiveDropFiles)
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
                            _configuration.Logger.Info($"Response: {JObject.Parse(response).ToString()}");
                        }
                        catch
                        {
                            _configuration.Logger.Info($"Response: {response}");
                        }
                    }
                    catch(Exception e)
                    {
                        _configuration.Logger.Error(e, "Error processing drop file: {0} - {1}", file, e.Message + ":" + e.StackTrace);
                    }
                }
            }
            catch (Exception e)
            {
                _configuration.Logger.Error(e, e.Message);
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
