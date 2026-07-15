using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace finanzas_backend.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Detalle_Cuotas",
                columns: table => new
                {
                    id_detalle_cuotas = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    monto_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    cantidad_cuotas = table.Column<int>(type: "integer", nullable: false),
                    monto_cuota = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    cuotas_pagadas = table.Column<int>(type: "integer", nullable: false),
                    pagada_completamente = table.Column<bool>(type: "boolean", nullable: false),
                    fecha_compra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_ultima_cuota = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Cuotas", x => x.id_detalle_cuotas);
                });

            migrationBuilder.CreateTable(
                name: "Subscripcion",
                columns: table => new
                {
                    id_subscripcion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    precio = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscripcion", x => x.id_subscripcion);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Transaccion",
                columns: table => new
                {
                    id_tipo_transaccion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Transaccion", x => x.id_tipo_transaccion);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    nombre_usuario = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    correo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    uid_auth = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Categoria_Gasto",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria_Gasto", x => x.id_categoria);
                    table.ForeignKey(
                        name: "FK_Categoria_Gasto_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Credito",
                columns: table => new
                {
                    id_credito = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    entidad = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credito", x => x.id_credito);
                    table.ForeignKey(
                        name: "FK_Credito_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deudas",
                columns: table => new
                {
                    id_deuda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    monto_original = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    saldo_actual = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    tasa_interes = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deudas", x => x.id_deuda);
                    table.ForeignKey(
                        name: "FK_Deudas_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Metodo_Pago",
                columns: table => new
                {
                    id_metodo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metodo_Pago", x => x.id_metodo);
                    table.ForeignKey(
                        name: "FK_Metodo_Pago_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pago_Subscripcion",
                columns: table => new
                {
                    id_pago = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_subscripcion = table.Column<int>(type: "integer", nullable: false),
                    mes = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    precio = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago_Subscripcion", x => x.id_pago);
                    table.ForeignKey(
                        name: "FK_Pago_Subscripcion_Subscripcion_id_subscripcion",
                        column: x => x.id_subscripcion,
                        principalTable: "Subscripcion",
                        principalColumn: "id_subscripcion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pago_Subscripcion_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presupuesto",
                columns: table => new
                {
                    id_presupuesto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    anio = table.Column<int>(type: "integer", nullable: false),
                    mes = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuesto", x => x.id_presupuesto);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Gasto",
                columns: table => new
                {
                    id_tipo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Gasto", x => x.id_tipo);
                    table.ForeignKey(
                        name: "FK_Tipo_Gasto_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cupo_Credito",
                columns: table => new
                {
                    id_cupo_credito = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_credito = table.Column<int>(type: "integer", nullable: false),
                    monto = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupo_Credito", x => x.id_cupo_credito);
                    table.ForeignKey(
                        name: "FK_Cupo_Credito_Credito_id_credito",
                        column: x => x.id_credito,
                        principalTable: "Credito",
                        principalColumn: "id_credito",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presupuesto_Item",
                columns: table => new
                {
                    id_presupuesto_item = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_presupuesto = table.Column<int>(type: "integer", nullable: false),
                    id_categoria_gasto = table.Column<int>(type: "integer", nullable: true),
                    id_tipo_gasto = table.Column<int>(type: "integer", nullable: true),
                    tipo_item = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    monto_objetivo = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    monto_real = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presupuesto_Item", x => x.id_presupuesto_item);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Item_Categoria_Gasto_id_categoria_gasto",
                        column: x => x.id_categoria_gasto,
                        principalTable: "Categoria_Gasto",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Item_Presupuesto_id_presupuesto",
                        column: x => x.id_presupuesto,
                        principalTable: "Presupuesto",
                        principalColumn: "id_presupuesto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presupuesto_Item_Tipo_Gasto_id_tipo_gasto",
                        column: x => x.id_tipo_gasto,
                        principalTable: "Tipo_Gasto",
                        principalColumn: "id_tipo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaccion",
                columns: table => new
                {
                    id_transaccion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    monto = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_tipo_transaccion = table.Column<int>(type: "integer", nullable: false),
                    id_metodo_pago = table.Column<int>(type: "integer", nullable: false),
                    id_categoria_gasto = table.Column<int>(type: "integer", nullable: true),
                    id_tipo_gasto = table.Column<int>(type: "integer", nullable: true),
                    id_detalle_cuotas = table.Column<int>(type: "integer", nullable: true),
                    id_credito = table.Column<int>(type: "integer", nullable: true),
                    id_deuda = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaccion", x => x.id_transaccion);
                    table.ForeignKey(
                        name: "FK_Transaccion_Categoria_Gasto_id_categoria_gasto",
                        column: x => x.id_categoria_gasto,
                        principalTable: "Categoria_Gasto",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Credito_id_credito",
                        column: x => x.id_credito,
                        principalTable: "Credito",
                        principalColumn: "id_credito",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Detalle_Cuotas_id_detalle_cuotas",
                        column: x => x.id_detalle_cuotas,
                        principalTable: "Detalle_Cuotas",
                        principalColumn: "id_detalle_cuotas",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Deudas_id_deuda",
                        column: x => x.id_deuda,
                        principalTable: "Deudas",
                        principalColumn: "id_deuda",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Metodo_Pago_id_metodo_pago",
                        column: x => x.id_metodo_pago,
                        principalTable: "Metodo_Pago",
                        principalColumn: "id_metodo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Tipo_Gasto_id_tipo_gasto",
                        column: x => x.id_tipo_gasto,
                        principalTable: "Tipo_Gasto",
                        principalColumn: "id_tipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Tipo_Transaccion_id_tipo_transaccion",
                        column: x => x.id_tipo_transaccion,
                        principalTable: "Tipo_Transaccion",
                        principalColumn: "id_tipo_transaccion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaccion_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SoporteFactura",
                columns: table => new
                {
                    id_factura = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_transaccion = table.Column<int>(type: "integer", nullable: true),
                    nombre_archivo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    tipo_mime = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tamano_bytes = table.Column<int>(type: "integer", nullable: false),
                    fecha_subida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    procesado_ia = table.Column<bool>(type: "boolean", nullable: false),
                    direccion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoporteFactura", x => x.id_factura);
                    table.ForeignKey(
                        name: "FK_SoporteFactura_Transaccion_id_transaccion",
                        column: x => x.id_transaccion,
                        principalTable: "Transaccion",
                        principalColumn: "id_transaccion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Gasto_id_usuario",
                table: "Categoria_Gasto",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Credito_id_usuario",
                table: "Credito",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Cupo_Credito_id_credito",
                table: "Cupo_Credito",
                column: "id_credito");

            migrationBuilder.CreateIndex(
                name: "IX_Deudas_id_usuario",
                table: "Deudas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Metodo_Pago_id_usuario",
                table: "Metodo_Pago",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Subscripcion_id_subscripcion",
                table: "Pago_Subscripcion",
                column: "id_subscripcion");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Subscripcion_id_usuario",
                table: "Pago_Subscripcion",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_id_usuario",
                table: "Presupuesto",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_Item_id_categoria_gasto",
                table: "Presupuesto_Item",
                column: "id_categoria_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_Item_id_presupuesto",
                table: "Presupuesto_Item",
                column: "id_presupuesto");

            migrationBuilder.CreateIndex(
                name: "IX_Presupuesto_Item_id_tipo_gasto",
                table: "Presupuesto_Item",
                column: "id_tipo_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_SoporteFactura_id_transaccion",
                table: "SoporteFactura",
                column: "id_transaccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Gasto_id_usuario",
                table: "Tipo_Gasto",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_categoria_gasto",
                table: "Transaccion",
                column: "id_categoria_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_credito",
                table: "Transaccion",
                column: "id_credito");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_detalle_cuotas",
                table: "Transaccion",
                column: "id_detalle_cuotas");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_deuda",
                table: "Transaccion",
                column: "id_deuda");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_metodo_pago",
                table: "Transaccion",
                column: "id_metodo_pago");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_tipo_gasto",
                table: "Transaccion",
                column: "id_tipo_gasto");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_tipo_transaccion",
                table: "Transaccion",
                column: "id_tipo_transaccion");

            migrationBuilder.CreateIndex(
                name: "IX_Transaccion_id_usuario",
                table: "Transaccion",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cupo_Credito");

            migrationBuilder.DropTable(
                name: "Pago_Subscripcion");

            migrationBuilder.DropTable(
                name: "Presupuesto_Item");

            migrationBuilder.DropTable(
                name: "SoporteFactura");

            migrationBuilder.DropTable(
                name: "Subscripcion");

            migrationBuilder.DropTable(
                name: "Presupuesto");

            migrationBuilder.DropTable(
                name: "Transaccion");

            migrationBuilder.DropTable(
                name: "Categoria_Gasto");

            migrationBuilder.DropTable(
                name: "Credito");

            migrationBuilder.DropTable(
                name: "Detalle_Cuotas");

            migrationBuilder.DropTable(
                name: "Deudas");

            migrationBuilder.DropTable(
                name: "Metodo_Pago");

            migrationBuilder.DropTable(
                name: "Tipo_Gasto");

            migrationBuilder.DropTable(
                name: "Tipo_Transaccion");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
