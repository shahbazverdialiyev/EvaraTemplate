using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvaraTemplate.Migrations
{
    public partial class Add_ImageName_to_Slider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Sliders");
        }
    }
}
