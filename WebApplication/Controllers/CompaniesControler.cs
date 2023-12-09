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
    [Route("api")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Companies
        [Route("companies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
             return Ok(await _companyService.GetAllCompanies()) ;
        }

        // GET: api/Companies/5
        [HttpGet]
        [Route("companies/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Company>> GetCompany([FromQuery] int id)
        {
            var company=await _companyService.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }
            
            return Ok(company);
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("companies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompany([FromBody]CompanyDTO company)
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
        [Route("companies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyInsertDTO company)
        {
            try
            {
                var companyDb = await _companyService.CreateCompany(company);
                return Ok(companyDb);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }


        // DELETE: api/Companies/5
        [HttpDelete]
        [Route("companies/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _companyService.DeleteCompany(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}

