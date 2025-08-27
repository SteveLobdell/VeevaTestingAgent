using Newtonsoft.Json;
using ReachOutAuth.Models.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

/// <summary>
/// The API namespace.
/// </summary>
namespace ReachOutAuth.Models.VeevaPromoMats.Data
{
   /// <summary>
   /// Class VeevaAPIResponseData.
   /// </summary>
   public class VeevaPromoMatsDocumentData : VeevaPromoMatsData
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="VeevaPromoMatsDocumentData"/> class.
      /// </summary>
      public VeevaPromoMatsDocumentData()
      {
         Products = new List<string>();
         SubProducts = new List<string>();
         IntendedAudiences = new List<string>();
         Brandings = new List<string>();
         MethodsOfDelivery = new List<string>();
         FieldGroups = new List<string>();
         DocumentClasses = new List<string>();
         InternalUsers = new List<string>();
         DiseaseStates = new List<string>();
         DocumentClasses = new List<string>();
         TherapeuticAreas = new List<string>();
         ProductManagers = new List<string>();
      }

      /// <summary>
      /// Gets or sets the sku.
      /// </summary>
      /// <value>The sku.</value>
      public string Sku { get; set; }

      /// <summary>
      /// Gets or sets the version identifier.
      /// </summary>
      /// <value>The version identifier.</value>
      public string VersionID { get; set; }

      /// <summary>
      /// Gets or sets the status.
      /// </summary>
      /// <value>The status.</value>
      public string Status { get; set; }

      /// <summary>
      /// Gets or sets the name of the document.
      /// </summary>
      /// <value>The name of the document.</value>
      public string DocumentName { get; set; }

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
      /// Gets or sets the document number.
      /// </summary>
      /// <value>The document number.</value>
      public string DocumentNumber { get; set; }

      /// <summary>
      /// Gets or sets the conference.
      /// </summary>
      /// <value>The conference.</value>
      public bool? Conference { get; set; }


      /// <summary>
      /// Gets or sets the product.
      /// </summary>
      /// <value>The product.</value>
      public List<string> Products { get; set; }

