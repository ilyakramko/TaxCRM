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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240408131126_Initial'
)
BEGIN
    CREATE TABLE [Entrepreneurs] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
        [LastName] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
        CONSTRAINT [PK_Entrepreneurs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240408131126_Initial'
)
BEGIN
    CREATE TABLE [EntrepreneurProfiles] (
        [Id] uniqueidentifier NOT NULL,
        [TaxPayerNumber] nvarchar(30) NOT NULL,
        [Country] int NOT NULL,
        [EntrepreneurId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_EntrepreneurProfiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_EntrepreneurProfiles_Entrepreneurs_EntrepreneurId] FOREIGN KEY ([EntrepreneurId]) REFERENCES [Entrepreneurs] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240408131126_Initial'
)
BEGIN
    CREATE TABLE [Incomes] (
        [Id] uniqueidentifier NOT NULL,
        [Value_Amount] decimal(12,2) NOT NULL,
        [Value_Currency] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [EntrepreneurProfileId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Incomes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Incomes_EntrepreneurProfiles_EntrepreneurProfileId] FOREIGN KEY ([EntrepreneurProfileId]) REFERENCES [EntrepreneurProfiles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240408131126_Initial'
)
BEGIN
    CREATE INDEX [IX_EntrepreneurProfiles_EntrepreneurId] ON [EntrepreneurProfiles] ([EntrepreneurId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240408131126_Initial'
)
BEGIN
    CREATE INDEX [IX_Incomes_EntrepreneurProfileId] ON [Incomes] ([EntrepreneurProfileId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240408131126_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240408131126_Initial', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240719132023_AddEmailField'
)
BEGIN
    ALTER TABLE [Entrepreneurs] ADD [Email] nvarchar(100) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240719132023_AddEmailField'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240719132023_AddEmailField', N'8.0.3');
END;
GO

COMMIT;
GO

