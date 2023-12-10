using Core.DbEntities;
using Core.WebEntities.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetAllContacts();

        Task<IEnumerable<ContactsWithCompanyAndCountryDTO>> GetContactsWithCompanyAndCountry();

        Task<IEnumerable<ContactDTO>> FilterContact(int? countryId, int? companyId);

        Task<ContactDTO> CreateContact(ContactInsertDTO contact);

        Task UpdateContact(ContactDTO contact);

        Task DeleteContact(int id);
    }
}
