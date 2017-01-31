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

        [HttpGet("Getusers")]
        public IActionResult GetUsers()
        {
            var userEntities = _foxStocksRepository.GetUsers();
            var results = Mapper.Map<IEnumerable<UserDTO>>(userEntities);
            return Ok(results);
        }

        [HttpGet("Portfolio/{id}")]
        public IActionResult DisplayPortfolio(int id)
        {
            var portfolioEntities = _foxStocksRepository.GetPortfolio(id);
            var results = Mapper.Map<IEnumerable<TransactionDTO>>(portfolioEntities);
            return Ok(results);
        }
        [HttpGet("Portfolio/{id}/{transId}", Name = "DisplayTransaction")]
        public IActionResult DisplayTransaction(int id, int transId)
        {
            var portfolioEntities = _foxStocksRepository.GetTransaction(id, transId);
            var results = Mapper.Map<IEnumerable<TransactionDTO>>(portfolioEntities);
            return Ok(results);
        }
        [HttpPost("Trade/{id}")]
        public IActionResult CreateTransaction(int id, [FromBody] CreateTransactionDTO transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_foxStocksRepository.UserExists(id))
            {
                return NotFound();
            }
            var newTransaction = Mapper.Map<Entities.Transactions>(transaction);
            _foxStocksRepository.AddTransaction(id, newTransaction);
            if (!_foxStocksRepository.Save())
            {
                return StatusCode(500, "Something went wrong.");
            }
            var createdTransaction = Mapper.Map<Models.TransactionDTO>(newTransaction);

            return CreatedAtRoute("DisplayTransaction", new
            { id = id, transId = createdTransaction.Id }, createdTransaction);
        }
        [HttpPatch("Update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody]JsonPatchDocument<UpdateUserDTO> UpdateUser)
        {
            if (UpdateUser == null)
            {
                return BadRequest();
            }
            
            if (!_foxStocksRepository.UserExists(id))
            {
                return NotFound();
            }
            var userEntity = _foxStocksRepository.GetUser(id);
            if(userEntity == null)
            {
                return NotFound();
            }

            var userToUpdate = Mapper.Map<UpdateUserDTO>(userEntity);

            UpdateUser.ApplyTo(userToUpdate, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(userToUpdate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(userToUpdate, userEntity);

            if (!_foxStocksRepository.Save())
            {
                return StatusCode(500, "Something went wrong.");
            }
            return NoContent();

        }
        [HttpDelete("Portfolio/{id}/{transId}")]
        public IActionResult DeleteTransaction(int id, int transId)
        {
            if (!_foxStocksRepository.UserExists(id))
            {
                return NotFound();
            }
            var transactionToDelete = _foxStocksRepository.GetTransaction(id, transId);
            if(transactionToDelete == null)
            {
                return NotFound();
            }
            _foxStocksRepository.DeleteTransaction(transactionToDelete);

            if (!_foxStocksRepository.Save())
            {
                return StatusCode(500, "Something went wrong");
            }
            return NoContent();

        }
    }
}
