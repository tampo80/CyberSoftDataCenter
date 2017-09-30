using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Utils
{
    public interface IEncryptionService
    {
        string CreateSalt();
        string EncryptPassword(string password, string salt);
    }
}
