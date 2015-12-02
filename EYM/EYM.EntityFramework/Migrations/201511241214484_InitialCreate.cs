namespace EYM.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Comment = c.String(),
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBalance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Credit = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        DateToOrder = c.DateTime(nullable: false),
                        ProductTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTemplate", t => t.ProductTemplateId, cascadeDelete: true)
                .Index(t => t.ProductTemplateId);
            
            CreateTable(
                "dbo.ProductTemplate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Description = c.String(),
                        ImagePath = c.String(),
                        ProviderId = c.Int(nullable: false),
                        ProductTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductType", t => t.ProductTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Provider", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.ProviderId)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Provider",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        ImagePath = c.String(),
                        Description = c.String(),
                        AdditionalInfo = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTemplate", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.ProductTemplate", "ProductTypeId", "dbo.ProductType");
            DropForeignKey("dbo.Product", "ProductTemplateId", "dbo.ProductTemplate");
            DropForeignKey("dbo.OrderLine", "ProductId", "dbo.Product");
            DropForeignKey("dbo.UserBalance", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Order", "UserId", "dbo.User");
            DropForeignKey("dbo.OrderLine", "OrderId", "dbo.Order");
            DropIndex("dbo.ProductTemplate", new[] { "ProductTypeId" });
            DropIndex("dbo.ProductTemplate", new[] { "ProviderId" });
            DropIndex("dbo.Product", new[] { "ProductTemplateId" });
            DropIndex("dbo.UserBalance", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.Order", new[] { "UserId" });
            DropIndex("dbo.OrderLine", new[] { "OrderId" });
            DropIndex("dbo.OrderLine", new[] { "ProductId" });
            DropTable("dbo.Provider");
            DropTable("dbo.ProductType");
            DropTable("dbo.ProductTemplate");
            DropTable("dbo.Product");
            DropTable("dbo.UserBalance");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Order");
            DropTable("dbo.OrderLine");
        }
    }
}
