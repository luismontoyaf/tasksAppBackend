﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBackend.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionModeloTareas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tareas");

            migrationBuilder.AddColumn<bool>(
                name: "Completada",
                table: "Tareas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Tareas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Tareas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completada",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Tareas");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Tareas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioId",
                table: "Tareas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_UsuarioId",
                table: "Tareas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
