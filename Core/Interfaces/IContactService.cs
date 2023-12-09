using Core.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<IEnumerable<object>> GetContactsWithCompanyAndCountry();
        Task<IEnumerable<Contact>> FilterContact(int? countryId, int? companyId);
        Task<Contact> CreateContact(Contact contact);
        Task UpdateContact(int id, Contact contact);
        Task DeleteContact(int id);

    }
}
