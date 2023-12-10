using Core.DbEntities;
using Core.WebEntities.Company;
using Core.WebEntities.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompanies();

        Task<CompanyDTO> CreateCompany(CompanyInsertDTO company);

        Task UpdateCompany(CompanyDTO company);

        Task DeleteContact(int id);
    }
}
