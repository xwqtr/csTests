using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyService.DB.Migrations
{
    public partial class BgServieConfiguraton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "BgServiceConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SecondsInterval = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BgServiceConfigurations", x => x.Id);
                });

            

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "BgServiceConfigurations");

        }
    }
}
