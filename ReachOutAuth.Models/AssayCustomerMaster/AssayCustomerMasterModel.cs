namespace ReachOutAuth.Models.AssayCustomerMaster
{
    public class AssayCustomerMasterModel
    {
        public int AssayCustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NPI { get; set; }
        public int SareptaId { get; set; }
        public string PrimaryAffiliatedSoC { get; set; }
        public string AssayAddressLine1 { get; set; }
        public string AssayAddressLine2 { get; set; }
        public string AssayCity { get; set; }
        public string AssayState { get; set; }
        public string AssayZipCode { get; set; }
        public string TerritoryId { get; set; }
    }
}
