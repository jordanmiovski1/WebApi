using Application.Services;
using Core.DbEntities;
using Core.Interfaces;
using Core.WebEntities.Contact;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication.Controllers
{

    [Route("api/contacts")]
    [ApiController]

    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/Contacts

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ContactDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]

        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _contactService.GetAllContacts());
        }

        [Route("companyandcountries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ContactsWithCompanyAndCountryDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]

        public async Task<IActionResult> GetContactsWithCompanyAndCountry()
        {
            return Ok(await _contactService.GetContactsWithCompanyAndCountry());
        }

        [Route("filter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ContactDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]

        public async Task<IActionResult> FilterContacts(int? countryId, int? companyId)
        {
            return Ok(await _contactService.FilterContact(countryId, companyId));
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateContact([FromBody] ContactDTO contact)
        {
            try
            {
                await _contactService.UpdateContact(contact);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactInsertDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateContact([FromBody] ContactInsertDTO contact)
        {
            try
            {
                var contactDb = await _contactService.CreateContact(contact);
                return Ok(contactDb);
            }
            catch (Exception ex)
            {
                return BadRequest("Error inserting Contact!");
            }
        }

        // DELETE: api/Companies/5
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await _contactService.DeleteContact(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest("Error removing Contact!");
            }

        }

    }

}
