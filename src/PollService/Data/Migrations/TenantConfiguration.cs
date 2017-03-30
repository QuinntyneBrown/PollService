using System.Data.Entity.Migrations;
using PollService.Data;
using PollService.Data.Model;

namespace PollService.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(PollServiceContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}
