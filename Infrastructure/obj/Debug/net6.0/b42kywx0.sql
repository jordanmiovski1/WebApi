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

CREATE TABLE [Company] (
    [CompanyId] int NOT NULL IDENTITY,
    [CompanyName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY ([CompanyId])
);
GO

CREATE TABLE [Country] (
    [CountryId] int NOT NULL IDENTITY,
    [CountryName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId])
);
GO

CREATE TABLE [Contact] (
    [ContactId] int NOT NULL IDENTITY,
    [ContactName] nvarchar(max) NOT NULL,
    [CompanyId] int NOT NULL,
    [CountryId] int NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY ([ContactId]),
    CONSTRAINT [FK_Contact_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([CompanyId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Contact_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Country] ([CountryId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Contact_CompanyId] ON [Contact] ([CompanyId]);
GO

CREATE INDEX [IX_Contact_CountryId] ON [Contact] ([CountryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231209135827_InitalContext', N'6.0.25');
GO

COMMIT;
GO

