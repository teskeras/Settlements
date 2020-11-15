using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Settlements.Data
{
    public class SettlementDTOPut
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string SettlementName { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public int? CountryId { get; set; }
    }
}
