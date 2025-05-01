using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class UserIDdatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
name: "PK_t_BackOfficeUsers",
table: "t_BackOfficeUsers"
);
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "t_BackOfficeUsers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");


            // Add the primary key on the UserID column
            migrationBuilder.AddPrimaryKey(
                name: "PK_t_BackOfficeUsers",
                table: "t_BackOfficeUsers",
                column: "UserID"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "t_BackOfficeUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
