-- Create Tables

CREATE TABLE IF NOT EXISTS "Users" (
    "Id" bigserial NOT NULL,
    "Name" text NOT NULL,
    "Email" text NOT NULL,
    "Password" text NOT NULL,
    "RefreshToken" text NULL,
    "RefreshTokenExpiryTime" timestamp with time zone NULL,
    "Image" text NULL,
    "Active" boolean NOT NULL,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    "Updated" timestamp with time zone NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "CostCenters" (
    "Id" bigserial NOT NULL,
    "Name" text NOT NULL,
    "Description" text NULL,
    "Sequence" integer NOT NULL DEFAULT 1,
    "Image" text NULL,
    "IsActive" boolean NOT NULL DEFAULT true,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    "Updated" timestamp with time zone NULL,
    CONSTRAINT "PK_CostCenters" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "FinancialInstitutions" (
    "Id" bigserial NOT NULL,
    "Name" text NOT NULL,
    "Description" text NULL,
    "Sequence" integer NOT NULL DEFAULT 1,
    "IsActive" boolean NOT NULL DEFAULT true,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    "Updated" timestamp with time zone NULL,
    CONSTRAINT "PK_FinancialInstitutions" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "UserFinancialInstitutions" (
    "Id" bigserial NOT NULL,
    "UserId" bigint NOT NULL,
    "FinancialInstitutionId" bigint NOT NULL,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    "Updated" timestamp with time zone NULL,
    CONSTRAINT "PK_UserFinancialInstitutions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_UserFinancialInstitutions_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_UserFinancialInstitutions_FinancialInstitutions_FinancialInstitutionId" FOREIGN KEY ("FinancialInstitutionId") REFERENCES "FinancialInstitutions" ("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "FinancialPlannings" (
    "Id" bigserial NOT NULL,
    "CostCenterId" bigint NOT NULL,
    "FinancialInstitutionId" bigint NOT NULL,
    "StartDate" timestamp with time zone NOT NULL,
    "LastStartDate" timestamp with time zone NOT NULL,
    "EndDate" timestamp with time zone NULL,
    "TimeInterval" integer NOT NULL,
    "InitialPaymentDay" integer NOT NULL DEFAULT 1,
    "Historic" text NOT NULL,
    "StartParcel" integer NOT NULL DEFAULT 1,
    "TotalParcel" integer NOT NULL DEFAULT 1,
    "Grouper" bigint NULL,
    "Value" numeric(18,2) NOT NULL,
    "IsActive" boolean NOT NULL DEFAULT true,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    "Updated" timestamp with time zone NULL,
    CONSTRAINT "PK_FinancialPlannings" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_FinancialPlannings_CostCenters_CostCenterId" FOREIGN KEY ("CostCenterId") REFERENCES "CostCenters" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FinancialPlannings_FinancialInstitutions_FinancialInstitutionId" FOREIGN KEY ("FinancialInstitutionId") REFERENCES "FinancialInstitutions" ("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "FinancialReleases" (
    "Id" bigserial NOT NULL,
    "CostCenterId" bigint NOT NULL,
    "FinancialInstitutionId" bigint NOT NULL,
    "FinancialPlanningId" bigint NULL,
    "PaymentDate" timestamp with time zone NOT NULL,
    "CompensationDate" timestamp with time zone NOT NULL,
    "Historic" text NOT NULL,
    "Parcel" integer NOT NULL,
    "TotalParcel" integer NOT NULL,
    "Grouper" bigint NULL,
    "Value" numeric(18,2) NOT NULL,
    "Realized" boolean NOT NULL,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    "Updated" timestamp with time zone NULL,
    CONSTRAINT "PK_FinancialReleases" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_FinancialReleases_CostCenters_CostCenterId" FOREIGN KEY ("CostCenterId") REFERENCES "CostCenters" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FinancialReleases_FinancialInstitutions_FinancialInstitutionId" FOREIGN KEY ("FinancialInstitutionId") REFERENCES "FinancialInstitutions" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FinancialReleases_FinancialPlannings_FinancialPlanningId" FOREIGN KEY ("FinancialPlanningId") REFERENCES "FinancialPlannings" ("Id") ON DELETE RESTRICT
);

-- Initial Seed Data

-- Admin User (Password: admin123 - BCrypt Hash)
INSERT INTO "Users" ("Name", "Email", "Password", "Active", "Created")
VALUES ('Administrator', 'admin@xcontrol.com', '$2a$11$CkYzAG8.337cQNnCJt3qPel.Q7midduo/LlEaURu8ppNngwny/rPq', true, now());

-- Initial Categories (Optional but helpful)
INSERT INTO "CostCenters" ("Name", "Description", "Sequence", "IsActive")
VALUES 
('Receitas', 'Entradas de recursos', 1, true),
('Despesas', 'Saídas de recursos', 2, true);

-- Initial Institution
INSERT INTO "FinancialInstitutions" ("Name", "Description", "Sequence", "IsActive")
VALUES ('Caixa Principal', 'Dinheiro em espécie', 1, true);

-- Initial User Financial Institution
INSERT INTO "UserFinancialInstitutions" ("UserId", "FinancialInstitutionId", "Created")
VALUES (1, 1, now());
