using Core.Interfaces;
using Core.WebEntities.Contact;
using Core.WebEntities.Country;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // GET: api/Companies

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CountryDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]

        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _countryService.GetAllCountries());
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateCountry([FromBody] CountryDTO country)
        {
            try
            {
                await _countryService.UpdateCountry(country);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryInsertDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateCountry([FromBody] CountryInsertDTO country)
        {
            try
            {
                var countryDb = await _countryService.CreateCountry(country);
                return Ok(countryDb);
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating Country");
            }
        }

        // DELETE: api/Companies/5
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                await _countryService.DeleteCountry(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Error removing Country");
            }

        }

    }
}
