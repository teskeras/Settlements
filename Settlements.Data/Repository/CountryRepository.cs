using System;
using System.Collections.Generic;
using System.Text;

namespace Settlements.Data.Repository
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(SettlementsContext dbContext) : base(dbContext)
        {
        }
    }
}
