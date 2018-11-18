namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipType1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.MembershipTypes SET NAME = 'Pay as you go' WHERE DurationInMonth = 0");
            Sql("UPDATE dbo.MembershipTypes SET NAME = 'Monthly' WHERE DurationInMonth = 1");
            Sql("UPDATE dbo.MembershipTypes SET NAME = 'Quarterly' WHERE DurationInMonth = 3");
            Sql("UPDATE dbo.MembershipTypes SET NAME = 'Yearly' WHERE DurationInMonth = 12");
        }
        
        public override void Down()
        {
        }
    }
}
