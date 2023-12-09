using Core.DbEntities;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private readonly WebAppContext _context;

        public ContactService(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task<IEnumerable<object>> GetContactsWithCompanyAndCountry()
        {
            var contacts = await _context.Contact
                                .Include(c => c.Company)
                                .Include(c => c.Country)
                                .Select(c => new
                                {
                                    c.ContactName,
                                    CompanyName = c.Company.CompanyName,
                                    CountryName = c.Country.CountryName
                                })
                                .ToListAsync();
            return contacts;
        }

        public async Task<IEnumerable<Contact>> FilterContact(int? countryId, int? companyId)
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
            return filteredContacts;
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task UpdateContact(int id, Contact contact)
        {
            if (id != contact.ContactId)
            {
                throw new Exception("Record not found!");
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
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
    }
}
