using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReachOutAuth.Models.Orders
{
    public class OrderAddress
    {
        /// <summary>
        /// Billing first name
        /// </summary>
        [JsonProperty]
        public string FirstName { get; set; }

        /// <summary>
        /// Billing last name
        /// </summary>
        [JsonProperty]
        public string LastName { get; set; }

        /// <summary>
        /// Billing company
        /// </summary>
        [JsonProperty]
        public string Company { get; set; }

        /// <summary>
        /// Billing street address
        /// </summary>
        [JsonProperty]
        public string Street { get; set; }

        /// <summary>
        /// Billing unit or second address line
        /// </summary>
        [JsonProperty]
        public string StreetUnit { get; set; }

        /// <summary>
        /// Billing city
        /// </summary>
        [JsonProperty]
        public string City { get; set; }

        /// <summary>
        /// Billing state or province
        /// </summary>
        [JsonProperty]
        public string State { get; set; }

        /// <summary>
        /// Billing postal code
        /// </summary>
        [JsonProperty]
        public string PostalCode { get; set; }

        /// <summary>
        /// Billing country
        /// </summary>
        [JsonProperty]
        public string Country { get; set; }

        /// <summary>
        /// Billing country
        /// </summary>
        [JsonProperty]
        public string CountryCode { get; set; }

        /// <summary>
        /// Billing phone
        /// </summary>
        [JsonProperty]
        public string Phone { get; set; }

        /// <summary>
        /// Billing e-mail address
        /// </summary>
        [JsonProperty]
        public string Email { get; set; }
    }
}
