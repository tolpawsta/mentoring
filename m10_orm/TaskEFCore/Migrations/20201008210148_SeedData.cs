﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskEFCore.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Description", "CategoryName" },
                values: new object[,]
                {
                    {1,"Soft drinks, coffees, teas, beers, and ales", "Beverages" },
                    {2,"Sweet and savory sauces, relishes, spreads, and seasonings", "Condiments" },
                    {3,"Desserts, candies, and sweet breads", "Confections" },
                    {4,"Cheeses", "Dairy Products" },
                    {5,"Breads, crackers, pasta, and cereal", "Grains/Cereals" },
                    {6,"Prepared meats", "Meat/Poultry" },
                    {7,"Dried fruit and bean curd", "Produce" },
                    {8,"Seaweed and fish", "Seafood" }
                });
            
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "RegionID", "RegionDescription" },
                values: new object[,]
                {
                    {1,"Eastern" },
                    {2,"Western" },
                    {3,"Northern" },
                    {4,"Southern" }
                });

            migrationBuilder.InsertData(
                table: "Territories",
                columns: new[] { "TerritoryID", "RegionId", "TerritoryDescription" },
                values: new object[,]
                {
                    { "01581", 1, "Westboro" },
                    { "01833", 1, "Georgetow" },
                    { "10019", 1, "New York" },
                    { "80202", 2, "Denver" },
                    { "94105", 2, "Menlo Park" },
                    { "03049", 3, "Hollis" },
                    { "44122", 3, "Beachwood" },
                    { "48304", 3, "Bloomfield Hills" },
                    { "29202", 4, "Columbia" },
                    { "72716", 4, "Bentonville" }
                });
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "01581");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "01833");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "03049");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "10019");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "29202");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "44122");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "48304");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "72716");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "80202");

            migrationBuilder.DeleteData(
                table: "Territories",
                keyColumn: "Id",
                keyValue: "94105");

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "RegionID",
                keyValue: 4);           
        }
    }
}
