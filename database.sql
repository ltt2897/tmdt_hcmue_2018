IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Email] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles] ([UserId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'2.0.0-rtm-26452');

GO

DROP INDEX [UserNameIndex] ON [AspNetUsers];

GO

DROP INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles];

GO

DROP INDEX [RoleNameIndex] ON [AspNetRoles];

GO

ALTER TABLE [AspNetUsers] ADD [DiaChi] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [GioiTinh] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [AspNetUsers] ADD [HoTen] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [NgaySinh] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';

GO

ALTER TABLE [AspNetUsers] ADD [SoDienThoai] nvarchar(max) NULL;

GO

CREATE TABLE [ChuDe] (
    [Id] int NOT NULL IDENTITY,
    [TenChuDe] nvarchar(max) NULL,
    CONSTRAINT [PK_ChuDe] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [DanhMuc] (
    [Id] int NOT NULL IDENTITY,
    [TenDanhMuc] nvarchar(max) NULL,
    CONSTRAINT [PK_DanhMuc] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [HoaDon] (
    [Id] int NOT NULL IDENTITY,
    [NgayLapHoaDon] datetime2 NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_HoaDon] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HoaDon_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [NhaXuatBan] (
    [Id] int NOT NULL IDENTITY,
    [TenNhaXuatBan] nvarchar(max) NULL,
    CONSTRAINT [PK_NhaXuatBan] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TacGia] (
    [Id] int NOT NULL IDENTITY,
    [TenTacGia] nvarchar(max) NULL,
    CONSTRAINT [PK_TacGia] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Sach] (
    [id] int NOT NULL IDENTITY,
    [ChieuDai] int NOT NULL,
    [ChieuRong] int NOT NULL,
    [ChuDeId] int NULL,
    [DanhMucId] int NULL,
    [DinhDang] nvarchar(100) NULL,
    [DonGia] int NOT NULL,
    [HinhAnh] nvarchar(200) NULL,
    [NhaXuatBanId] int NULL,
    [PhanTramGiamGia] int NOT NULL,
    [SoLuong] int NOT NULL,
    [SoTrang] int NOT NULL,
    [TacGiaId] int NULL,
    [TenSach] nvarchar(50) NULL,
    [TomTat] nvarchar(max) NULL,
    CONSTRAINT [PK_Sach] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Sach_ChuDe_ChuDeId] FOREIGN KEY ([ChuDeId]) REFERENCES [ChuDe] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sach_DanhMuc_DanhMucId] FOREIGN KEY ([DanhMucId]) REFERENCES [DanhMuc] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sach_NhaXuatBan_NhaXuatBanId] FOREIGN KEY ([NhaXuatBanId]) REFERENCES [NhaXuatBan] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sach_TacGia_TacGiaId] FOREIGN KEY ([TacGiaId]) REFERENCES [TacGia] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ChiTietHoaDon] (
    [HoaDonId] int NOT NULL,
    [SachId] int NOT NULL,
    [NgayThem] datetime2 NOT NULL,
    CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY ([HoaDonId], [SachId]),
    CONSTRAINT [FK_ChiTietHoaDon_HoaDon_HoaDonId] FOREIGN KEY ([HoaDonId]) REFERENCES [HoaDon] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChiTietHoaDon_Sach_SachId] FOREIGN KEY ([SachId]) REFERENCES [Sach] ([id]) ON DELETE CASCADE
);

GO

CREATE TABLE [NhanXet] (
    [Id] int NOT NULL IDENTITY,
    [NoiDung] nvarchar(max) NULL,
    [Sachid] int NULL,
    [TieuDe] nvarchar(50) NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_NhanXet] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NhanXet_Sach_Sachid] FOREIGN KEY ([Sachid]) REFERENCES [Sach] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_NhanXet_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Wishlist] (
    [SachID] int NOT NULL,
    [UserID] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Wishlist] PRIMARY KEY ([SachID], [UserID]),
    CONSTRAINT [FK_Wishlist_Sach_SachID] FOREIGN KEY ([SachID]) REFERENCES [Sach] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wishlist_AspNetUsers_UserID] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_ChiTietHoaDon_SachId] ON [ChiTietHoaDon] ([SachId]);

