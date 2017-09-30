using CyberSoftDataCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Models
{
    public class UsersInfos
    {
        CdataCenterDbContext _context = null;
        public UsersInfos(CdataCenterDbContext context)
        {
            _context = context;
        }
        private CdataCenterDbContext db = null;
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string FristName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Lastconected { get; set; }

        public bool IsActive { get; set; }

        public bool IsOnLine { get; set; }

        private string role;

        public string Role
        {
            get { return RoleName(Id); }
            set { role = value; }
        }
        private string roleDescription;

        public string RoleDescription
        {
            get { return RoleDescriptions(Id); }
            set { roleDescription = value; }
        }




        public string RoleName(int id)
        {
            string RoleNName = "NAP";
            UsersRoles UR = _context.UsersRoles.FirstOrDefault(e => e.UsersID == id);
            Roles R = new Models.Roles();
            if (UR != null)
            {
                R = _context.Roles.FirstOrDefault(e => e.RolesID == UR.RolesID);
                RoleNName = R.RoleName;
            }



            return RoleNName;
        }


        public string RoleDescriptions(int id)
        {
            string RoleNName = "NAP";
            UsersRoles UR = _context.UsersRoles.FirstOrDefault(e => e.UsersID == id);
            Roles R = new Models.Roles();
            if (UR != null)
            {
                R = _context.Roles.FirstOrDefault(e => e.RolesID == UR.RolesID);
                RoleNName = R.RoleName;
            }



            return RoleNName;
        }
    }
}
