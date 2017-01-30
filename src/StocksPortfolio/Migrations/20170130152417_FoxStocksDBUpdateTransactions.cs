using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StocksPortfolio.Migrations
{
    public partial class FoxStocksDBUpdateTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Buy",
                table: "Transactions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Transactions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buy",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Transactions");
        }
    }
}
