using System;

namespace ReachOutAuth.Models.Roster
{
    public class RosterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string WorkType { get; set; }
        public string TeamName { get; set; }
        public string Manager { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public string TerritoryName { get; set; }
        public string Email { get; set; }
        public string CostCenter { get; set; }
        public string FunctionalArea { get; set; }
        public string TerritoryId { get; set; }
        public string Audience { get; set; }
        public string Status { get; set; }
        public string AssayShippingStreetAddress1 { get; set; }
        public string AssayShippingStreetAddress2 { get; set; }
        public string AssayShippingCity { get; set; }
        public string AssayShippingState { get; set; }
        public string AssayShippingPostalCode { get; set; }
    }
}