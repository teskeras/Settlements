using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Settlements.Data
{
    public class SettlementDTOGet
    {
        public int Id { get; set; }
        public string SettlementName { get; set; }
        public string PostalCode { get; set; }
        public CountryDTO Country { get; set; }
    }
}
