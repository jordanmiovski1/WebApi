using Core.DbEntities;
using Core.Interfaces;
using Core.WebEntities.Company;
using Core.WebEntities.Contact;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private readonly WebAppContext _context;

        public ContactService(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactDTO>> GetAllContacts()
        {
            return MapToModify(await _context.Contact.ToListAsync());
        }

        public async Task<IEnumerable<ContactsWithCompanyAndCountryDTO>> GetContactsWithCompanyAndCountry()
        {
            //test
            var contacts = await _context.Contact
                                .Include(c => c.Company)
                                .Include(c => c.Country)
                                .Select(c => new
                                ContactsWithCompanyAndCountryDTO
                                {
                                    ContactName = c.ContactName,
                                 // CompanyId = c.CompanyId,
                                //  CountryId = c.CountryId,
                                    CompanyName = c.Company.CompanyName,
                                    CountryName = c.Country.CountryName
                                })
                                .ToListAsync();
            return contacts;
        }

        public async Task<IEnumerable<ContactDTO>> FilterContact(int? countryId, int? companyId)
        {
            IQueryable<Contact> query = _context.Contact;

            if (countryId.HasValue)
            {
                query = query.Where(c => c.CountryId == countryId.Value);
            }

            if (companyId.HasValue)
            {
                query = query.Where(c => c.CompanyId == companyId.Value);
            }

            var filteredContacts = await query.ToListAsync();
            return MapToModify(filteredContacts);
        }

        public async Task<ContactDTO> CreateContact(ContactInsertDTO contact)
        {
            var contactToInsert = new Contact()
            {
                ContactName = contact.ContactName,
                CompanyId = contact.CompanyId,
                CountryId = contact.CountryId
            };
            _context.Contact.Add(contactToInsert);
            await _context.SaveChangesAsync();
            return MapToModify(contactToInsert);
        }

        public async Task UpdateContact(ContactDTO contact)
        {
            try
            {
                var contactDb = await _context.Contact.FindAsync(contact.Id);
                if (contactDb == null)
                    throw new Exception("Record not found!");

                contactDb.ContactName = contact.ContactName;
                contactDb.CompanyId = contact.CompanyId;
                contactDb.CountryId = contact.CountryId;

                _context.Entry(contactDb).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteContact(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                throw new Exception("Record not found!");
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }

        #region Helpers

        private IEnumerable<ContactDTO> MapToModify(List<Contact> contacts)
        {
            return contacts.Select(x => new ContactDTO()
            {
                Id = x.ContactId,
                ContactName = x.ContactName,
                CompanyId = x.CompanyId,
                CountryId = x.CountryId,
            });
        }


        private ContactDTO MapToModify(Contact contactToInsert)
        {
            return new ContactDTO()
            {
                Id = contactToInsert.ContactId,
                ContactName = contactToInsert.ContactName,
                CountryId = contactToInsert.CountryId,
                CompanyId = contactToInsert.CompanyId
            };
        }

        #endregion
    }
}
