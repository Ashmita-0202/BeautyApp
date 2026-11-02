using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TN.Authentication.Domain.Entities.Company;

namespace TN.Setup.Application.Setup.Queries.Company
{
   public record GetAllCompanyQueryResponse
    (
    string id = "",
            string name = "",
            string address = "",
            string email = "",
            string contactNumber = "",
            string pan = "",
            string imageUrl = "",
            bool isEnabled = true,
            DateTime createdDate = default,
            string createdBy = "",
            DateTime modifiedDate = default,
            string modifiedBy = "",
            bool isDeleted = true

   );
}
