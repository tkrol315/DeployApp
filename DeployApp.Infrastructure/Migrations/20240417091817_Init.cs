using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeployApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "group_011",
                columns: table => new
                {
                    id_011 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_011 = table.Column<string>(type: "text", nullable: false),
                    description_011 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_011", x => x.id_011);
                });

            migrationBuilder.CreateTable(
                name: "instance_type_003",
                columns: table => new
                {
                    id_003 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description_003 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instance_type_003", x => x.id_003);
                });

            migrationBuilder.CreateTable(
                name: "project_001",
                columns: table => new
                {
                    id_001 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title_001 = table.Column<string>(type: "text", nullable: false),
                    description_001 = table.Column<string>(type: "text", nullable: false),
                    is_active_001 = table.Column<bool>(type: "boolean", nullable: false),
                    yt_code_001 = table.Column<string>(type: "text", nullable: false),
                    repository_url_001 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_001", x => x.id_001);
                });

            migrationBuilder.CreateTable(
                name: "tag_010",
                columns: table => new
                {
                    id_010 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_010 = table.Column<string>(type: "text", nullable: false),
                    description_010 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_010", x => x.id_010);
                });

            migrationBuilder.CreateTable(
                name: "project_version_002",
                columns: table => new
                {
                    id_002 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_001_002 = table.Column<int>(type: "integer", nullable: false),
                    major_002 = table.Column<int>(type: "integer", nullable: false),
                    minor_002 = table.Column<int>(type: "integer", nullable: false),
                    patch_002 = table.Column<int>(type: "integer", nullable: false),
                    description_002 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_version_002", x => x.id_002);
                    table.ForeignKey(
                        name: "FK_project_version_002_project_001_id_001_002",
                        column: x => x.id_001_002,
                        principalTable: "project_001",
                        principalColumn: "id_001",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instance_004",
                columns: table => new
                {
                    id_004 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_001_004 = table.Column<int>(type: "integer", nullable: false),
                    id_003_004 = table.Column<int>(type: "integer", nullable: false),
                    name_004 = table.Column<string>(type: "text", nullable: true),
                    key_004 = table.Column<string>(type: "text", nullable: false),
                    secret_004 = table.Column<string>(type: "text", nullable: false),
                    id_002_actual_004 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instance_004", x => x.id_004);
                    table.ForeignKey(
                        name: "FK_instance_004_instance_type_003_id_003_004",
                        column: x => x.id_003_004,
                        principalTable: "instance_type_003",
                        principalColumn: "id_003",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_instance_004_project_001_id_001_004",
                        column: x => x.id_001_004,
                        principalTable: "project_001",
                        principalColumn: "id_001",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_instance_004_project_version_002_id_002_actual_004",
                        column: x => x.id_002_actual_004,
                        principalTable: "project_version_002",
                        principalColumn: "id_002");
                });

            migrationBuilder.CreateTable(
                name: "instance_group_006",
                columns: table => new
                {
                    id_004_006 = table.Column<int>(type: "integer", nullable: false),
                    id_011_006 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instance_group_006", x => new { x.id_004_006, x.id_011_006 });
                    table.ForeignKey(
                        name: "FK_instance_group_006_group_011_id_011_006",
                        column: x => x.id_011_006,
                        principalTable: "group_011",
                        principalColumn: "id_011",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_instance_group_006_instance_004_id_004_006",
                        column: x => x.id_004_006,
                        principalTable: "instance_004",
                        principalColumn: "id_004",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instance_tag_005",
                columns: table => new
                {
                    id_004_005 = table.Column<int>(type: "integer", nullable: false),
                    id_010_005 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instance_tag_005", x => new { x.id_004_005, x.id_010_005 });
                    table.ForeignKey(
                        name: "FK_instance_tag_005_instance_004_id_004_005",
                        column: x => x.id_004_005,
                        principalTable: "instance_004",
                        principalColumn: "id_004",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_instance_tag_005_tag_010_id_010_005",
                        column: x => x.id_010_005,
                        principalTable: "tag_010",
                        principalColumn: "id_010",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_group_011_name_011",
                table: "group_011",
                column: "name_011",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_instance_004_id_001_004",
                table: "instance_004",
                column: "id_001_004");

            migrationBuilder.CreateIndex(
                name: "IX_instance_004_id_002_actual_004",
                table: "instance_004",
                column: "id_002_actual_004",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_instance_004_id_003_004",
                table: "instance_004",
                column: "id_003_004",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_instance_group_006_id_011_006",
                table: "instance_group_006",
                column: "id_011_006");

            migrationBuilder.CreateIndex(
                name: "IX_instance_tag_005_id_010_005",
                table: "instance_tag_005",
                column: "id_010_005");

            migrationBuilder.CreateIndex(
                name: "IX_project_001_title_001",
                table: "project_001",
                column: "title_001",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_version_002_id_001_002",
                table: "project_version_002",
                column: "id_001_002");

            migrationBuilder.CreateIndex(
                name: "IX_tag_010_name_010",
                table: "tag_010",
                column: "name_010",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "instance_group_006");

            migrationBuilder.DropTable(
                name: "instance_tag_005");

            migrationBuilder.DropTable(
                name: "group_011");

            migrationBuilder.DropTable(
                name: "instance_004");

            migrationBuilder.DropTable(
                name: "tag_010");

            migrationBuilder.DropTable(
                name: "instance_type_003");

            migrationBuilder.DropTable(
                name: "project_version_002");

            migrationBuilder.DropTable(
                name: "project_001");
        }
    }
}
