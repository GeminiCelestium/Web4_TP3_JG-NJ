using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web2.API.Migrations
{
    /// <inheritdoc />
    public partial class initialSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Villes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Evenements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titre = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Organisateur = table.Column<string>(type: "text", nullable: false),
                    DateFin = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Adresse = table.Column<string>(type: "text", nullable: false),
                    prix = table.Column<double>(type: "double precision", nullable: false),
                    VilleID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evenements_Villes_VilleID",
                        column: x => x.VilleID,
                        principalTable: "Villes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEvenement",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(type: "integer", nullable: false),
                    EvenementsID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEvenement", x => new { x.CategoriesID, x.EvenementsID });
                    table.ForeignKey(
                        name: "FK_CategoryEvenement_Categories_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEvenement_Evenements_EvenementsID",
                        column: x => x.EvenementsID,
                        principalTable: "Evenements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    NombrePlace = table.Column<int>(type: "integer", nullable: false),
                    EvenementID = table.Column<int>(type: "integer", nullable: false),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Participations_Evenements_EvenementID",
                        column: x => x.EvenementID,
                        principalTable: "Evenements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEvenement_EvenementsID",
                table: "CategoryEvenement",
                column: "EvenementsID");

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_VilleID",
                table: "Evenements",
                column: "VilleID");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_EvenementID",
                table: "Participations",
                column: "EvenementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEvenement");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Evenements");

            migrationBuilder.DropTable(
                name: "Villes");
        }
    }
}
