using Microsoft.EntityFrameworkCore.Migrations;

namespace csc_assignment_2.Migrations
{
    public partial class addplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Plan",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plan",
                table: "AspNetUsers");
        }
    }
}
