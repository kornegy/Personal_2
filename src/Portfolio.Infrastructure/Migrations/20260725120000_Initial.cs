using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Infrastructure.Migrations;

/// <summary>Первая миграция: создаёт все таблицы сайта.</summary>
[Migration("20260725120000_Initial")]
public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ContactMessages",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                SenderName = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                SenderEmail = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                Subject = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Body = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                CreatedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                SenderIpHash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ContactMessages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Experiences",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                Company = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Position = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                StartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                EndDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                SortOrder = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Experiences", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Profiles",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                FullName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Title = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                Headline = table.Column<string>(type: "TEXT", maxLength: 320, nullable: false),
                About = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                Location = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                Phone = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                PhotoUrl = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                ResumeUrl = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                CareerStartYear = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Profiles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Projects",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                Title = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Summary = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                ImageUrl = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                DemoUrl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                SourceUrl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                Year = table.Column<int>(type: "INTEGER", nullable: false),
                SortOrder = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Projects", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SkillCategories",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                LanguageCode = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                Icon = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                SortOrder = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SkillCategories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SocialLinks",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                Url = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                Icon = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                ProfileId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SocialLinks", x => x.Id);
                table.ForeignKey(
                    name: "FK_SocialLinks_Profiles_ProfileId",
                    column: x => x.ProfileId,
                    principalTable: "Profiles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProjectTechnologies",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProjectTechnologies", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProjectTechnologies_Projects_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Projects",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Skills",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                SkillCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Skills", x => x.Id);
                table.ForeignKey(
                    name: "FK_Skills_SkillCategories_SkillCategoryId",
                    column: x => x.SkillCategoryId,
                    principalTable: "SkillCategories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ContactMessages_SenderIpHash_CreatedAtUtc",
            table: "ContactMessages",
            columns: ["SenderIpHash", "CreatedAtUtc"]);

        migrationBuilder.CreateIndex(
            name: "IX_Experiences_LanguageCode_SortOrder",
            table: "Experiences",
            columns: ["LanguageCode", "SortOrder"]);

        migrationBuilder.CreateIndex(
            name: "IX_Profiles_LanguageCode",
            table: "Profiles",
            column: "LanguageCode",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Projects_LanguageCode_SortOrder",
            table: "Projects",
            columns: ["LanguageCode", "SortOrder"]);

        migrationBuilder.CreateIndex(
            name: "IX_ProjectTechnologies_ProjectId_SortOrder",
            table: "ProjectTechnologies",
            columns: ["ProjectId", "SortOrder"]);

        migrationBuilder.CreateIndex(
            name: "IX_SkillCategories_LanguageCode_SortOrder",
            table: "SkillCategories",
            columns: ["LanguageCode", "SortOrder"]);

        migrationBuilder.CreateIndex(
            name: "IX_Skills_SkillCategoryId_SortOrder",
            table: "Skills",
            columns: ["SkillCategoryId", "SortOrder"]);

        migrationBuilder.CreateIndex(
            name: "IX_SocialLinks_ProfileId",
            table: "SocialLinks",
            column: "ProfileId");

        migrationBuilder.CreateIndex(
            name: "IX_SocialLinks_SortOrder",
            table: "SocialLinks",
            column: "SortOrder");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ContactMessages");
        migrationBuilder.DropTable(name: "Experiences");
        migrationBuilder.DropTable(name: "ProjectTechnologies");
        migrationBuilder.DropTable(name: "Skills");
        migrationBuilder.DropTable(name: "SocialLinks");
        migrationBuilder.DropTable(name: "Projects");
        migrationBuilder.DropTable(name: "SkillCategories");
        migrationBuilder.DropTable(name: "Profiles");
    }
}
