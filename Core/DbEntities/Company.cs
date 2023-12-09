using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DbEntities
{
    public class Company
    {
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }

        private ICollection<Contact> Contacts { get; set; }
    }
}
