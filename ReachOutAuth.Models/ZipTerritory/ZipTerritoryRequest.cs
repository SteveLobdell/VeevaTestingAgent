using System;
using System.Collections.Generic;
using System.Text;

namespace ReachOutAuth.Models.ZipTerritory
{
    public class ZipTerritoryRequest
    {
        public string TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public string ZipCode { get; set; }
    }
}