using Microsoft.EntityFrameworkCore.Migrations;

namespace BilisimBilgisayar.Data.Migrations
{
    public partial class ilk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler_Tablosu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler_Tablosu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Urunler_Tablosu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunResimDosyasi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunTeknikBilgileri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunFiyat = table.Column<float>(type: "real", nullable: false),
                    kategoriID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler_Tablosu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Urunler_Tablosu_Kategoriler_Tablosu_kategoriID",
                        column: x => x.kategoriID,
                        principalTable: "Kategoriler_Tablosu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_Tablosu_kategoriID",
                table: "Urunler_Tablosu",
                column: "kategoriID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urunler_Tablosu");

            migrationBuilder.DropTable(
                name: "Kategoriler_Tablosu");
        }
    }
}
