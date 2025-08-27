using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

/// <summary>
/// The Products namespace.
/// </summary>
namespace ReachOutAuth.Models.Products
{
   /// <summary>
   /// Error reason enum
   /// </summary>
   public enum ReachOutProductErrorCode
   {
      /// <summary>
      /// The none
      /// </summary>
      None,

      /// <summary>
      /// The missing required fields
      /// </summary>
      MissingRequiredFields,

      /// <summary>
      /// The fatal error while parsing CSV
      /// </summary>
      FatalErrorParsingData
   }

   /// <summary>
   /// Reachout Product
   /// </summary>
   public class ReachOutProduct
   {

      /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>The identifier.</value>
      public int ProductID { get; set; }

      /// <summary>
      /// Gets or sets the sku.
      /// </summary>
      /// <value>The sku.</value>
      public string Sku { get; set; }

      /// <summary>
      /// Gets or sets the name.
      /// </summary>
      /// <value>The name.</value>
      public string Name { get; set; }

      /// <summary>
      /// Gets or sets the title.
      /// </summary>
      /// <value>The title.</value>
      public string Title { get; set; }

      /// <summary>
      /// Gets or sets the category.
      /// </summary>
      /// <value>The category.</value>
      public string Category { get; set; }

      /// <summary>
      /// Gets or sets the description.
      /// </summary>
      /// <value>The description.</value>
      public string Description { get; set; }

      /// <summary>
      /// Gets or sets the create date.
      /// </summary>
      /// <value>The create date.</value>
      public DateTime? CreateDate { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="ReachOutProduct" /> is active.
      /// </summary>
      /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
      public bool Active { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="ReachOutProduct" /> is expired.
      /// </summary>
      /// <value><c>true</c> if expired; otherwise, <c>false</c>.</value>
      public bool Expired { get; set; }

      /// <summary>
      /// Gets or sets the external identifier.
      /// </summary>
      /// <value>The external identifier.</value>
      public int ExternalID { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [leave behind].
      /// </summary>
      /// <value><c>true</c> if [leave behind]; otherwise, <c>false</c>.</value>
      public bool LeaveBehind { get; set; }

      /// <summary>
      /// Gets or sets the brand.
      /// </summary>
      /// <value>The brand.</value>
      public string Brand { get; set; }

      /// <summary>
      /// Gets or sets the country.
      /// </summary>
      /// <value>The country.</value>
      public string Country { get; set; }

      /// <summary>
      /// Gets or sets the field group.
      /// </summary>
      /// <value>The field group.</value>
      public string FieldGroup { get; set; }

      /// <summary>
      /// Gets or sets the product manager.
      /// </summary>
      /// <value>The product manager.</value>
      public string ProductManager { get; set; }

      /// <summary>
      /// Gets or sets the cost.
      /// </summary>
      /// <value>The cost.</value>
      public decimal Cost { get; set; }

      /// <summary>
      /// Gets or sets the intended audiences.
      /// </summary>
      /// <value>The intended audiences.</value>
      public List<string> IntendedAudiences { get; set; }

      /// <summary>
      /// Gets or sets the low stock email project manager.
      /// </summary>
      /// <value>The low stock email project manager.</value>
      public string LowStockEmailProjectManager { get; set; }

      /// <summary>
      /// Gets or sets the active date.
      /// </summary>
      /// <value>The active date.</value>
      public DateTime? ActiveDate { get; set; }

      /// <summary>
      /// Gets or sets the expiration date.
      /// </summary>
      /// <value>The expiration date.</value>
      public DateTime? ExpirationDate { get; set; }

      /// <summary>
      /// Gets or sets the expiration date.
      /// </summary>
      /// <value>The expiration date.</value>
      public DateTime? ExternalExpirationDate { get; set; }

      /// <summary>
      /// Gets or sets the obsolete date.
      /// </summary>
      /// <value>The obsolete date.</value>
      public DateTime? ObsoleteDate { get; set; }

      /// <summary>
      /// Gets or sets the review date.
      /// </summary>
      /// <value>The review date.</value>
      public DateTime? ReviewDate { get; set; }

      /// <summary>
      /// Gets or sets the review email address.
      /// </summary>
      /// <value>The review email address.</value>
      public string ReviewEmailAddress { get; set; }

      /// <summary>
      /// Gets or sets the error reason.
      /// </summary>
      /// <value>The error reason.</value>
      public ReachOutProductErrorCode ErrorCode { get; set; }

      /// <summary>
      /// Gets or sets the version number.
      /// </summary>
      /// <value>The version number.</value>
      public int VersionMajor { get; set; }

      /// <summary>
      /// Gets or sets the products.
      /// </summary>
      /// <value>The products.</value>
      public List<string> Products { get; set; }

      /// <summary>
      /// Gets or sets the sub products.
      /// </summary>
      /// <value>The sub products.</value>
      public List<string> SubProducts { get; set; }

      /// <summary>
      /// Gets or sets the FDM description.
      /// </summary>
      /// <value>The FDM description.</value>
      public string FDMDescription { get; set; }

      /// <summary>
      /// Gets or sets the status.
      /// </summary>
      /// <value>The status.</value>
      public string ExternalStatus { get; set; }

      /// <summary>
      /// Gets or sets the document identifier.
      /// </summary>
      /// <value>The document identifier.</value>
      public string DocumentID { get; set; }

      /// <summary>
      /// Gets or sets the type of the document.
      /// </summary>
      /// <value>The type of the document.</value>
      public string DocumentType { get; set; }

      /// <summary>
      /// Gets or sets the type of the document sub.
      /// </summary>
      /// <value>The type of the document sub.</value>
      public string DocumentSubType { get; set; }

      /// <summary>
      /// Gets or sets the branding.
      /// </summary>
      /// <value>The branding.</value>
      public string Branding { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [prescribing information required].
      /// </summary>
      /// <value><c>true</c> if [prescribing information required]; otherwise, <c>false</c>.</value>
      public bool PrescribingInfoRequired { get; set; }

      /// <summary>
      /// Gets or sets the audience.
      /// </summary>
      /// <value>The audience.</value>
      public string Audience { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [requires FDM].
      /// </summary>
      /// <value><c>true</c> if [requires FDM]; otherwise, <c>false</c>.</value>
      public bool RequiresFDM { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [fulfillment FGS].
      /// </summary>
      /// <value><c>true</c> if [fulfillment FGS]; otherwise, <c>false</c>.</value>
      public bool FulfillmentFGS { get; set; }

      /// <summary>
      /// Gets or sets the internal users.
      /// </summary>
      /// <value>The internal users.</value>
      public string InternalUsers { get; set; }

      /// <summary>
      /// Gets or sets the method of devliery.
      /// </summary>
      /// <value>The method of devliery.</value>
      public string MethodOfDelivery { get; set; }

      /// <summary>
      /// Gets or sets the name of the veeva document.
      /// </summary>
      /// <value>The name of the veeva document.</value>
      public string VeevaDocumentName { get; set; }
   }
}
