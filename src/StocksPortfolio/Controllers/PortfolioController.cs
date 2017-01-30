using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StocksPortfolio.Models;
using StocksPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        private IFoxStocksRepository _foxStocksRepository;

        public PortfolioController(IFoxStocksRepository foxStocksRepository)
        {
            _foxStocksRepository = foxStocksRepository;
        }

        [HttpGet("getusers")]
        public IActionResult GetUsers()
        {
            var userEntities = _foxStocksRepository.GetUsers();
            var results = Mapper.Map<IEnumerable<Users>>(userEntities);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult DisplayPortfolio(int id)
        {
            var portfolioEntities = _foxStocksRepository.GetPortfolio(id);
            var results = Mapper.Map<IEnumerable<Transactions>>(portfolioEntities);
            return Ok(results);
        }
        [HttpPost("Buy")]
        public IActionResult CreateTransaction([FromBody] CreateTransaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

        }
    }
}
