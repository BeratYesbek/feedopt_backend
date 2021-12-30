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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [AdoptionNoticeImages] (
        [Id] int NOT NULL IDENTITY,
        [ImagePath] nvarchar(max) NULL,
        [AdoptionNoticeId] int NOT NULL,
        CONSTRAINT [PK_AdoptionNoticeImages] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [AdoptionNotices] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [UserId] int NOT NULL,
        [LocationId] int NOT NULL,
        [AnimalSpeciesId] int NOT NULL,
        CONSTRAINT [PK_AdoptionNotices] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [AnimalCategories] (
        [Id] int NOT NULL IDENTITY,
        [AnimalCategoryName] nvarchar(max) NULL,
        CONSTRAINT [PK_AnimalCategories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [AnimalSpecies] (
        [Id] int NOT NULL IDENTITY,
        [Kind] nvarchar(max) NULL,
        [AnimalCategoryId] int NOT NULL,
        CONSTRAINT [PK_AnimalSpecies] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [Chats] (
        [Id] int NOT NULL IDENTITY,
        [SenderId] int NOT NULL,
        [ReceiverId] int NOT NULL,
        [Message] nvarchar(max) NULL,
        [IsSeen] bit NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_Chats] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [Locations] (
        [Id] int NOT NULL IDENTITY,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [PlaceId] nvarchar(max) NULL,
        [Latitude] nvarchar(max) NULL,
        [Longitude] nvarchar(max) NULL,
        CONSTRAINT [PK_Locations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [MissingDeclarationImages] (
        [Id] int NOT NULL IDENTITY,
        [ImagePath] nvarchar(max) NULL,
        [MissingDeclarationId] int NOT NULL,
        CONSTRAINT [PK_MissingDeclarationImages] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [MissingDeclarations] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [AnimalSpeciesId] int NOT NULL,
        [UserId] int NOT NULL,
        [LocationId] int NOT NULL,
        CONSTRAINT [PK_MissingDeclarations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [OperationClaims] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_OperationClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [UserOperationClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [OperationClaimId] int NOT NULL,
        CONSTRAINT [PK_UserOperationClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [PasswordHash] varbinary(max) NULL,
        [PasswordSalt] varbinary(max) NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211207191119_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211207191119_InitialCreate', N'5.0.1');
END;
GO

COMMIT;
GO

