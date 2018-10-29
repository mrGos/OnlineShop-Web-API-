namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editdtb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityRoles", newName: "ApplicationRoles");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "ApplicationUserRoles");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "ApplicationUserClaims");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "ApplicationUserLogins");
            DropPrimaryKey("dbo.ApplicationUserClaims");
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 250),
                        Website = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        Other = c.String(),
                        Lat = c.Double(),
                        Lng = c.Double(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Email = c.String(maxLength: 250),
                        Message = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Tags", c => c.String());
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Pages", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.PostCategories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Posts", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.ApplicationRoles", "Description", c => c.String(maxLength: 250));
            AddColumn("dbo.ApplicationRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Slides", "Content", c => c.String());
            AlterColumn("dbo.Posts", "ViewCount", c => c.Int());
            AlterColumn("dbo.SupportOnlines", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ApplicationUserClaims", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicationUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ApplicationUserClaims", "UserId");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.ApplicationUsers", "Id");
            DropColumn("dbo.Products", "UpdateTime");
            DropColumn("dbo.ProductCategories", "UpdateTime");
            DropColumn("dbo.Pages", "UpdateTime");
            DropColumn("dbo.PostCategories", "UpdateTime");
            DropColumn("dbo.Posts", "UpdateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.PostCategories", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.Pages", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdateTime", c => c.DateTime());
            AddColumn("dbo.Products", "UpdateTime", c => c.DateTime());
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropPrimaryKey("dbo.ApplicationUserClaims");
            AlterColumn("dbo.ApplicationUserClaims", "UserId", c => c.String());
            AlterColumn("dbo.ApplicationUserClaims", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.SupportOnlines", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Posts", "ViewCount", c => c.Int(nullable: false));
            DropColumn("dbo.Slides", "Content");
            DropColumn("dbo.ApplicationRoles", "Discriminator");
            DropColumn("dbo.ApplicationRoles", "Description");
            DropColumn("dbo.Posts", "UpdatedDate");
            DropColumn("dbo.PostCategories", "UpdatedDate");
            DropColumn("dbo.Pages", "UpdatedDate");
            DropColumn("dbo.ProductCategories", "UpdatedDate");
            DropColumn("dbo.Products", "UpdatedDate");
            DropColumn("dbo.Products", "OriginalPrice");
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Products", "Tags");
            DropColumn("dbo.Orders", "CustomerId");
            DropColumn("dbo.OrderDetails", "Price");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationGroups");
            AddPrimaryKey("dbo.ApplicationUserClaims", "Id");
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "IdentityUserClaims");
            RenameTable(name: "dbo.ApplicationUserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.ApplicationRoles", newName: "IdentityRoles");
        }
    }
}
