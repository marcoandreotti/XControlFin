PRAGMA foreign_keys = ON;

-- Create Tables

CREATE TABLE IF NOT EXISTS "Users" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "RefreshToken" TEXT NULL,
    "RefreshTokenExpiryTime" TEXT NULL,
    "Image" TEXT NULL,
    "Active" INTEGER NOT NULL,
    "Created" TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "Updated" TEXT NULL
);

CREATE TABLE IF NOT EXISTS "CostCenters" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NULL,
    "Sequence" INTEGER NOT NULL DEFAULT 1,
    "Image" TEXT NULL,
    "IsActive" INTEGER NOT NULL DEFAULT 1,
    "Created" TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "Updated" TEXT NULL
);

CREATE TABLE IF NOT EXISTS "FinancialInstitutions" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NULL,
    "Sequence" INTEGER NOT NULL DEFAULT 1,
    "IsActive" INTEGER NOT NULL DEFAULT 1,
    "Created" TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "Updated" TEXT NULL
);

CREATE TABLE IF NOT EXISTS "UserFinancialInstitutions" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "UserId" INTEGER NOT NULL,
    "FinancialInstitutionId" INTEGER NOT NULL,
    "Created" TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "Updated" TEXT NULL,
    CONSTRAINT "FK_UserFinancialInstitutions_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_UserFinancialInstitutions_FinancialInstitutions_FinancialInstitutionId" FOREIGN KEY ("FinancialInstitutionId") REFERENCES "FinancialInstitutions" ("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "FinancialPlannings" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "CostCenterId" INTEGER NOT NULL,
    "FinancialInstitutionId" INTEGER NOT NULL,
    "StartDate" TEXT NOT NULL,
    "LastStartDate" TEXT NOT NULL,
    "EndDate" TEXT NULL,
    "TimeInterval" INTEGER NOT NULL,
    "InitialPaymentDay" INTEGER NOT NULL DEFAULT 1,
    "Historic" TEXT NOT NULL,
    "StartParcel" INTEGER NOT NULL DEFAULT 1,
    "TotalParcel" INTEGER NOT NULL DEFAULT 1,
    "Grouper" INTEGER NULL,
    "Value" NUMERIC(18,2) NOT NULL,
    "IsActive" INTEGER NOT NULL DEFAULT 1,
    "Created" TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "Updated" TEXT NULL,
    CONSTRAINT "FK_FinancialPlannings_CostCenters_CostCenterId" FOREIGN KEY ("CostCenterId") REFERENCES "CostCenters" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FinancialPlannings_FinancialInstitutions_FinancialInstitutionId" FOREIGN KEY ("FinancialInstitutionId") REFERENCES "FinancialInstitutions" ("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "FinancialReleases" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "CostCenterId" INTEGER NOT NULL,
    "FinancialInstitutionId" INTEGER NOT NULL,
    "FinancialPlanningId" INTEGER NULL,
    "PaymentDate" TEXT NOT NULL,
    "CompensationDate" TEXT NOT NULL,
    "ScheduledDate" TEXT NULL,
    "Historic" TEXT NOT NULL,
    "Parcel" INTEGER NOT NULL,
    "TotalParcel" INTEGER NOT NULL,
    "Grouper" INTEGER NULL,
    "Value" NUMERIC(18,2) NOT NULL,
    "Realized" INTEGER NOT NULL,
    "Created" TEXT NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "Updated" TEXT NULL,
    CONSTRAINT "FK_FinancialReleases_CostCenters_CostCenterId" FOREIGN KEY ("CostCenterId") REFERENCES "CostCenters" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FinancialReleases_FinancialInstitutions_FinancialInstitutionId" FOREIGN KEY ("FinancialInstitutionId") REFERENCES "FinancialInstitutions" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FinancialReleases_FinancialPlannings_FinancialPlanningId" FOREIGN KEY ("FinancialPlanningId") REFERENCES "FinancialPlannings" ("Id") ON DELETE RESTRICT
);

-- Initial Seed Data

-- Admin User (Password: admin123 - BCrypt Hash)
INSERT INTO "Users" ("Name", "Email", "Password", "Active", "Created")
VALUES ('Administrator', 'admin@xcontrol.com', '$2a$11$CkYzAG8.337cQNnCJt3qPel.Q7midduo/LlEaURu8ppNngwny/rPq', 1, CURRENT_TIMESTAMP);

-- Initial Categories
INSERT INTO "CostCenters" ("Name", "Description", "Sequence", "IsActive")
VALUES 
('Receitas', 'Entradas de recursos', 1, 1),
('Despesas', 'Saídas de recursos', 2, 1);

-- Initial Institution
INSERT INTO "FinancialInstitutions" ("Name", "Description", "Sequence", "IsActive")
VALUES ('Caixa Principal', 'Dinheiro em espécie', 1, 1);

-- Initial User Financial Institution
INSERT INTO "UserFinancialInstitutions" ("UserId", "FinancialInstitutionId", "Created")
VALUES (1, 1, CURRENT_TIMESTAMP);
