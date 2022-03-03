﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM.Infrastructure.Migrations
{
    public partial class UpdateFbAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FbTokens_FbAccounts_FbAccountId",
                table: "FbTokens");

            migrationBuilder.AlterColumn<long>(
                name: "FbAccountId",
                table: "FbTokens",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_FbTokens_FbAccounts_FbAccountId",
                table: "FbTokens",
                column: "FbAccountId",
                principalTable: "FbAccounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FbTokens_FbAccounts_FbAccountId",
                table: "FbTokens");

            migrationBuilder.AlterColumn<long>(
                name: "FbAccountId",
                table: "FbTokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FbTokens_FbAccounts_FbAccountId",
                table: "FbTokens",
                column: "FbAccountId",
                principalTable: "FbAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
