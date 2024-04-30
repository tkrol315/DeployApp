using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeployApp.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class StatusFromStringToEnumTextFieldsToVchars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name_010",
                table: "tag_010",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description_010",
                table: "tag_010",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description_002",
                table: "project_version_002",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "yt_code_001",
                table: "project_001",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "title_001",
                table: "project_001",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "repository_url_001",
                table: "project_001",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description_001",
                table: "project_001",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description_003",
                table: "instance_type_003",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name_004",
                table: "instance_004",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name_011",
                table: "group_011",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description_011",
                table: "group_011",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.Sql("ALTER TABLE deploy_instance_101 ALTER COLUMN status_101 TYPE integer USING (status_101::integer);");
            migrationBuilder.Sql("UPDATE deploy_instance_101 SET status_101 = 0 WHERE status_101 IS NULL;");

            migrationBuilder.AlterColumn<int>(
                name: "status_101",
                table: "deploy_instance_101",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name_010",
                table: "tag_010",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "description_010",
                table: "tag_010",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description_002",
                table: "project_version_002",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "yt_code_001",
                table: "project_001",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "title_001",
                table: "project_001",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "repository_url_001",
                table: "project_001",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "description_001",
                table: "project_001",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "description_003",
                table: "instance_type_003",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "name_004",
                table: "instance_004",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name_011",
                table: "group_011",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "description_011",
                table: "group_011",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status_101",
                table: "deploy_instance_101",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
