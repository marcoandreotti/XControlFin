-- Create Tables

CREATE TABLE [Users] (
    [Id] AUTOINCREMENT CONSTRAINT [PK_Users] PRIMARY KEY,
    [Name] VARCHAR(255) NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [Password] VARCHAR(255) NOT NULL,
    [RefreshToken] VARCHAR(255) NULL,
    [RefreshTokenExpiryTime] DATETIME NULL,
    [Image] MEMO NULL,
    [Active] BIT NOT NULL DEFAULT True,
    [Created] DATETIME NOT NULL DEFAULT Now(),
    [Updated] DATETIME NULL
);

CREATE TABLE [CostCenters] (
    [Id] AUTOINCREMENT CONSTRAINT [PK_CostCenters] PRIMARY KEY,
    [Name] VARCHAR(255) NOT NULL,
    [Description] MEMO NULL,
    [Sequence] INTEGER NOT NULL DEFAULT 1,
    [Image] VARCHAR(255) NULL,
    [IsActive] BIT NOT NULL DEFAULT True,
    [Created] DATETIME NOT NULL DEFAULT Now(),
    [Updated] DATETIME NULL
);

CREATE TABLE [FinancialInstitutions] (
    [Id] AUTOINCREMENT CONSTRAINT [PK_FinancialInstitutions] PRIMARY KEY,
    [Name] VARCHAR(255) NOT NULL,
    [Description] MEMO NULL,
    [Sequence] INTEGER NOT NULL DEFAULT 1,
    [IsActive] BIT NOT NULL DEFAULT True,
    [Created] DATETIME NOT NULL DEFAULT Now(),
    [Updated] DATETIME NULL
);

CREATE TABLE [UserFinancialInstitutions] (
    [Id] AUTOINCREMENT CONSTRAINT [PK_UserFinancialInstitutions] PRIMARY KEY,
    [UserId] INTEGER NOT NULL,
    [FinancialInstitutionId] INTEGER NOT NULL,
    [Created] DATETIME NOT NULL DEFAULT Now(),
    [Updated] DATETIME NULL,
    CONSTRAINT [FK_UserFinancialInstitutions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserFinancialInstitutions_FinancialInstitutions_FinancialInstitutionId] FOREIGN KEY ([FinancialInstitutionId]) REFERENCES [FinancialInstitutions] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [FinancialPlannings] (
    [Id] AUTOINCREMENT CONSTRAINT [PK_FinancialPlannings] PRIMARY KEY,
    [CostCenterId] INTEGER NOT NULL,
    [FinancialInstitutionId] INTEGER NOT NULL,
    [StartDate] DATETIME NOT NULL,
    [LastStartDate] DATETIME NOT NULL,
    [EndDate] DATETIME NULL,
    [TimeInterval] INTEGER NOT NULL,
    [InitialPaymentDay] INTEGER NOT NULL DEFAULT 1,
    [Historic] MEMO NOT NULL,
    [StartParcel] INTEGER NOT NULL DEFAULT 1,
    [TotalParcel] INTEGER NOT NULL DEFAULT 1,
    [Grouper] INTEGER NULL,
    [Value] DECIMAL(18,2) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT True,
    [Created] DATETIME NOT NULL DEFAULT Now(),
    [Updated] DATETIME NULL,
    CONSTRAINT [FK_FinancialPlannings_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [CostCenters] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FinancialPlannings_FinancialInstitutions_FinancialInstitutionId] FOREIGN KEY ([FinancialInstitutionId]) REFERENCES [FinancialInstitutions] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [FinancialReleases] (
    [Id] AUTOINCREMENT CONSTRAINT [PK_FinancialReleases] PRIMARY KEY,
    [CostCenterId] INTEGER NOT NULL,
    [FinancialInstitutionId] INTEGER NOT NULL,
    [FinancialPlanningId] INTEGER NULL,
    [PaymentDate] DATETIME NOT NULL,
    [CompensationDate] DATETIME NOT NULL,
    [ScheduledDate] DATETIME NULL,
    [Historic] MEMO NOT NULL,
    [Parcel] INTEGER NOT NULL,
    [TotalParcel] INTEGER NOT NULL,
    [Grouper] INTEGER NULL,
    [Value] DECIMAL(18,2) NOT NULL,
    [Realized] BIT NOT NULL DEFAULT False,
    [Created] DATETIME NOT NULL DEFAULT Now(),
    [Updated] DATETIME NULL,
    CONSTRAINT [FK_FinancialReleases_CostCenters_CostCenterId] FOREIGN KEY ([CostCenterId]) REFERENCES [CostCenters] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FinancialReleases_FinancialInstitutions_FinancialInstitutionId] FOREIGN KEY ([FinancialInstitutionId]) REFERENCES [FinancialInstitutions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FinancialReleases_FinancialPlannings_FinancialPlanningId] FOREIGN KEY ([FinancialPlanningId]) REFERENCES [FinancialPlannings] ([Id]) ON DELETE NO ACTION
);

-- Initial Seed Data

-- Admin User (Password: admin123 - BCrypt Hash)
INSERT INTO [Users] ([Name], [Email], [Password], [Active], [Created])
VALUES ('Administrator', 'admin@xcontrol.com', '$2a$11$CkYzAG8.337cQNnCJt3qPel.Q7midduo/LlEaURu8ppNngwny/rPq', True, Now());

-- Initial Categories
INSERT INTO [CostCenters] ([Name], [Description], [Sequence], [IsActive])
VALUES ('Receitas', 'Entradas de recursos', 1, True);

INSERT INTO [CostCenters] ([Name], [Description], [Sequence], [IsActive])
VALUES ('Despesas', 'Saídas de recursos', 2, True);

-- Initial Institution
INSERT INTO [FinancialInstitutions] ([Name], [Description], [Sequence], [IsActive])
VALUES ('Caixa Principal', 'Dinheiro em espécie', 1, True);

-- Initial User Financial Institution
INSERT INTO [UserFinancialInstitutions] ([UserId], [FinancialInstitutionId], [Created])
VALUES (1, 1, Now());
