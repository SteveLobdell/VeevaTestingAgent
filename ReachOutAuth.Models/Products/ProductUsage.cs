using System;
using System.Collections.Generic;
using System.Text;

namespace ReachOutAuth.Models.Products
{
    /// <summary>
    /// Class ProductUsage.
    /// </summary>
    public class ProductUsage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUsage"/> class.
        /// </summary>
        public ProductUsage(int numOfMonths) 
        {
            this.InitializeData(numOfMonths);
        }

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
        /// Gets or sets the version identifier.
        /// </summary>
        /// <value>The version identifier.</value>
        public string VersionID { get; set; }

        /// <summary>
        /// Gets or sets the in stock.
        /// </summary>
        /// <value>The in stock.</value>
        public int InStock { get; set; }

        /// <summary>
        /// Gets or sets the on hold.
        /// </summary>
        /// <value>The on hold.</value>
        public int OnHold { get; set; }

        /// <summary>
        /// Gets or sets the allocated.
        /// </summary>
        /// <value>The allocated.</value>
        public int Allocated { get; set; }

        /// <summary>
        /// Gets or sets the available to promise.
        /// </summary>
        /// <value>The available to promise.</value>
        public int AvailableToPromise { get; set; }

        /// <summary>
        /// Gets or sets the entity attribute values.
        /// </summary>
        /// <value>The entity attribute values.</value>
        public List<KeyValuePair<string, string>> EntityAttributeValues { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public List<ProductUsageData> Data { get; set; }

        /// <summary>
        /// Gets or sets the date of first use.
        /// </summary>
        /// <value>The date of first use.</value>
        public string DateOfFirstUse { get; set; }

        /// <summary>
        /// Gets or sets the objective.
        /// </summary>
        /// <value>The objective.</value>
        public string Objective { get; set; }

        /// <summary>
        /// Initializes the specified number months.
        /// </summary>
        /// <param name="numMonths">The number months.</param>
        private void InitializeData(int numMonths)
        {
            this.Data = new List<ProductUsageData>();
            DateTime month = DateTime.Now;

            for(int i = 0; i < numMonths; i++) 
            {
                this.Data.Add(new ProductUsageData
                {
                    Month = month.Month,
                    Year = month.Year,
                    Quanity = 0
                }) ;

                month = month.AddMonths(-1);
            }
        }
    }
}
