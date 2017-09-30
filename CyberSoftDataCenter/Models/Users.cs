using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class Users
    {
        public int UsersID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string FuleName { get; set; }
        public string Tel { get; set; }
        public bool Isconnected { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastContedDate { get; set; }
        public bool IsActif { get; set; }

    }
}
