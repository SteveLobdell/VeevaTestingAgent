using Newtonsoft.Json;
using ReachOutAuth.Models.Messages;
using ReachOutAuth.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace ReachOutVeevaPromoMats.Models.API
{
    /// <summary>
    /// Class VeevaAPIResponseData.
    /// </summary>
    public class VeevaAPIResponseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VeevaAPIResponseData"/> class.
        /// </summary>
        public VeevaAPIResponseData()
        {
            this.Products = new List<string>();
            this.SubProducts = new List<string>();
            this.IntendedAudiences = new List<string>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("status__v")]
        public string Status { get; set; }

        [JsonProperty("name__v")]
        public string DocumentName { get; set; }

        /// <summary>
        /// Gets or sets the document number.
        /// </summary>
        /// <value>The document number.</value>
        [JsonProperty("document_number__v")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>The product.</value>
        [JsonProperty("product__v")]
        public List<string> Products { get; set; }

        /// <summary>
        /// Gets or sets the sub product.
        /// </summary>
        /// <value>The sub product.</value>
        [JsonProperty("subproduct__c")]
        public List<string> SubProducts { get; set; }

        /// <summary>
        /// Gets or sets the intended audience.
        /// </summary>
        /// <value>The intended audience.</value>
        [JsonProperty("audience1__c")]
        public List<string> IntendedAudiences { get; set; }

        /// <summary>
        /// Gets or sets the FDM description.
        /// </summary>
        /// <value>The FDM description.</value>
        [JsonProperty(" fdm_description__c")]
        public string FDMDescription { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [JsonProperty("description__v")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>The version number.</value>
        [JsonProperty("major_version_number__v")]
        public int VersionMajor { get; set; }

        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        /// <value>The version number.</value>
        [JsonProperty("minor_version_number__v")]
        public int VersionMinor { get; set; }

        /// <summary>
        /// Gets or sets the initial in review date.
        /// </summary>
        /// <value>The initial in review date.</value>
        [JsonProperty("initial_inreview_date__v")]
        public DateTimeOffset? InitialInReviewDate { get; set; }

        /// <summary>
        /// Gets or sets the version modified date.
        /// </summary>
        /// <value>The version modified date.</value>
        [JsonProperty("version_modified_date__v")]
        public DateTimeOffset? VersionModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the version creation date.
        /// </summary>
        /// <value>The version creation date.</value>
        [JsonProperty("version_creation_date__v")]
        public DateTimeOffset? VersionCreationDate { get; set; }

        /// <summary>
        /// Gets or sets the initial steady state date.
        /// </summary>
        /// <value>The initial steady state date.</value>
        [JsonProperty("initial_steadystate_date__v")]
        public DateTimeOffset? InitialSteadyStateDate { get; set; }

        /// <summary>
        /// Gets or sets the CMRB meeting date.
        /// </summary>
        /// <value>The CMRB meeting date.</value>
        [JsonProperty("cmrb_meeting_date__c")]
        public DateTimeOffset? CMRBMeetingDate { get; set; }

        /// <summary>
        /// Gets or sets the review cycle start date.
        /// </summary>
        /// <value>The review cycle start date.</value>
        [JsonProperty("review_cycle_start_date__c")]
        public DateTimeOffset? ReviewCycleStartDate { get; set; }

        /// <summary>
        /// Gets or sets the approved for distribution date.
        /// </summary>
        /// <value>The approved for distribution date.</value>
        [JsonProperty("approved_for_distribution_date__c")]
        public DateTimeOffset? ApprovedForDistributionDate { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>The expiration date.</value>
        [JsonProperty("expiration_date__c")]
        public DateTimeOffset? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the approved for production date4.
        /// </summary>
        /// <value>The approved for production date4.</value>
        [JsonProperty("approved_for_production_date__c")]
        public DateTimeOffset? ApprovedForProductionDate { get; set; }

        /// <summary>
        /// Gets or sets the planned date of first use date.
        /// </summary>
        /// <value>The planned date of first use date.</value>
        [JsonProperty("planned_date_of_first_use__c")]
        public DateTimeOffset? PlannedDateOfFirstUseDate { get; set; }

        /// <summary>
        /// Converts to reachoutproduct.
        /// </summary>
        /// <returns>ReachOutProduct.</returns>
        public ReachOutProduct ToReachOutProduct()
        {
            List<string> inactiveStatuses = new List<string>() { "inactive", "expired", "superseded" };

            ReachOutProduct product = new ReachOutProduct()
            {
                Expired = this.Status.ToLower() == "expired" || (this.ExpirationDate != null && this.ExpirationDate < DateTime.UtcNow),
                Sku = this.DocumentNumber,
                Name = this.DocumentName,
                Description = this.Description,
                ExternalStatus = this.Status,
                Products = this.Products,
                SubProducts = this.SubProducts,
                IntendedAudiences = this.IntendedAudiences,
                VersionMajor = this.VersionMajor,
                FDMDescription = this.FDMDescription,
                DocumentID = this.ID
            };

            product.Active = !inactiveStatuses.Contains(this.Status.ToLower()) && !product.Expired;

            if (this.ExpirationDate.HasValue)
            {
                product.ExternalExpirationDate = this.ExpirationDate.Value.Date;
                product.ExpirationDate = this.ExpirationDate.Value.Date;
            }
            else if (!product.Active)
            {
                product.ExpirationDate = DateTime.Now.Date;
            }

            return product;
        }
    }
}
