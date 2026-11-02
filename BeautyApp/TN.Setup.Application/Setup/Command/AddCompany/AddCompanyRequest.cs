using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TN.Authentication.Domain.Entities.Company;

namespace TN.Setup.Application.Setup.Command.AddCompany
{
    public record AddCompanyRequest
    (
            string name,
            string address,
            string email,
            string contactNumber,
            string pan,
            string imageUrl,
            bool isEnabled,
            bool isDeleted,
            string organizationId
    );
}