      /// <summary>
      /// Gets or sets the product.
      /// </summary>
      /// <value>The product.</value>
      public string Product
      {
         get
         {
                if(Products == null)
            {
               return string.Empty;
            }

            return string.Join(",", Products);
         }
         set
         {
            Products = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the sub product.
      /// </summary>
      /// <value>The sub product.</value>
      public List<string> SubProducts { get; set; }

      /// <summary>
      /// Gets or sets the sub product.
      /// </summary>
      /// <value>The sub product.</value>
      public string SubProduct
      {
         get
         {
            if (SubProducts == null)
            {
               return string.Empty;
            }

            return string.Join(",", SubProducts);
         }
         set
         {
            SubProducts = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the document classes.
      /// </summary>
      /// <value>The document classes.</value>
      public List<string> DocumentClasses { get; set; }

      /// <summary>
      /// Gets or sets the intended audience.
      /// </summary>
      /// <value>The intended audience.</value>
      public string DocumentClass
      {
         get
         {
            if (DocumentClasses == null)
            {
               return string.Empty;
            }

            return string.Join(",", DocumentClasses);
         }
         set
         {
            DocumentClasses = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the intended audience.
      /// </summary>
      /// <value>The intended audience.</value>
      public List<string> IntendedAudiences { get; set; }

      /// <summary>
      /// Gets or sets the intended audience.
      /// </summary>
      /// <value>The intended audience.</value>
      public string IntendedAudience
      {
         get
         {
            if (IntendedAudiences == null)
            {
               return string.Empty;
            }

            return string.Join(",", IntendedAudiences);
         }
         set
         {
            IntendedAudiences = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the internal users.
      /// </summary>
      /// <value>The internal users.</value>
      public List<string> InternalUsers { get; set; }

      public string InternalUser
      {
         get
         {
            if (InternalUsers == null)
            {
               return string.Empty;
            }

            return string.Join(",", InternalUsers);
         }
         set
         {
            InternalUsers = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the therapeutic areas.
      /// </summary>
      /// <value>The therapeutic areas.</value>
      public List<string> TherapeuticAreas { get; set; }

      /// <summary>
      /// Gets or sets the therapeutic area.
      /// </summary>
      /// <value>The therapeutic area.</value>
      public string TherapeuticArea
      {
         get
         {
            if (TherapeuticAreas == null)
            {
               return string.Empty;
            }

            return string.Join(",", TherapeuticAreas);
         }
         set
         {
            TherapeuticAreas = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="VeevaPromoMatsDocumentData"/> is fulfillment.
      /// </summary>
      /// <value><c>true</c> if fulfillment; otherwise, <c>false</c>.</value>
      public bool? Fulfillment { get; set; }

      /// <summary>
      /// Gets or sets the fulfillment method.
      /// </summary>
      /// <value>The fulfillment method.</value>
      public string FulfillmentMethod { get; set; }

      /// <summary>
      /// Gets or sets the FDM description.
      /// </summary>
      /// <value>The FDM description.</value>
      public string FDMDescription { get; set; }

      /// <summary>
      /// Gets or sets the method of delivery.
      /// </summary>
      /// <value>The method of delivery.</value>
      public List<string> MethodsOfDelivery { get; set; }

      /// <summary>
      /// Gets or sets the method of delivery.
      /// </summary>
      /// <value>The method of delivery.</value>
      public string MethodOfDelivery
      {
         get
         {
            if (MethodsOfDelivery == null)
            {
               return string.Empty;
            }

            return string.Join(",", MethodsOfDelivery);
         }
         set
         {
            MethodsOfDelivery = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether [requres FDM].
      /// </summary>
      /// <value><c>true</c> if [requres FDM]; otherwise, <c>false</c>.</value>
      public bool? RequresFDM { get; set; }

      /// <summary>
      /// Gets or sets the description.
      /// </summary>
      /// <value>The description.</value>
      public string Description { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [prescribing information required].
      /// </summary>
      /// <value><c>true</c> if [prescribing information required]; otherwise, <c>false</c>.</value>
      public bool? PrescribingInfoRequired { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [field use].
      /// </summary>
      /// <value><c>true</c> if [field use]; otherwise, <c>false</c>.</value>
      public bool? FieldUse { get; set; }

      /// <summary>
      /// Gets or sets the version number.
      /// </summary>
      /// <value>The version number.</value>
      public int MajorVersionNumber { get; set; }

      /// <summary>
      /// Gets or sets the minor version number.
      /// </summary>
      /// <value>The minor version number.</value>
      public int MinorVersionNumber { get; set; }

      /// <summary>
      /// Gets or sets the thumbnail.
      /// </summary>
      /// <value>The thumbnail.</value>
      public string Thumbnail { get; set; }

      /// <summary>
      /// Gets or sets the branding.
      /// </summary>
      /// <value>The branding.</value>
      public List<string> Brandings { get; set; }

      /// <summary>
      /// Gets or sets the branding.
      /// </summary>
      /// <value>The branding.</value>
      public string Branding
      {
         get
         {
            if (Brandings == null)
            {
               return string.Empty;
            }

            return string.Join(",", Brandings);
         }
         set
         {
            Brandings = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the disease states.
      /// </summary>
      /// <value>The disease states.</value>
      public List<string> DiseaseStates { get; set; }

      /// <summary>
      /// Gets or sets the state of the disease.
      /// </summary>
      /// <value>The state of the disease.</value>
      public string DiseaseState
      {
         get
         {
            if (DiseaseStates == null)
            {
               return string.Empty;
            }

            return string.Join(",", DiseaseStates);
         }
         set
         {
            DiseaseStates = value.Split(',').ToList<string>();
         }
      }


      /// <summary>
      /// Gets or sets the initial in review date.
      /// </summary>
      /// <value>The initial in review date.</value>
      public DateTimeOffset? InitialInReviewDate { get; set; }

      /// <summary>
      /// Gets or sets the version modified date.
      /// </summary>
      /// <value>The version modified date.</value>
      public DateTimeOffset? VersionModifiedDate { get; set; }

      /// <summary>
      /// Gets or sets the version creation date.
      /// </summary>
      /// <value>The version creation date.</value>
      public DateTimeOffset? VersionCreationDate { get; set; }

      /// <summary>
      /// Gets or sets the initial steady state date.
      /// </summary>
      /// <value>The initial steady state date.</value>
      public DateTimeOffset? InitialSteadyStateDate { get; set; }

      /// <summary>
      /// Gets or sets the CMRB meeting date.
      /// </summary>
      /// <value>The CMRB meeting date.</value>
      public DateTimeOffset? CMRBMeetingDate { get; set; }

      /// <summary>
      /// Gets or sets the review cycle start date.
      /// </summary>
      /// <value>The review cycle start date.</value>Country
      public DateTimeOffset? ReviewCycleStartDate { get; set; }

      /// <summary>
      /// Gets or sets the approved for distribution date.
      /// </summary>
      /// <value>The approved for distribution date.</value>
      public DateTimeOffset? ApprovedForDistributionDate { get; set; }

      /// <summary>
      /// Gets or sets the expiration date.
      /// </summary>
      /// <value>The expiration date.</value>
      public DateTimeOffset? ExpirationDate { get; set; }

      /// <summary>
      /// Gets or sets the approved for production date4.
      /// </summary>
      /// <value>The approved for production date4.</value>
      public DateTimeOffset? ApprovedForProductionDate { get; set; }

      /// <summary>
      /// Gets or sets the planned date of first use date.
      /// </summary>
      /// <value>The planned date of first use date.</value>
      public DateTimeOffset? PlannedDateOfFirstUseDate { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [leave behind].
      /// </summary>
      /// <value><c>true</c> if [leave behind]; otherwise, <c>false</c>.</value>
      public string LeaveBehind { get; set; }

      /// <summary>
      /// Gets or sets the medicine.
      /// </summary>
      /// <value>The medicine.</value>
      public string Medicine { get; set; }

      /// <summary>
      /// Gets or sets the country.
      /// </summary>
      /// <value>The country.</value>
      public string Country { get; set; }

      /// <summary>
      /// Gets or sets the field group.
      /// </summary>
      /// <value>The field group.</value>
      public string FieldGroup
      {
         get
         {
            if (FieldGroups == null)
            {
               return string.Empty;
            }

            return string.Join(",", FieldGroups);
         }
         set
         {
            FieldGroups = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the field groups.
      /// </summary>
      /// <value>The field groups.</value>
      public List<string> FieldGroups { get; set; }


      /// <summary>
      /// Gets or sets the product managers.
      /// </summary>
      /// <value>The product managers.</value>
      public List<string> ProductManagers { get; set; }

      /// <summary>
      /// Gets or sets the product manager.
      /// </summary>
      /// <value>The product manager.</value>
      public string ProductManager
      {
         get
         {
            if (ProductManagers == null)
            {
               return string.Empty;
            }

            return string.Join(",", ProductManagers);
         }
         set
         {
            ProductManagers = value.Split(',').ToList<string>();
         }
      }

      /// <summary>
      /// Gets or sets the category.
      /// </summary>
      /// <value>The category.</value>
      public string Category { get; set; }

      /// <summary>
      /// Gets or sets the fair market value.
      /// </summary>
      /// <value>The fair market value.</value>
      public decimal FairMarketValue { get; set; }

      /// <summary>
      /// Gets or sets the low stock email.
      /// </summary>
      /// <value>The low stock email.</value>
      public string LowStockEmail { get; set; }

      /// <summary>
      /// Gets or sets the review email.
      /// </summary>
      /// <value>The review email.</value>
      public string ReviewEmail { get; set; }

      /// <summary>
      /// Gets or sets the review email.
      /// </summary>
      /// <value>The review email.</value>
      public string Objective { get; set; }

      /// <summary>
      /// Gets or sets the appropriate use.
      /// </summary>
      /// <value>The appropriate use.</value>
      public string AppropriateUse { get; set; }

      /// <summary>
      /// Gets or sets the active date time.
      /// </summary>
      /// <value>The active date time.</value>
      public DateTimeOffset? ActiveDate { get; set; }

      /// <summary>
      /// Gets or sets the obsolete date.
      /// </summary>
      /// <value>The obsolete date.</value>
      public DateTimeOffset? ObsoleteDate { get; set; }

      /// <summary>
      /// Gets or sets the review date.
      /// </summary>
      /// <value>The review date.</value>
      public DateTimeOffset? ReviewDate { get; set; }

      /// <summary>
      /// Gets or sets the name of the veeva document.
      /// </summary>
      /// <value>The name of the veeva document.</value>
      public string VeevaDocumentName { get; set; }

      /// <summary>
      /// Converts to reachoutproduct.
      /// </summary>
      /// <returns>ReachOutProduct.</returns>
      public ReachOutProduct ToReachOutProduct()
      {
         List<string> inactiveStatuses = new List<string>() { "inactive", "expired", "superseded" };

         ReachOutProduct product = new ReachOutProduct()
         {
            Expired = this.Status.ToLower() == "expired" || this.ExpirationDate != null && this.ExpirationDate < DateTime.UtcNow,
            Sku = this.DocumentNumber,
            Name = this.DocumentName,
            Description = this.Description,
            ExternalStatus = this.Status,
            Products = this.Products,
            SubProducts = this.SubProducts,
            IntendedAudiences = this.IntendedAudiences,
            VersionMajor = this.MajorVersionNumber,
            FDMDescription = this.FDMDescription,
            DocumentID = this.ID.ToString(),
            DocumentType = this.DocumentType,
            DocumentSubType = this.DocumentSubType,
            Branding = string.Join(",", this.Brandings),
            PrescribingInfoRequired = this.PrescribingInfoRequired.HasValue ? this.PrescribingInfoRequired.Value : false,
            Audience = this.IntendedAudience,
            InternalUsers = string.Join(",", this.InternalUsers),
            MethodOfDelivery = string.Join(",", this.MethodsOfDelivery),
            RequiresFDM = this.RequresFDM.HasValue ? this.RequresFDM.Value : false,
            VeevaDocumentName = this.VeevaDocumentName
         };

         product.Active = !inactiveStatuses.Contains(this.Status.ToLower()) && !product.Expired;

         if (this.ExpirationDate.HasValue)
         {
            product.ExternalExpirationDate = this.ExpirationDate.Value.DateTime;
            product.ExpirationDate = this.ExpirationDate.Value.DateTime;
         }
         else if (!product.Active)
         {
            product.ExpirationDate = DateTime.Now;
         }

         return product;
      }
   }
}
