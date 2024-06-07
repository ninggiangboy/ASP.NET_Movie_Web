IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
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

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Films] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    [OtherTitle] nvarchar(100) NULL,
    [Slug] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NULL,
    [TrailerUrl] nvarchar(255) NULL,
    [PosterUrl] nvarchar(255) NULL,
    [ThumbnailUrl] nvarchar(255) NULL,
    [Duration] int NULL,
    [AverageRating] decimal(18,2) NULL,
    [TotalEpisode] int NULL,
    [DurationPerEpisode] int NULL,
    [VideoUrl] nvarchar(255) NULL,
    [Type] int NOT NULL,
    [Actor] nvarchar(max) NULL,
    [Director] nvarchar(max) NULL,
    [TotalView] int NOT NULL,
    [ReleaseYear] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CountryId] int NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Films_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id])
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [FilmId] int NULL,
    [Content] nvarchar(max) NOT NULL,
    [Time] datetime2 NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id])
);
GO

CREATE TABLE [Episodes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(100) NOT NULL,
    [Slug] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NULL,
    [ThumbnailUrl] nvarchar(255) NULL,
    [VideoUrl] nvarchar(255) NOT NULL,
    [View] int NOT NULL,
    [Duration] int NULL,
    [CreatedAt] datetime2 NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_Episodes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Episodes_Films_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FilmGenre] (
    [FilmsId] int NOT NULL,
    [GenresId] int NOT NULL,
    CONSTRAINT [PK_FilmGenre] PRIMARY KEY ([FilmsId], [GenresId]),
    CONSTRAINT [FK_FilmGenre_Films_FilmsId] FOREIGN KEY ([FilmsId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmGenre_Genres_GenresId] FOREIGN KEY ([GenresId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FilmUser] (
    [FavoriteFilmsId] int NOT NULL,
    [FollowersId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_FilmUser] PRIMARY KEY ([FavoriteFilmsId], [FollowersId]),
    CONSTRAINT [FK_FilmUser_AspNetUsers_FollowersId] FOREIGN KEY ([FollowersId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmUser_Films_FavoriteFilmsId] FOREIGN KEY ([FavoriteFilmsId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Rating] (
    [Id] int NOT NULL IDENTITY,
    [FilmId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Rate] int NOT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rating_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rating_Films_FilmId] FOREIGN KEY ([FilmId]) REFERENCES [Films] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_Comments_FilmId] ON [Comments] ([FilmId]);
GO

CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
GO

CREATE INDEX [IX_Episodes_MovieId] ON [Episodes] ([MovieId]);
GO

CREATE INDEX [IX_FilmGenre_GenresId] ON [FilmGenre] ([GenresId]);
GO

CREATE INDEX [IX_Films_CountryId] ON [Films] ([CountryId]);
GO

CREATE INDEX [IX_FilmUser_FollowersId] ON [FilmUser] ([FollowersId]);
GO

CREATE INDEX [IX_Rating_FilmId] ON [Rating] ([FilmId]);
GO

CREATE INDEX [IX_Rating_UserId] ON [Rating] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240526125656_Initial', N'6.0.30');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [Balance] decimal(18,2) NULL;
GO

CREATE TABLE [Transactions] (
    [Id] nvarchar(450) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Transactions_UserId] ON [Transactions] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240602042954_Add_Transaction_Migrations', N'6.0.30');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Discriminator');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AspNetUsers] DROP COLUMN [Discriminator];
GO

ALTER TABLE [Transactions] ADD [Status] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Transactions] ADD [TransactionReference] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Transactions] ADD [Type] int NOT NULL DEFAULT 0;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Balance');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [Balance] decimal(18,2) NOT NULL;
ALTER TABLE [AspNetUsers] ADD DEFAULT 0.0 FOR [Balance];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240603074150_Update_Migrations', N'6.0.30');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Genres] ADD [Image] nvarchar(255) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Countries] ADD [Image] nvarchar(255) NOT NULL DEFAULT N'';
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'Time');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Comments] ALTER COLUMN [Time] datetime2 NOT NULL;
ALTER TABLE [Comments] ADD DEFAULT '0001-01-01T00:00:00.0000000' FOR [Time];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240605064553_add_image', N'6.0.30');
GO

COMMIT;
GO

