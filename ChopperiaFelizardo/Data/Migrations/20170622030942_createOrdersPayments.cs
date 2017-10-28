using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChopperiaFelizardo.Migrations
{
    public partial class createOrdersPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersPayments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BoxID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FormPaymentID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrdersPayments_Boxs_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersPayments_FormsPayments_FormPaymentID",
                        column: x => x.FormPaymentID,
                        principalTable: "FormsPayments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersPayments_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersPayments_BoxID",
                table: "OrdersPayments",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersPayments_FormPaymentID",
                table: "OrdersPayments",
                column: "FormPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersPayments_OrderID",
                table: "OrdersPayments",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersPayments");
        }
    }
}
