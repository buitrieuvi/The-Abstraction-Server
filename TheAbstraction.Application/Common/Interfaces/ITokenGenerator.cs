using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        //public string GenerateToken(string userName, string password);
        string GenerateJwtToken((string userId, string userName, IList<string> roles) userDetails);
    }
}
