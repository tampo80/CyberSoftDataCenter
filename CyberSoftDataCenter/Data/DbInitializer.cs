using CyberSoftDataCenter.Models;
using CyberSoftDataCenter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberSoftDataCenter.Data
{
    public class DbInitializer
    {
        public static void Initialize(CdataCenterDbContext context)
        {

            context.Database.EnsureCreated();

            EncryptionService EncryptService = new EncryptionService();
            string salt = EncryptService.CreateSalt();
            Users users = new Users();
            users.PasswordSalt = salt;
            users.UserName = "gaston@live.fr";
            //users.adresse = "";
            users.IsActif = true;
            users.FuleName = "Administrateur Cyber Soft";
            users.CreationDate = DateTime.UtcNow;
            users.Password = "azerty123";
            users.Tel = "002289194132";
            string encrypedpassword = EncryptService.EncryptPassword(users.Password, salt);

            users.Password = encrypedpassword;
            Roles roles = new Roles
            {
                Description = "Adminstrateur de l'application",
                RoleName = "Admin"

            };

            context.Users.Add(users);
            context.Roles.Add(roles);
            context.UsersRoles.Add(new UsersRoles
            {

                Users = users,
                Roles = roles
            });

            context.SaveChanges();
        }
    }
}
