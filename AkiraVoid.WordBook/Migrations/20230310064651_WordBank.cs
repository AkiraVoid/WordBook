using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AkiraVoid.WordBook.Migrations
{
    /// <inheritdoc />
    public partial class WordBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "parts_of_speech",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<int>(type: "INTEGER", nullable: false),
                    display_name = table.Column<string>(type: "TEXT", nullable: true),
                    abbreviation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parts_of_speech", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "words",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    spell = table.Column<string>(type: "TEXT", nullable: true),
                    language = table.Column<int>(type: "INTEGER", nullable: false),
                    added_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    last_memorized_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    memorization_times = table.Column<int>(type: "INTEGER", nullable: false),
                    Pronunciation = table.Column<string>(type: "TEXT", nullable: true),
                    has_memorized = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    is_important = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_words", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "explanations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    explanation = table.Column<string>(type: "TEXT", nullable: true),
                    translation = table.Column<string>(type: "TEXT", nullable: true),
                    word_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    part_of_speech_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_explanations", x => x.id);
                    table.ForeignKey(
                        name: "part_of_speech_to_explanations",
                        column: x => x.part_of_speech_id,
                        principalTable: "parts_of_speech",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "word_to_explanations",
                        column: x => x.word_id,
                        principalTable: "words",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "parts_of_speech",
                columns: new[] { "id", "abbreviation", "display_name", "name" },
                values: new object[,]
                {
                    { new Guid("1315f153-1596-4622-bdba-434b239c63b9"), "助動", "助動詞", 20 },
                    { new Guid("2235054b-f47d-4156-ad20-1b14a4114292"), "名", "名詞", 10 },
                    { new Guid("2254bace-a80c-41b6-9f68-8e6519c3458b"), "conj.", "conjunction", 8 },
                    { new Guid("34cc4f49-c92a-453a-aebe-e3917afb806a"), "pron.", "pronoun", 1 },
                    { new Guid("43b5f7f3-860f-408c-9614-a5ba26d6a18b"), "adj.", "adjective", 2 },
                    { new Guid("49f06470-0b2a-46dc-8b8d-63ae2a036634"), "副", "副詞", 16 },
                    { new Guid("5d127d36-9841-4198-b435-44e976ca6d6a"), "形動", "形容動詞", 15 },
                    { new Guid("5d46507e-65ac-44d7-987e-ddd9b06a8b38"), "n.", "noun", 0 },
                    { new Guid("634b15d4-3934-4977-ae86-f14ac0307ae3"), "v.", "verb", 4 },
                    { new Guid("67959c5c-ae7f-40b7-8953-5382852476e4"), "adv.", "adverb", 3 },
                    { new Guid("6f438479-edd2-46ef-b40e-10086f8cb638"), "感動", "感動詞", 19 },
                    { new Guid("7513069d-13f5-4b1a-bac4-792330a225c0"), "数", "数詞", 12 },
                    { new Guid("78753e63-283d-4419-87d9-a159795c8950"), "連体", "連体詞", 17 },
                    { new Guid("7f433147-fa2b-408a-be15-835cf1b7aa2f"), "art.", "article", 6 },
                    { new Guid("9a95aa8d-0089-4a83-a255-52028645fbde"), "prep.", "preposition", 7 },
                    { new Guid("9c61aff0-79a6-49e1-ba7c-e7432c6ae604"), "代", "代名詞", 11 },
                    { new Guid("ad2b60da-cc90-40f3-b672-085552fe16c6"), "動", "動詞", 13 },
                    { new Guid("b4f48c14-ddac-4baa-bb24-fdcec7bdb76f"), "接続", "接続詞", 18 },
                    { new Guid("b71ab6c6-79d2-429b-bbe0-30336aa27219"), "interj.", "interjection", 9 },
                    { new Guid("c6360920-2930-4db2-84da-024ca152a4c0"), "助", "助詞", 21 },
                    { new Guid("f361aa7f-dd59-4a68-9c7a-6a67c8c702e1"), "形", "形容詞", 14 },
                    { new Guid("fb9c654e-1e02-49ca-9378-b6dd37cc043a"), "num.", "numeral", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_explanations_part_of_speech_id",
                table: "explanations",
                column: "part_of_speech_id");

            migrationBuilder.CreateIndex(
                name: "IX_explanations_word_id",
                table: "explanations",
                column: "word_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "explanations");

            migrationBuilder.DropTable(
                name: "parts_of_speech");

            migrationBuilder.DropTable(
                name: "words");
        }
    }
}
