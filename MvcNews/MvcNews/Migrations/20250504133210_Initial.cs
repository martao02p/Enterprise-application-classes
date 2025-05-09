using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcNews.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RowVersion",
                table: "News",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldRowVersion: true,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "News",
                type: "BLOB",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }
    }
}
