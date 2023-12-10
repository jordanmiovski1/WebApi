using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DbEntities;
using Core.Interfaces;
using Core.WebEntities;
using Core.WebEntities.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebApiApplication2.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService CompanyService)
        {
            _companyService = CompanyService;
        }

        // GET: api/Companies

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]

        public async Task<IActionResult> GetCompanies()
        {
            return Ok(await _companyService.GetAllCompanies());
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDTO company)
        {
            try
            {
                await _companyService.UpdateCompany(company);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyInsertDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateCompany([FromBody] CompanyInsertDTO company)
        {
            try
            {
                var companyDb = await _companyService.CreateCompany(company);
                return Ok(companyDb);
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting Company!");
            }
        }

        // DELETE: api/Companies/5
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _companyService.DeleteContact(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Error removing Company!");
            }

        }

    }
}