GO

CREATE INDEX [IX_HoaDon_UserId] ON [HoaDon] ([UserId]);

GO

CREATE INDEX [IX_NhanXet_Sachid] ON [NhanXet] ([Sachid]);

GO

CREATE INDEX [IX_NhanXet_UserId] ON [NhanXet] ([UserId]);

GO

CREATE INDEX [IX_Sach_ChuDeId] ON [Sach] ([ChuDeId]);

GO

CREATE INDEX [IX_Sach_DanhMucId] ON [Sach] ([DanhMucId]);

GO

CREATE INDEX [IX_Sach_NhaXuatBanId] ON [Sach] ([NhaXuatBanId]);

GO

CREATE INDEX [IX_Sach_TacGiaId] ON [Sach] ([TacGiaId]);

GO

CREATE INDEX [IX_Wishlist_UserID] ON [Wishlist] ([UserID]);

GO

ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171127033557_CreateDatabase', N'2.0.0-rtm-26452');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Sach') AND [c].[name] = N'TenSach');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Sach] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Sach] ALTER COLUMN [TenSach] nvarchar(255) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171202060848_AddTableLienLac', N'2.0.0-rtm-26452');

GO

CREATE TABLE [TraLoiLienLac] (
    [Id] int NOT NULL IDENTITY,
    [NoiDung] nvarchar(max) NULL,
    [TieuDe] nvarchar(max) NULL,
    CONSTRAINT [PK_TraLoiLienLac] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [LienLac] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(50) NULL,
    [NoiDung] nvarchar(max) NULL,
    [Ten] nvarchar(50) NULL,
    [TieuDe] nvarchar(200) NULL,
    [traLoiLienLacId] int NULL,
    CONSTRAINT [PK_LienLac] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LienLac_TraLoiLienLac_traLoiLienLacId] FOREIGN KEY ([traLoiLienLacId]) REFERENCES [TraLoiLienLac] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_LienLac_traLoiLienLacId] ON [LienLac] ([traLoiLienLacId]) WHERE [traLoiLienLacId] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171202071224_AddTraLoiLienLacTable', N'2.0.0-rtm-26452');

GO

ALTER TABLE [LienLac] DROP CONSTRAINT [FK_LienLac_TraLoiLienLac_traLoiLienLacId];

GO

DROP INDEX [IX_LienLac_traLoiLienLacId] ON [LienLac];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'LienLac') AND [c].[name] = N'traLoiLienLacId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [LienLac] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [LienLac] DROP COLUMN [traLoiLienLacId];

GO

ALTER TABLE [TraLoiLienLac] ADD [LienLacId] int NULL;

GO

ALTER TABLE [LienLac] ADD [NgayGoi] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';

GO

CREATE INDEX [IX_TraLoiLienLac_LienLacId] ON [TraLoiLienLac] ([LienLacId]);

GO

ALTER TABLE [TraLoiLienLac] ADD CONSTRAINT [FK_TraLoiLienLac_LienLac_LienLacId] FOREIGN KEY ([LienLacId]) REFERENCES [LienLac] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171202090109_UpdateLienLacVaTraLoiLienLac', N'2.0.0-rtm-26452');

GO

ALTER TABLE [TraLoiLienLac] ADD [NgayTraLoi] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171202090401_UpdateTraLoiLienLac', N'2.0.0-rtm-26452');

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'SoDienThoai');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AspNetUsers] DROP COLUMN [SoDienThoai];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171204064813_DeleteSoDienThoaiColumnApplicationUsers', N'2.0.0-rtm-26452');

GO

ALTER TABLE [NhanXet] ADD [NgayDang] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171205150055_AddNgayDangToNhanXetTable', N'2.0.0-rtm-26452');

GO

ALTER TABLE [HoaDon] ADD [DiaChi] nvarchar(50) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171207015248_ThemDiaChiVaoHoaDon', N'2.0.0-rtm-26452');

GO

ALTER TABLE [HoaDon] ADD [GhiChu] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171207033434_ThemCotGhiChuVaoHoaDon', N'2.0.0-rtm-26452');

