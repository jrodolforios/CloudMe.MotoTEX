using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(nullable: false),
                    ClientName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(nullable: false),
                    AllowRememberConsent = table.Column<bool>(nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(nullable: false),
                    RequirePkce = table.Column<bool>(nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    BackChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    AllowOfflineAccess = table.Column<bool>(nullable: false),
                    IdentityTokenLifetime = table.Column<int>(nullable: false),
                    AccessTokenLifetime = table.Column<int>(nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(nullable: false),
                    ConsentLifetime = table.Column<int>(nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(nullable: false),
                    RefreshTokenUsage = table.Column<int>(nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(nullable: false),
                    RefreshTokenExpiration = table.Column<int>(nullable: false),
                    AccessTokenType = table.Column<int>(nullable: false),
                    EnableLocalLogin = table.Column<bool>(nullable: false),
                    IncludeJwtId = table.Column<bool>(nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(nullable: false),
                    ClientClaimsPrefix = table.Column<string>(maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    UserSsoLifetime = table.Column<int>(nullable: true),
                    UserCodeType = table.Column<string>(maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(nullable: false),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    Level = table.Column<string>(maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    LogEvent = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiClaims_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiProperties_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopes_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiSecrets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiSecrets_ApiResources_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientClaims_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientCorsOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Origin = table.Column<string>(maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCorsOrigins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCorsOrigins_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGrantTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    GrantType = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGrantTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientGrantTypes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientIdPRestrictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Provider = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdPRestrictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientIdPRestrictions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPostLogoutRedirectUris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostLogoutRedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPostLogoutRedirectUris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPostLogoutRedirectUris_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProperties_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRedirectUris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRedirectUris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRedirectUris_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientScopes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Scope = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientScopes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSecrets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecrets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSecrets_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityClaims_IdentityResources_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityProperties_IdentityResources_IdentityResourceId",
                        column: x => x.IdentityResourceId,
                        principalTable: "IdentityResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Conteudo = table.Column<string>(nullable: false),
                    UltimaVersao = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrato_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CorVeiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorVeiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorVeiculo_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorVeiculo_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorVeiculo_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaixaDesconto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Valor = table.Column<float>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixaDesconto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaixaDesconto_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaixaDesconto_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaixaDesconto_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faturamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Ano = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: true),
                    PercentualComissao = table.Column<decimal>(nullable: false),
                    DataGeracao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faturamento_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faturamento_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faturamento_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormaPagamento_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormaPagamento_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormaPagamento_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    NomeArquivo = table.Column<string>(nullable: true),
                    Dados = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foto_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foto_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Foto_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupoUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Localizacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Endereco = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: false),
                    NomePublico = table.Column<string>(nullable: true),
                    IdUsuario = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localizacao_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Localizacao_Users_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Localizacao_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Localizacao_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensagem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdRemetente = table.Column<Guid>(nullable: false),
                    Assunto = table.Column<string>(nullable: true),
                    Corpo = table.Column<string>(nullable: false),
                    Apagada = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagem_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensagem_Users_IdRemetente",
                        column: x => x.IdRemetente,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensagem_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensagem_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rota",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rota_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rota_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rota_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tarifa",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Bandeirada = table.Column<float>(nullable: false),
                    KmRodadoBandeira1 = table.Column<float>(nullable: false),
                    KmRodadoBandeira2 = table.Column<float>(nullable: false),
                    HoraParada = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarifa_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tarifa_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tarifa_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiScopeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiScopeClaims_ApiScopes_ApiScopeId",
                        column: x => x.ApiScopeId,
                        principalTable: "ApiScopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Placa = table.Column<string>(nullable: false),
                    Marca = table.Column<string>(nullable: false),
                    Modelo = table.Column<string>(nullable: false),
                    Ano = table.Column<string>(nullable: false),
                    Versao = table.Column<string>(nullable: true),
                    Capacidade = table.Column<int>(nullable: false),
                    Cor = table.Column<string>(nullable: true),
                    IdFoto = table.Column<Guid>(nullable: true),
                    IdCorVeiculo = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Veiculo_CorVeiculo_IdCorVeiculo",
                        column: x => x.IdCorVeiculo,
                        principalTable: "CorVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Veiculo_Foto_IdFoto",
                        column: x => x.IdFoto,
                        principalTable: "Foto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Veiculo_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Veiculo_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioGrupoUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdUsuario = table.Column<Guid>(nullable: false),
                    IdGrupoUsuario = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioGrupoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoUsuario_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoUsuario_GrupoUsuario_IdGrupoUsuario",
                        column: x => x.IdGrupoUsuario,
                        principalTable: "GrupoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoUsuario_Users_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoUsuario_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoUsuario_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    CEP = table.Column<string>(nullable: false),
                    Logradouro = table.Column<string>(nullable: false),
                    Numero = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: false),
                    Localidade = table.Column<string>(nullable: false),
                    UF = table.Column<string>(nullable: false),
                    IdLocalizacao = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Localizacao_IdLocalizacao",
                        column: x => x.IdLocalizacao,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MensagemDestinatario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdMensagem = table.Column<Guid>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: false),
                    IdGrupoUsuario = table.Column<Guid>(nullable: true),
                    DataRecebimento = table.Column<DateTime>(nullable: true),
                    DataLeitura = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagemDestinatario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_GrupoUsuario_IdGrupoUsuario",
                        column: x => x.IdGrupoUsuario,
                        principalTable: "GrupoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_Mensagem_IdMensagem",
                        column: x => x.IdMensagem,
                        principalTable: "Mensagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_Users_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Passageiro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: true),
                    IdEndereco = table.Column<Guid>(nullable: false),
                    IdLocalizacaoAtual = table.Column<Guid>(nullable: true),
                    IdFoto = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passageiro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passageiro_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passageiro_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passageiro_Foto_IdFoto",
                        column: x => x.IdFoto,
                        principalTable: "Foto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passageiro_Localizacao_IdLocalizacaoAtual",
                        column: x => x.IdLocalizacaoAtual,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passageiro_Users_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passageiro_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passageiro_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PontoTaxi",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    IdEndereco = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoTaxi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontoTaxi_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PontoTaxi_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PontoTaxi_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PontoTaxi_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoCorrida",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdPassageiro = table.Column<Guid>(nullable: false),
                    IdLocalizacaoOrigem = table.Column<Guid>(nullable: false),
                    IdLocalizacaoDestino = table.Column<Guid>(nullable: false),
                    IdRota = table.Column<Guid>(nullable: true),
                    IdFormaPagamento = table.Column<Guid>(nullable: false),
                    IdFaixaDesconto = table.Column<Guid>(nullable: true),
                    TipoAtendimento = table.Column<int>(nullable: false, defaultValue: 0),
                    Data = table.Column<DateTime>(nullable: true),
                    ETA = table.Column<int>(nullable: false),
                    TempoDisponivel = table.Column<int>(nullable: true),
                    ValorEstimado = table.Column<float>(nullable: true),
                    ValorProposto = table.Column<float>(nullable: true),
                    Situacao = table.Column<int>(nullable: false, defaultValue: 0),
                    StatusMonitoramento = table.Column<int>(nullable: false, defaultValue: 0),
                    IsInterUrbano = table.Column<bool>(nullable: false, defaultValue: false),
                    IdxFaixaBusca = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoCorrida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_FaixaDesconto_IdFaixaDesconto",
                        column: x => x.IdFaixaDesconto,
                        principalTable: "FaixaDesconto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_FormaPagamento_IdFormaPagamento",
                        column: x => x.IdFormaPagamento,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_Localizacao_IdLocalizacaoDestino",
                        column: x => x.IdLocalizacaoDestino,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_Localizacao_IdLocalizacaoOrigem",
                        column: x => x.IdLocalizacaoOrigem,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_Passageiro_IdPassageiro",
                        column: x => x.IdPassageiro,
                        principalTable: "Passageiro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_Rota_IdRota",
                        column: x => x.IdRota,
                        principalTable: "Rota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorrida_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Taxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Disponivel = table.Column<bool>(nullable: false, defaultValue: false),
                    NumeroIdentificacao = table.Column<int>(nullable: true),
                    IdUsuario = table.Column<Guid>(nullable: true),
                    IdEndereco = table.Column<Guid>(nullable: false),
                    IdLocalizacaoAtual = table.Column<Guid>(nullable: true),
                    IdFoto = table.Column<Guid>(nullable: false),
                    IdPontoTaxi = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxista_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taxista_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Taxista_Foto_IdFoto",
                        column: x => x.IdFoto,
                        principalTable: "Foto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Taxista_Localizacao_IdLocalizacaoAtual",
                        column: x => x.IdLocalizacaoAtual,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taxista_PontoTaxi_IdPontoTaxi",
                        column: x => x.IdPontoTaxi,
                        principalTable: "PontoTaxi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taxista_Users_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taxista_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taxista_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Assunto = table.Column<string>(nullable: false),
                    Conteudo = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IdTaxista = table.Column<Guid>(nullable: true),
                    IdPassageiro = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contato_Passageiro_IdPassageiro",
                        column: x => x.IdPassageiro,
                        principalTable: "Passageiro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contato_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contato_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contato_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emergencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    TaxistaId = table.Column<Guid>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emergencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emergencias_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emergencias_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emergencias_Taxista_TaxistaId",
                        column: x => x.TaxistaId,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emergencias_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaixaDescontoTaxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdFaixaDesconto = table.Column<Guid>(nullable: false),
                    IdTaxista = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixaDescontoTaxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaixaDescontoTaxista_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaixaDescontoTaxista_FaixaDesconto_IdFaixaDesconto",
                        column: x => x.IdFaixaDesconto,
                        principalTable: "FaixaDesconto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaixaDescontoTaxista_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaixaDescontoTaxista_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaixaDescontoTaxista_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaturamentoTaxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdFaturamento = table.Column<Guid>(nullable: false),
                    FaturamentoId = table.Column<Guid>(nullable: true),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    LinkBoleto = table.Column<string>(nullable: true),
                    JsonBoletoAPI = table.Column<string>(nullable: true),
                    DataGeracao = table.Column<DateTime>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturamentoTaxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Faturamento_FaturamentoId",
                        column: x => x.FaturamentoId,
                        principalTable: "Faturamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorito",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdPassageiro = table.Column<Guid>(nullable: false),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    Preferencia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorito_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorito_Passageiro_IdPassageiro",
                        column: x => x.IdPassageiro,
                        principalTable: "Passageiro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorito_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorito_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorito_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamentoTaxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdFormaPagamento = table.Column<Guid>(nullable: false),
                    IdTaxista = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamentoTaxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormaPagamentoTaxista_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormaPagamentoTaxista_FormaPagamento_IdFormaPagamento",
                        column: x => x.IdFormaPagamento,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormaPagamentoTaxista_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormaPagamentoTaxista_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormaPagamentoTaxista_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoCorridaTaxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdSolicitacaoCorrida = table.Column<Guid>(nullable: false),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    Acao = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoCorridaTaxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorridaTaxista_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorridaTaxista_SolicitacaoCorrida_IdSolicitacaoC~",
                        column: x => x.IdSolicitacaoCorrida,
                        principalTable: "SolicitacaoCorrida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorridaTaxista_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorridaTaxista_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorridaTaxista_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoTaxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdVeiculo = table.Column<Guid>(nullable: false),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoTaxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculoTaxista_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VeiculoTaxista_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeiculoTaxista_Veiculo_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeiculoTaxista_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VeiculoTaxista_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Corrida",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdSolicitacao = table.Column<Guid>(nullable: false),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    IdVeiculo = table.Column<Guid>(nullable: false),
                    IdRotaExecutada = table.Column<Guid>(nullable: true),
                    IdTarifa = table.Column<Guid>(nullable: false),
                    Inicio = table.Column<DateTime>(nullable: true),
                    Fim = table.Column<DateTime>(nullable: true),
                    UltimaPausa = table.Column<DateTime>(nullable: true),
                    AvaliacaoTaxista = table.Column<int>(nullable: true, defaultValue: 0),
                    AvaliacaoPassageiro = table.Column<int>(nullable: true, defaultValue: 0),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    TempoEmEspera = table.Column<int>(nullable: false, defaultValue: 0),
                    IdFaturamentoTaxista = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corrida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corrida_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Corrida_FaturamentoTaxista_IdFaturamentoTaxista",
                        column: x => x.IdFaturamentoTaxista,
                        principalTable: "FaturamentoTaxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Corrida_Rota_IdRotaExecutada",
                        column: x => x.IdRotaExecutada,
                        principalTable: "Rota",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Corrida_SolicitacaoCorrida_IdSolicitacao",
                        column: x => x.IdSolicitacao,
                        principalTable: "SolicitacaoCorrida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corrida_Tarifa_IdTarifa",
                        column: x => x.IdTarifa,
                        principalTable: "Tarifa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corrida_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corrida_Veiculo_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Corrida_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Corrida_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiClaims_ApiResourceId",
                table: "ApiClaims",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiProperties_ApiResourceId",
                table: "ApiProperties",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiResources_Name",
                table: "ApiResources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopeClaims_ApiScopeId",
                table: "ApiScopeClaims",
                column: "ApiScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopes_ApiResourceId",
                table: "ApiScopes",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiScopes_Name",
                table: "ApiScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiSecrets_ApiResourceId",
                table: "ApiSecrets",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientClaims_ClientId",
                table: "ClientClaims",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCorsOrigins_ClientId",
                table: "ClientCorsOrigins",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGrantTypes_ClientId",
                table: "ClientGrantTypes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdPRestrictions_ClientId",
                table: "ClientIdPRestrictions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPostLogoutRedirectUris_ClientId",
                table: "ClientPostLogoutRedirectUris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProperties_ClientId",
                table: "ClientProperties",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRedirectUris_ClientId",
                table: "ClientRedirectUris",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientId",
                table: "Clients",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientScopes_ClientId",
                table: "ClientScopes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSecrets_ClientId",
                table: "ClientSecrets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_DeleteUserId",
                table: "Contato",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_IdPassageiro",
                table: "Contato",
                column: "IdPassageiro");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_IdTaxista",
                table: "Contato",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_InsertUserId",
                table: "Contato",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_UpdateUserId",
                table: "Contato",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_DeleteUserId",
                table: "Contrato",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_InsertUserId",
                table: "Contrato",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_UpdateUserId",
                table: "Contrato",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_DeleteUserId",
                table: "Corrida",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdFaturamentoTaxista",
                table: "Corrida",
                column: "IdFaturamentoTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdRotaExecutada",
                table: "Corrida",
                column: "IdRotaExecutada");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdSolicitacao",
                table: "Corrida",
                column: "IdSolicitacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdTarifa",
                table: "Corrida",
                column: "IdTarifa");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdTaxista",
                table: "Corrida",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdVeiculo",
                table: "Corrida",
                column: "IdVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_InsertUserId",
                table: "Corrida",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_UpdateUserId",
                table: "Corrida",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorVeiculo_DeleteUserId",
                table: "CorVeiculo",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorVeiculo_InsertUserId",
                table: "CorVeiculo",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorVeiculo_UpdateUserId",
                table: "CorVeiculo",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_DeleteUserId",
                table: "Emergencias",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_InsertUserId",
                table: "Emergencias",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_TaxistaId",
                table: "Emergencias",
                column: "TaxistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_UpdateUserId",
                table: "Emergencias",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_DeleteUserId",
                table: "Endereco",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_IdLocalizacao",
                table: "Endereco",
                column: "IdLocalizacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_InsertUserId",
                table: "Endereco",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_UpdateUserId",
                table: "Endereco",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDesconto_DeleteUserId",
                table: "FaixaDesconto",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDesconto_InsertUserId",
                table: "FaixaDesconto",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDesconto_UpdateUserId",
                table: "FaixaDesconto",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDescontoTaxista_DeleteUserId",
                table: "FaixaDescontoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDescontoTaxista_IdFaixaDesconto",
                table: "FaixaDescontoTaxista",
                column: "IdFaixaDesconto");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDescontoTaxista_IdTaxista",
                table: "FaixaDescontoTaxista",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDescontoTaxista_InsertUserId",
                table: "FaixaDescontoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDescontoTaxista_UpdateUserId",
                table: "FaixaDescontoTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturamento_DeleteUserId",
                table: "Faturamento",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturamento_InsertUserId",
                table: "Faturamento",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturamento_UpdateUserId",
                table: "Faturamento",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_DeleteUserId",
                table: "FaturamentoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_FaturamentoId",
                table: "FaturamentoTaxista",
                column: "FaturamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_IdTaxista",
                table: "FaturamentoTaxista",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_InsertUserId",
                table: "FaturamentoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_UpdateUserId",
                table: "FaturamentoTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_DeleteUserId",
                table: "Favorito",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_IdPassageiro",
                table: "Favorito",
                column: "IdPassageiro");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_IdTaxista",
                table: "Favorito",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_InsertUserId",
                table: "Favorito",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_UpdateUserId",
                table: "Favorito",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_DeleteUserId",
                table: "FormaPagamento",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_InsertUserId",
                table: "FormaPagamento",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_UpdateUserId",
                table: "FormaPagamento",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_DeleteUserId",
                table: "FormaPagamentoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_IdFormaPagamento",
                table: "FormaPagamentoTaxista",
                column: "IdFormaPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_IdTaxista",
                table: "FormaPagamentoTaxista",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_InsertUserId",
                table: "FormaPagamentoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_UpdateUserId",
                table: "FormaPagamentoTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_DeleteUserId",
                table: "Foto",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_InsertUserId",
                table: "Foto",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_UpdateUserId",
                table: "Foto",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_DeleteUserId",
                table: "GrupoUsuario",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_InsertUserId",
                table: "GrupoUsuario",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_UpdateUserId",
                table: "GrupoUsuario",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityClaims_IdentityResourceId",
                table: "IdentityClaims",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityProperties_IdentityResourceId",
                table: "IdentityProperties",
                column: "IdentityResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityResources_Name",
                table: "IdentityResources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_DeleteUserId",
                table: "Localizacao",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_IdUsuario",
                table: "Localizacao",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_InsertUserId",
                table: "Localizacao",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_UpdateUserId",
                table: "Localizacao",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_DeleteUserId",
                table: "Mensagem",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_IdRemetente",
                table: "Mensagem",
                column: "IdRemetente");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_InsertUserId",
                table: "Mensagem",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_UpdateUserId",
                table: "Mensagem",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_DeleteUserId",
                table: "MensagemDestinatario",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_IdGrupoUsuario",
                table: "MensagemDestinatario",
                column: "IdGrupoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_IdMensagem",
                table: "MensagemDestinatario",
                column: "IdMensagem");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_IdUsuario",
                table: "MensagemDestinatario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_InsertUserId",
                table: "MensagemDestinatario",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_UpdateUserId",
                table: "MensagemDestinatario",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_DeleteUserId",
                table: "Passageiro",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_IdEndereco",
                table: "Passageiro",
                column: "IdEndereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_IdFoto",
                table: "Passageiro",
                column: "IdFoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_IdLocalizacaoAtual",
                table: "Passageiro",
                column: "IdLocalizacaoAtual",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_IdUsuario",
                table: "Passageiro",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_InsertUserId",
                table: "Passageiro",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_UpdateUserId",
                table: "Passageiro",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PontoTaxi_DeleteUserId",
                table: "PontoTaxi",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoTaxi_IdEndereco",
                table: "PontoTaxi",
                column: "IdEndereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PontoTaxi_InsertUserId",
                table: "PontoTaxi",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoTaxi_UpdateUserId",
                table: "PontoTaxi",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rota_DeleteUserId",
                table: "Rota",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_InsertUserId",
                table: "Rota",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_UpdateUserId",
                table: "Rota",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_DeleteUserId",
                table: "SolicitacaoCorrida",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_IdFaixaDesconto",
                table: "SolicitacaoCorrida",
                column: "IdFaixaDesconto");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_IdFormaPagamento",
                table: "SolicitacaoCorrida",
                column: "IdFormaPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_IdLocalizacaoDestino",
                table: "SolicitacaoCorrida",
                column: "IdLocalizacaoDestino");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_IdLocalizacaoOrigem",
                table: "SolicitacaoCorrida",
                column: "IdLocalizacaoOrigem");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_IdPassageiro",
                table: "SolicitacaoCorrida",
                column: "IdPassageiro");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_IdRota",
                table: "SolicitacaoCorrida",
                column: "IdRota");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_InsertUserId",
                table: "SolicitacaoCorrida",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_UpdateUserId",
                table: "SolicitacaoCorrida",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_DeleteUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_IdSolicitacaoCorrida",
                table: "SolicitacaoCorridaTaxista",
                column: "IdSolicitacaoCorrida");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_IdTaxista",
                table: "SolicitacaoCorridaTaxista",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_InsertUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_UpdateUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_DeleteUserId",
                table: "Tarifa",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_InsertUserId",
                table: "Tarifa",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_UpdateUserId",
                table: "Tarifa",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_DeleteUserId",
                table: "Taxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_IdEndereco",
                table: "Taxista",
                column: "IdEndereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_IdFoto",
                table: "Taxista",
                column: "IdFoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_IdLocalizacaoAtual",
                table: "Taxista",
                column: "IdLocalizacaoAtual",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_IdPontoTaxi",
                table: "Taxista",
                column: "IdPontoTaxi");

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_IdUsuario",
                table: "Taxista",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_InsertUserId",
                table: "Taxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_UpdateUserId",
                table: "Taxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_DeleteUserId",
                table: "UsuarioGrupoUsuario",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_IdGrupoUsuario",
                table: "UsuarioGrupoUsuario",
                column: "IdGrupoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_IdUsuario",
                table: "UsuarioGrupoUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_InsertUserId",
                table: "UsuarioGrupoUsuario",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_UpdateUserId",
                table: "UsuarioGrupoUsuario",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_DeleteUserId",
                table: "Veiculo",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdCorVeiculo",
                table: "Veiculo",
                column: "IdCorVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdFoto",
                table: "Veiculo",
                column: "IdFoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_InsertUserId",
                table: "Veiculo",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_UpdateUserId",
                table: "Veiculo",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_DeleteUserId",
                table: "VeiculoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_IdTaxista",
                table: "VeiculoTaxista",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_IdVeiculo",
                table: "VeiculoTaxista",
                column: "IdVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_InsertUserId",
                table: "VeiculoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_UpdateUserId",
                table: "VeiculoTaxista",
                column: "UpdateUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiClaims");

            migrationBuilder.DropTable(
                name: "ApiProperties");

            migrationBuilder.DropTable(
                name: "ApiScopeClaims");

            migrationBuilder.DropTable(
                name: "ApiSecrets");

            migrationBuilder.DropTable(
                name: "ClientClaims");

            migrationBuilder.DropTable(
                name: "ClientCorsOrigins");

            migrationBuilder.DropTable(
                name: "ClientGrantTypes");

            migrationBuilder.DropTable(
                name: "ClientIdPRestrictions");

            migrationBuilder.DropTable(
                name: "ClientPostLogoutRedirectUris");

            migrationBuilder.DropTable(
                name: "ClientProperties");

            migrationBuilder.DropTable(
                name: "ClientRedirectUris");

            migrationBuilder.DropTable(
                name: "ClientScopes");

            migrationBuilder.DropTable(
                name: "ClientSecrets");

            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "Corrida");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Emergencias");

            migrationBuilder.DropTable(
                name: "FaixaDescontoTaxista");

            migrationBuilder.DropTable(
                name: "Favorito");

            migrationBuilder.DropTable(
                name: "FormaPagamentoTaxista");

            migrationBuilder.DropTable(
                name: "IdentityClaims");

            migrationBuilder.DropTable(
                name: "IdentityProperties");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "MensagemDestinatario");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "UsuarioGrupoUsuario");

            migrationBuilder.DropTable(
                name: "VeiculoTaxista");

            migrationBuilder.DropTable(
                name: "ApiScopes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "FaturamentoTaxista");

            migrationBuilder.DropTable(
                name: "Tarifa");

            migrationBuilder.DropTable(
                name: "IdentityResources");

            migrationBuilder.DropTable(
                name: "Mensagem");

            migrationBuilder.DropTable(
                name: "SolicitacaoCorrida");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "GrupoUsuario");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "ApiResources");

            migrationBuilder.DropTable(
                name: "Faturamento");

            migrationBuilder.DropTable(
                name: "Taxista");

            migrationBuilder.DropTable(
                name: "FaixaDesconto");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "Passageiro");

            migrationBuilder.DropTable(
                name: "Rota");

            migrationBuilder.DropTable(
                name: "CorVeiculo");

            migrationBuilder.DropTable(
                name: "PontoTaxi");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Localizacao");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
