using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WppCampaign.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WppCampaign");

            migrationBuilder.CreateTable(
                name: "CommunityData",
                schema: "WppCampaign",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunityID = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LogData",
                schema: "WppCampaign",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageID = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    MessageSent = table.Column<string>(type: "nvarchar(4000)", nullable: true),
                    IsReceivedMessage = table.Column<bool>(type: "bit", nullable: false),
                    IsReadMessage = table.Column<bool>(type: "bit", nullable: false),
                    StatusCode = table.Column<short>(type: "smallint", nullable: true),
                    ResponseMessage = table.Column<string>(type: "nvarchar(4000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogData", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "MessageData",
                schema: "WppCampaign",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageText = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPollMessage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WppGroup",
                schema: "WppCampaign",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    InvitationLink = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    IsGroupAnnouncement = table.Column<bool>(type: "bit", nullable: false),
                    CommunityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WppGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WppGroup_CommunityData_CommunityID",
                        column: x => x.CommunityID,
                        principalSchema: "WppCampaign",
                        principalTable: "CommunityData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SendPoll",
                schema: "WppCampaign",
                columns: table => new
                {
                    PollID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageID = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PollMaxOptions = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendPoll", x => x.PollID);
                    table.ForeignKey(
                        name: "FK_SendPoll_MessageData_MessageID",
                        column: x => x.MessageID,
                        principalSchema: "WppCampaign",
                        principalTable: "MessageData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberGroup",
                schema: "WppCampaign",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    Phones = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsMember = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberGroup", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_MemberGroup_WppGroup_GroupID",
                        column: x => x.GroupID,
                        principalSchema: "WppCampaign",
                        principalTable: "WppGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PollOptions",
                schema: "WppCampaign",
                columns: table => new
                {
                    PollOptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PollID = table.Column<int>(type: "int", nullable: false),
                    OptionName = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollOptions", x => x.PollOptionID);
                    table.ForeignKey(
                        name: "FK_PollOptions_SendPoll_PollID",
                        column: x => x.PollID,
                        principalSchema: "WppCampaign",
                        principalTable: "SendPoll",
                        principalColumn: "PollID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityData_CommunityID",
                schema: "WppCampaign",
                table: "CommunityData",
                column: "CommunityID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberGroup_GroupID",
                schema: "WppCampaign",
                table: "MemberGroup",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageData_MessageText",
                schema: "WppCampaign",
                table: "MessageData",
                column: "MessageText");

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_OptionName",
                schema: "WppCampaign",
                table: "PollOptions",
                column: "OptionName");

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_PollID",
                schema: "WppCampaign",
                table: "PollOptions",
                column: "PollID");

            migrationBuilder.CreateIndex(
                name: "IX_SendPoll_MessageID",
                schema: "WppCampaign",
                table: "SendPoll",
                column: "MessageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WppGroup_CommunityID",
                schema: "WppCampaign",
                table: "WppGroup",
                column: "CommunityID");

            migrationBuilder.CreateIndex(
                name: "IX_WppGroup_Name",
                schema: "WppCampaign",
                table: "WppGroup",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogData",
                schema: "WppCampaign");

            migrationBuilder.DropTable(
                name: "MemberGroup",
                schema: "WppCampaign");

            migrationBuilder.DropTable(
                name: "PollOptions",
                schema: "WppCampaign");

            migrationBuilder.DropTable(
                name: "WppGroup",
                schema: "WppCampaign");

            migrationBuilder.DropTable(
                name: "SendPoll",
                schema: "WppCampaign");

            migrationBuilder.DropTable(
                name: "CommunityData",
                schema: "WppCampaign");

            migrationBuilder.DropTable(
                name: "MessageData",
                schema: "WppCampaign");
        }
    }
}