GO

ALTER TABLE [HoaDon] ADD [TrangThai] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171207083654_ThemTrangThaiVaoHoaDon', N'2.0.0-rtm-26452');

GO

ALTER TABLE [ChiTietHoaDon] ADD [SoLuong] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171208074838_ThemSoLuongVaoChiTietHoaDon', N'2.0.0-rtm-26452');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171208081545_HoaDonVaChiTietHoaDon', N'2.0.0-rtm-26452');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171222104117_ModifyHoaDonTable', N'2.0.0-rtm-26452');

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'HoaDon') AND [c].[name] = N'TrangThai');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [HoaDon] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [HoaDon] DROP COLUMN [TrangThai];

GO

ALTER TABLE [HoaDon] ADD [NgayGiao] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171224030924_ThemNgayGiaoVaoHoaDon', N'2.0.0-rtm-26452');

GO

ALTER TABLE [HoaDon] ADD [TongThanhTien] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171225011729_ThemTongThanhTienVaoHoaDon', N'2.0.0-rtm-26452');

GO

ALTER TABLE [ChiTietHoaDon] ADD [ThanhTien] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20171225013637_ThemThanhTienVaoChiTietHoaDon', N'2.0.0-rtm-26452');

GO

ALTER TABLE [Sach] DROP CONSTRAINT [FK_Sach_ChuDe_ChuDeId];

GO

ALTER TABLE [Sach] DROP CONSTRAINT [FK_Sach_DanhMuc_DanhMucId];

GO

ALTER TABLE [Sach] DROP CONSTRAINT [FK_Sach_NhaXuatBan_NhaXuatBanId];

GO

ALTER TABLE [Sach] DROP CONSTRAINT [FK_Sach_TacGia_TacGiaId];

GO

DROP INDEX [IX_Sach_TacGiaId] ON [Sach];
DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Sach') AND [c].[name] = N'TacGiaId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Sach] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Sach] ALTER COLUMN [TacGiaId] int NOT NULL;
CREATE INDEX [IX_Sach_TacGiaId] ON [Sach] ([TacGiaId]);

GO

DROP INDEX [IX_Sach_NhaXuatBanId] ON [Sach];
DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Sach') AND [c].[name] = N'NhaXuatBanId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Sach] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Sach] ALTER COLUMN [NhaXuatBanId] int NOT NULL;
CREATE INDEX [IX_Sach_NhaXuatBanId] ON [Sach] ([NhaXuatBanId]);

GO

DROP INDEX [IX_Sach_DanhMucId] ON [Sach];
DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Sach') AND [c].[name] = N'DanhMucId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Sach] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Sach] ALTER COLUMN [DanhMucId] int NOT NULL;
CREATE INDEX [IX_Sach_DanhMucId] ON [Sach] ([DanhMucId]);

GO

DROP INDEX [IX_Sach_ChuDeId] ON [Sach];
DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Sach') AND [c].[name] = N'ChuDeId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Sach] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Sach] ALTER COLUMN [ChuDeId] int NOT NULL;
CREATE INDEX [IX_Sach_ChuDeId] ON [Sach] ([ChuDeId]);

GO

ALTER TABLE [NhaXuatBan] ADD [SoDienThoai] nvarchar(max) NULL;

GO

ALTER TABLE [Sach] ADD CONSTRAINT [FK_Sach_ChuDe_ChuDeId] FOREIGN KEY ([ChuDeId]) REFERENCES [ChuDe] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Sach] ADD CONSTRAINT [FK_Sach_DanhMuc_DanhMucId] FOREIGN KEY ([DanhMucId]) REFERENCES [DanhMuc] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Sach] ADD CONSTRAINT [FK_Sach_NhaXuatBan_NhaXuatBanId] FOREIGN KEY ([NhaXuatBanId]) REFERENCES [NhaXuatBan] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Sach] ADD CONSTRAINT [FK_Sach_TacGia_TacGiaId] FOREIGN KEY ([TacGiaId]) REFERENCES [TacGia] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180223140659_ThemSoDienThoaiVaoXuatBan', N'2.0.0-rtm-26452');

GO

