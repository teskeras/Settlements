using Settlements.Data.Repository;
using System;
using System.Collections.Generic;

#nullable disable

namespace Settlements.Data
{
    public partial class Country : IEntity
    {
        public Country()
        {
            Settlements = new HashSet<Settlement>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Settlement> Settlements { get; set; }
    }
}
