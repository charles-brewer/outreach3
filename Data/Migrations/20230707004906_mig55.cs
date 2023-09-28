﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MadeContact",
                table: "Visitation",
                newName: "Contact");

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitationDate",
                table: "Visitation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitationDate",
                table: "Visitation");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Visitation",
                newName: "MadeContact");
        }
    }
}
