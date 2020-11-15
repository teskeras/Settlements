using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Settlements.Data.Repository
{
    public class SettlementRepository : BaseRepository<Settlement>, ISettlementRepository
    {
        public SettlementRepository (SettlementsContext dbContext) : base(dbContext)
        {

        }

        public PagedList<SettlementDTOGet> GetSettlements(SettlementParameters settlementParameters)
        {
            return PagedList<SettlementDTOGet>.ToPagedList(Query().OrderBy(s => s.SettlementName), settlementParameters.PageNumber, settlementParameters.PageSize);
        }
    }
}
