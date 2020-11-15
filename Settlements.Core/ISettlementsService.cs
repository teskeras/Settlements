using Settlements.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Settlements.Core
{
    public interface ISettlementsService
    {
        PagedList<SettlementDTOGet> GetSettlements(SettlementParameters settlementParameters);
        SettlementDTOGet GetSettlement(int id);
        SettlementDTOPost CreateSettlement(SettlementDTOPost settlementDto);
        SettlementDTOPut EditSettlement(SettlementDTOPut settlementDto, int id);
        void DeleteSettlement(int id);
        IEnumerable<CountryDTO> GetCountries(string search);
    }
}
