using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddOrderToQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewQuestionQuestionList");

            migrationBuilder.CreateTable(
                name: "QuestionListInterviewQuestions",
                columns: table => new
                {
                    QuestionListId = table.Column<int>(type: "int", nullable: false),
                    InterviewQuestionId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionListInterviewQuestions", x => new { x.QuestionListId, x.InterviewQuestionId });
                    table.ForeignKey(
                        name: "FK_QuestionListInterviewQuestions_InterviewQuestions_InterviewQuestionId",
                        column: x => x.InterviewQuestionId,
                        principalTable: "InterviewQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionListInterviewQuestions_QuestionLists_QuestionListId",
                        column: x => x.QuestionListId,
                        principalTable: "QuestionLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionListInterviewQuestions_InterviewQuestionId",
                table: "QuestionListInterviewQuestions",
                column: "InterviewQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionListInterviewQuestions");

            migrationBuilder.CreateTable(
                name: "InterviewQuestionQuestionList",
                columns: table => new
                {
                    InterviewQuestionsId = table.Column<int>(type: "int", nullable: false),
                    QuestionListsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewQuestionQuestionList", x => new { x.InterviewQuestionsId, x.QuestionListsId });
                    table.ForeignKey(
                        name: "FK_InterviewQuestionQuestionList_InterviewQuestions_InterviewQuestionsId",
                        column: x => x.InterviewQuestionsId,
                        principalTable: "InterviewQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewQuestionQuestionList_QuestionLists_QuestionListsId",
                        column: x => x.QuestionListsId,
                        principalTable: "QuestionLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewQuestionQuestionList_QuestionListsId",
                table: "InterviewQuestionQuestionList",
                column: "QuestionListsId");
        }
    }
}
