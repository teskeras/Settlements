using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Settlements.Core;
using Settlements.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Settlements.Controllers
{
    [Route("api/[controller]")]
    public class SettlementsController : Controller
    {
        private readonly ISettlementsService settlementsService;
        
        public SettlementsController(ISettlementsService settlementsService)
        {
            this.settlementsService = settlementsService;
        }

        [HttpGet("[action]")]
        public IActionResult GetSettlements([FromQuery] SettlementParameters settlementParameters)
        {
            var settlements = settlementsService.GetSettlements(settlementParameters);
            var metadata = new
            {
                settlements.TotalCount,
                settlements.PageSize,
                settlements.CurrentPage,
                settlements.TotalPages,
                settlements.HasNext,
                settlements.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(settlements);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetSettlement(int id)
        {
            var settlement = settlementsService.GetSettlement(id);
            return Ok(settlement);
        }

        [HttpPost("[action]")]
        public IActionResult CreateSettlement([FromBody] SettlementDTOPost settlementDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var settlement = settlementsService.CreateSettlement(settlementDto);
            return Created($"api/Settlements/GetSettlement/{settlement.Id}", settlement.Id);
        }

        [HttpPut("[action]/{id}")]
        public IActionResult EditSettlement([FromBody] SettlementDTOPut settlementDto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var settlement = settlementsService.EditSettlement(settlementDto, id);
            if (settlement == null)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteSettlement(int id)
        {
            settlementsService.DeleteSettlement(id);
            return NoContent();
        }

        [HttpGet("[action]/{search}")]
        public IActionResult GetCountries(string search)
        {
            if (string.IsNullOrWhiteSpace(search) || string.IsNullOrEmpty(search)) return BadRequest("Required search string");
            var list = settlementsService.GetCountries(search);
            return Ok(list);
        }
    }
}
