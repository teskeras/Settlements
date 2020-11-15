using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Settlements.Data.Repository
{
    public interface ISettlementRepository : IBaseRepository<Settlement>
    {
        PagedList<SettlementDTOGet> GetSettlements(SettlementParameters settlementParameters);
    }
}
