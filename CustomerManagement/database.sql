-- Use master database
USE master;
GO

-- Drop database if it already exists
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'CustomerManagementDB')
BEGIN
    ALTER DATABASE CustomerManagementDB 
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CustomerManagementDB;
END
GO

-- Create new database
CREATE DATABASE CustomerManagementDB;
GO

-- Switch to new database
USE CustomerManagementDB;
GO


-- Segment table to store business segments
CREATE TABLE Segment(
    SegmentId INT IDENTITY(1,1) PRIMARY KEY,
    SegmentName VARCHAR(100) NOT NULL,
    Description VARCHAR(200)
);
GO


-- Main Customer table
CREATE TABLE Customer(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(150) NOT NULL,
    Email VARCHAR(150) UNIQUE,
    Phone VARCHAR(20),
    Website VARCHAR(200),
    Industry VARCHAR(100),
    CompanySize VARCHAR(50),
    Classification VARCHAR(50) 
        CHECK (Classification IN ('Prospect','Active','Inactive','VIP','At-Risk')),
    Type VARCHAR(50) 
        CHECK (Type IN ('Business','Individual')),
    SegmentId INT,
    ParentCustomerId INT NULL,
    AccountValue DECIMAL(18,2) DEFAULT 0,
    HealthScore INT DEFAULT 100,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (SegmentId) REFERENCES Segment(SegmentId),
    FOREIGN KEY (ParentCustomerId) REFERENCES Customer(CustomerId)
);
GO


-- Contact persons table
CREATE TABLE ContactPerson(
    ContactPersonId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    Name VARCHAR(150),
    Email VARCHAR(150),
    Phone VARCHAR(20),
    Title VARCHAR(100),
    IsPrimary BIT DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE
);
GO


-- Customer address table
CREATE TABLE CustomerAddress(
    AddressId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    AddressType VARCHAR(50) 
        CHECK (AddressType IN ('Billing','Shipping','Primary')),
    Street VARCHAR(200) NOT NULL,
    City VARCHAR(100) NOT NULL,
    State VARCHAR(100) NOT NULL,
    PostalCode VARCHAR(20) NOT NULL,
    Country VARCHAR(100) NOT NULL,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE
);
GO


-- Customer interaction table
CREATE TABLE CustomerInteraction(
    InteractionId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    InteractionType VARCHAR(50) 
        CHECK (InteractionType IN ('Call','Email','Meeting','Support Ticket')),
    Subject VARCHAR(200),
    Details VARCHAR(MAX),
    InteractionDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE
);
GO


-- Customer audit table
CREATE TABLE CustomerAudit(
    AuditId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    ChangedField VARCHAR(100),
    OldValue VARCHAR(200),
    NewValue VARCHAR(200),
    ChangedDate DATETIME DEFAULT GETDATE()
);
GO


-- Insert segments
INSERT INTO Segment (SegmentName, Description)
VALUES 
('Enterprise','Large Enterprise Customers'),
('SMB','Small Medium Business');
GO


-- Insert customers (3 team-based customers)
INSERT INTO Customer
(CustomerName, Email, Phone, Website, Industry, CompanySize,
 Classification, Type, SegmentId, AccountValue)
VALUES
('Revature','info@revature.com','9000000001',
 'www.revature.com','IT Services','500+',
 'Active','Business',1,500000),

('Prajakta Consulting','prajakta@gmail.com','9000000002',
 'www.prajaktaconsulting.com','Consulting','10-50',
 'Prospect','Business',2,50000),

('Tanaya Solutions','tanaya@gmail.com','9000000003',
 'www.tanayasolutions.com','IT Services','100-200',
 'Active','Business',1,200000);
GO


-- Insert contact persons
INSERT INTO ContactPerson (CustomerId, Name, Email, Phone, Title, IsPrimary)
VALUES
(1,'Harshali','harshali@revature.com','9000000001','HR',1),
(2,'Prajakta','prajakta@gmail.com','9000000002','Owner',1),
(3,'Tanaya','tanaya@gmail.com','9000000003','Manager',1);
GO


-- Insert addresses
INSERT INTO CustomerAddress
(CustomerId, AddressType, Street, City, State, PostalCode, Country)
VALUES
(1,'Billing','Hinjewadi','Pune','Maharashtra','411057','India'),
(2,'Primary','Baner','Pune','Maharashtra','411045','India'),
(3,'Primary','Wakad','Pune','Maharashtra','411057','India');
GO


-- Insert interactions
INSERT INTO CustomerInteraction
(CustomerId, InteractionType, Subject, Details)
VALUES
(1,'Meeting','Contract Discussion','Discussed enterprise contract'),
(2,'Call','Initial Inquiry','Explained service offerings'),
(3,'Email','Project Discussion','Discussed new client onboarding');
GO


-- Procedure to get active customers
GO
CREATE OR ALTER PROCEDURE GetActiveCustomers
AS
BEGIN
    SELECT * 
    FROM Customer
    WHERE IsDeleted = 0
      AND Classification = 'Active';
END;
GO


-- Procedure to get lifetime value
GO
CREATE OR ALTER PROCEDURE GetCustomerLifetimeValue
    @CustomerId INT
AS
BEGIN
    SELECT CustomerName,
           AccountValue AS LifetimeValue
    FROM Customer
    WHERE CustomerId = @CustomerId;
END;
GO


-- Procedure to calculate health score
GO
CREATE OR ALTER PROCEDURE CalculateCustomerHealthScore
    @CustomerId INT
AS
BEGIN
    DECLARE @InteractionCount INT;

    SELECT @InteractionCount = COUNT(*)
    FROM CustomerInteraction
    WHERE CustomerId = @CustomerId;

    UPDATE Customer
    SET HealthScore =
        CASE
            WHEN @InteractionCount > 5 THEN 90
            WHEN @InteractionCount BETWEEN 3 AND 5 THEN 75
            ELSE 50
        END
    WHERE CustomerId = @CustomerId;

    SELECT CustomerName, HealthScore
    FROM Customer
    WHERE CustomerId = @CustomerId;
END;
GO


-- Procedure to check duplicate email
GO
CREATE OR ALTER PROCEDURE CheckDuplicateCustomer
    @Email VARCHAR(150)
AS
BEGIN
    SELECT *
    FROM Customer
    WHERE Email = @Email;
END;
GO


-- Final output section
PRINT 'Customer Table Data';
SELECT * FROM Customer;

PRINT 'Segment Table Data';
SELECT * FROM Segment;

PRINT 'Contact Person Data';
SELECT * FROM ContactPerson;

PRINT 'Customer Address Data';
SELECT * FROM CustomerAddress;

PRINT 'Customer Interaction Data';
SELECT * FROM CustomerInteraction;

PRINT 'Active Customers';
EXEC GetActiveCustomers;

PRINT 'Customer Lifetime Value';
EXEC GetCustomerLifetimeValue 1;

PRINT 'Customer Health Score';
EXEC CalculateCustomerHealthScore 1;

PRINT 'Duplicate Customer Check';
EXEC CheckDuplicateCustomer 'info@revature.com';
