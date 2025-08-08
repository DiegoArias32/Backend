using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NUMERO_CLIENTE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TIPO_DOCUMENTO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    NUMERO_DOCUMENTO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    NOMBRE_COMPLETO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TELEFONO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CELULAR = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DIRECCION = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    IS_ACTIVE = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SEDES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOMBRE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CODIGO = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    DIRECCION = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    TELEFONO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CIUDAD = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DEPARTAMENTO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ES_PRINCIPAL = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    IS_ACTIVE = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEDES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TIPOS_CITA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOMBRE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRIPCION = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    ICONO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    TIEMPO_ESTIMADO_MINUTOS = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 120),
                    REQUIERE_DOCUMENTACION = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    IS_ACTIVE = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOS_CITA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CITAS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NUMERO_CITA = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    FECHA_CITA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    HORA_CITA = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false, defaultValue: "Pendiente"),
                    OBSERVACIONES = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    FECHA_COMPLETADA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    TECNICO_ASIGNADO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    OBSERVACIONES_TECNICO = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    CLIENTE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SEDE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO_CITA_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    IS_ACTIVE = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CITAS_CLIENTES",
                        column: x => x.CLIENTE_ID,
                        principalTable: "CLIENTES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CITAS_SEDES",
                        column: x => x.SEDE_ID,
                        principalTable: "SEDES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CITAS_TIPOS_CITA",
                        column: x => x.TIPO_CITA_ID,
                        principalTable: "TIPOS_CITA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HORAS_DISPONIBLES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    HORA = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false),
                    SEDE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO_CITA_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    IS_ACTIVE = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HORAS_DISPONIBLES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HORASDISPONIBLES_SEDE",
                        column: x => x.SEDE_ID,
                        principalTable: "SEDES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HORASDISPONIBLES_TIPOCITA",
                        column: x => x.TIPO_CITA_ID,
                        principalTable: "TIPOS_CITA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CITAS_CLIENTE_FECHA",
                table: "CITAS",
                columns: new[] { "CLIENTE_ID", "FECHA_CITA" });

            migrationBuilder.CreateIndex(
                name: "IX_CITAS_ESTADO",
                table: "CITAS",
                column: "ESTADO");

            migrationBuilder.CreateIndex(
                name: "IX_CITAS_FECHA_CITA",
                table: "CITAS",
                column: "FECHA_CITA");

            migrationBuilder.CreateIndex(
                name: "IX_CITAS_NUMERO_CITA",
                table: "CITAS",
                column: "NUMERO_CITA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CITAS_SEDE_ID",
                table: "CITAS",
                column: "SEDE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CITAS_TIPO_CITA_ID",
                table: "CITAS",
                column: "TIPO_CITA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_EMAIL",
                table: "CLIENTES",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_NUMERO_CLIENTE",
                table: "CLIENTES",
                column: "NUMERO_CLIENTE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_NUMERO_DOCUMENTO",
                table: "CLIENTES",
                column: "NUMERO_DOCUMENTO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HORAS_DISPONIBLES_TIPO_CITA_ID",
                table: "HORAS_DISPONIBLES",
                column: "TIPO_CITA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HORASDISPONIBLES_SEDE_HORA",
                table: "HORAS_DISPONIBLES",
                columns: new[] { "SEDE_ID", "HORA" });

            migrationBuilder.CreateIndex(
                name: "IX_SEDES_CODIGO",
                table: "SEDES",
                column: "CODIGO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CITAS");

            migrationBuilder.DropTable(
                name: "HORAS_DISPONIBLES");

            migrationBuilder.DropTable(
                name: "CLIENTES");

            migrationBuilder.DropTable(
                name: "SEDES");

            migrationBuilder.DropTable(
                name: "TIPOS_CITA");
        }
    }
}
