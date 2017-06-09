using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Fix_Test.MzCentralDbMigrations
{
    public partial class LdapAndOAuth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OAuthAccounts",
                c => new
                {
                    OAuthAccountId = c.Int(nullable: false, identity: true),
                    ProviderId = c.Int(nullable: false),
                    AccessToken = c.String(nullable: false, maxLength: 255, unicode: false),
                })
                .PrimaryKey(t => t.OAuthAccountId)
                .Index(t => t.ProviderId);


        }

        public override void Down()
        {
            //DropForeignKey("dbo.LdapDomains", "TenantKey", "dbo.GlobalTenants");
            DropTable("dbo.OAuthAccounts");
        }
    }
}
