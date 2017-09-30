using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models.AccountViewModels
{
    public class UserListModelView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string FuleName { get; set; }
        public string RoleName { get; set; }
    }
}
