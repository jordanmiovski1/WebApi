using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebEntities.Contact
{
    public class ContactsWithCompanyAndCountryDTO
    {
        public string ContactName { get; set; }

        //   public int CountryId { get; set; }

        public string CountryName { get; set; }

        //    public int CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}
