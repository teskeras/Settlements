using Settlements.Data.Repository;
using System;
using System.Collections.Generic;

#nullable disable

namespace Settlements.Data
{
    public partial class Settlement : IEntity
    {
        public int Id { get; set; }
        public string SettlementName { get; set; }
        public string PostalCode { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
