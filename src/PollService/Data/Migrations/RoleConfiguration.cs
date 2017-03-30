using System.Data.Entity.Migrations;
using PollService.Data;
using PollService.Data.Model;
using PollService.Features.Users;

namespace PollService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(PollServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.PRODUCT
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
