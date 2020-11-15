using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Settlements.Data;
using Settlements.Data.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Settlements.Core
{
    [Produces("application/json")]
    [Route("api/settlements")]
    public class SettlementsService : ISettlementsService
    {
        private readonly ISettlementRepository settlementRepository;
        private readonly ICountryRepository countryRepository;

        public SettlementsService(ISettlementRepository settlementRepository, ICountryRepository countryRepository)
        {
            this.settlementRepository = settlementRepository;
            this.countryRepository = countryRepository;
        }

        public PagedList<SettlementDTOGet> GetSettlements(SettlementParameters settlementParameters)
        {
            return settlementRepository.GetSettlements(settlementParameters);
        }

        public SettlementDTOGet GetSettlement(int id)
        {
            var settlement = settlementRepository.Query().Include(s => s.Country).FirstOrDefault(s => s.Id == id);
            return Mapper.Map<SettlementDTOGet>(settlement);
        }

        public SettlementDTOPost CreateSettlement(SettlementDTOPost settlementDto)
        {
            var settlement = Mapper.Map<Settlement>(settlementDto);
            settlementRepository.Insert(settlement);
            settlementRepository.Commit();
            return Mapper.Map<SettlementDTOPost>(settlement);
        }

        public SettlementDTOPut EditSettlement(SettlementDTOPut settlementDto, int id)
        {
            var edited = Mapper.Map<Settlement>(settlementDto);
            var original = settlementRepository.Query().AsNoTracking().FirstOrDefault(s => s.Id == id && s.Id == edited.Id);
            if (original == null)
            {
                return null;
            }
            settlementRepository.Update(edited);
            settlementRepository.Commit();
            return Mapper.Map<SettlementDTOPut>(edited);
        }

        public void DeleteSettlement(int id)
        {
            var settlement = settlementRepository.Query().FirstOrDefault(s => s.Id == id);
            if (settlement == null)
            {
                return;
            }
            settlementRepository.Delete(settlement);
            settlementRepository.Commit();
        }

        public IEnumerable<CountryDTO> GetCountries(string search)
        {
            var searching = search.Trim();
            return countryRepository.Query().Where(c => c.CountryName.Contains(searching)).ProjectTo<CountryDTO>().ToList();
        }
    }
}
