using Core.DbEntities;
using Core.Interfaces;
using Core.WebEntities.Company;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CompanyService: ICompanyService
    {
        private readonly WebAppContext _context;

        public CompanyService(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompanies()
        {
            return MapToModify(await _context.Company.ToListAsync());
        }

        public async Task<CompanyDTO> GetCompany(int id)
        {
            return MapToModify(await _context.Company.FindAsync(id));
        }

        public async Task<CompanyDTO> CreateCompany(CompanyInsertDTO companyDTO)
        {
            var company = new Company() { CompanyName = companyDTO.Name };
            _context.Company.Add(company);
            await _context.SaveChangesAsync();
            var companyResult=MapToModify(company);
            return companyResult;
        }

        public async Task UpdateCompany(CompanyDTO company)
        {
            try
            {
                var companyDb = await _context.Company.FindAsync(company.Id);
                if (companyDb == null)
                    throw new Exception("Record not found!");

                companyDb.CompanyName = company.Name;
                _context.Entry(companyDb).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCompany(int id)
        {
            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                throw new Exception("Company not found!");
            }

            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
        }

        #region Helpers
        private IEnumerable<CompanyDTO> MapToModify(List<Company> companies)
        {
            return companies.Select(x => new CompanyDTO()
            {
                Id = x.CompanyId,
                Name = x.CompanyName
            });

        }


        private CompanyDTO MapToModify(Company? company)
        {
            if (company == null) 
                throw new Exception("Record not found!");

            return new CompanyDTO() { Id = company.CompanyId, Name = company.CompanyName };

        }


        #endregion
    }
}
