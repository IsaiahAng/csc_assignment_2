using Microsoft.EntityFrameworkCore.Migrations;

namespace csc_assignment_2.Migrations
{
    public partial class changeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Plan",
                table: "AspNetUsers",
                newName: "SubPlan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubPlan",
                table: "AspNetUsers",
                newName: "Plan");
        }
    }
}
