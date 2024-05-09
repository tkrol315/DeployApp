using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeployApp.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionAddedToDeployDeployLogAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_active_100",
                table: "deploy_100",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<string>(
                name: "description_100",
                table: "deploy_100",
                type: "varchar(250)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.CreateTable(
                name: "deploy_log_102",
                columns: table => new
                {
                    id_102 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_100_101_102 = table.Column<int>(type: "integer", nullable: false),
                    id_004_101_102 = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_102 = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status_102 = table.Column<int>(type: "integer", nullable: false),
                    log_102 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deploy_log_102", x => x.id_102);
                    table.ForeignKey(
                        name: "FK_deploy_log_102_deploy_instance_101_id_100_101_102_id_004_10~",
                        columns: x => new { x.id_100_101_102, x.id_004_101_102 },
                        principalTable: "deploy_instance_101",
                        principalColumns: new[] { "id_100_101", "id_004_101" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deploy_log_102_id_100_101_102_id_004_101_102",
                table: "deploy_log_102",
                columns: new[] { "id_100_101_102", "id_004_101_102" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deploy_log_102");

            migrationBuilder.DropColumn(
                name: "description_100",
                table: "deploy_100");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active_100",
                table: "deploy_100",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);
        }
    }
}
