using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeployApp.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Deploy_And_Deploy_Instance_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deploy_100",
                columns: table => new
                {
                    id_100 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_001_100 = table.Column<int>(type: "integer", nullable: false),
                    id_002_100 = table.Column<int>(type: "integer", nullable: false),
                    start_100 = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_100 = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active_100 = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deploy_100", x => x.id_100);
                    table.ForeignKey(
                        name: "FK_deploy_100_project_001_id_001_100",
                        column: x => x.id_001_100,
                        principalTable: "project_001",
                        principalColumn: "id_001",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_deploy_100_project_version_002_id_002_100",
                        column: x => x.id_002_100,
                        principalTable: "project_version_002",
                        principalColumn: "id_002",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deploy_instance_101",
                columns: table => new
                {
                    id_100_101 = table.Column<int>(type: "integer", nullable: false),
                    id_004_101 = table.Column<int>(type: "integer", nullable: false),
                    status_101 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deploy_instance_101", x => new { x.id_100_101, x.id_004_101 });
                    table.ForeignKey(
                        name: "FK_deploy_instance_101_deploy_100_id_100_101",
                        column: x => x.id_100_101,
                        principalTable: "deploy_100",
                        principalColumn: "id_100",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_deploy_instance_101_instance_004_id_004_101",
                        column: x => x.id_004_101,
                        principalTable: "instance_004",
                        principalColumn: "id_004",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deploy_100_id_001_100",
                table: "deploy_100",
                column: "id_001_100");

            migrationBuilder.CreateIndex(
                name: "IX_deploy_100_id_002_100",
                table: "deploy_100",
                column: "id_002_100");

            migrationBuilder.CreateIndex(
                name: "IX_deploy_instance_101_id_004_101",
                table: "deploy_instance_101",
                column: "id_004_101");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deploy_instance_101");

            migrationBuilder.DropTable(
                name: "deploy_100");
        }
    }
}
