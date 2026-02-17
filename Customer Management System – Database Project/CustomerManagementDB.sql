USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'CustomerManagementDB')
BEGIN
    ALTER DATABASE CustomerManagementDB 
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

    DROP DATABASE CustomerManagementDB;
END
GO

CREATE DATABASE CustomerManagementDB;
GO

USE CustomerManagementDB;
GO

CREATE TABLE Segment(
    SegmentId INT IDENTITY(1,1) PRIMARY KEY,
    SegmentName VARCHAR(100),
    Description VARCHAR(200)
);

CREATE TABLE Customer(
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(150) NOT NULL,
    Email VARCHAR(150),
    Phone VARCHAR(20),
    SegmentId INT,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (SegmentId) REFERENCES Segment(SegmentId)
);

CREATE TABLE ContactPerson(
    ContactPersonId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    Name VARCHAR(150),
    Email VARCHAR(150),
    Phone VARCHAR(20),
    Title VARCHAR(100),
    IsPrimary BIT,
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

CREATE TABLE CustomerAddress(
    AddressId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    AddressType VARCHAR(50),
    Street VARCHAR(200),
    City VARCHAR(100),
    State VARCHAR(100),
    PostalCode VARCHAR(20),
    Country VARCHAR(100),
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

CREATE TABLE CustomerAudit(
    AuditId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    ChangedField VARCHAR(100),
    OldValue VARCHAR(200),
    NewValue VARCHAR(200),
    ChangedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE CustomerInteraction(
    InteractionId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT,
    InteractionType VARCHAR(50),
    Subject VARCHAR(200),
    Details VARCHAR(MAX),
    InteractionDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);
GO

INSERT INTO Segment (SegmentName, Description)
VALUES ('Corporate','Corporate Customers');

INSERT INTO Customer (CustomerName, Email, Phone, SegmentId)
VALUES 
('Revature - Harshali','harshali@revature.com','9000000001',1),
('Revature - Prajakta','prajakta@revature.com','9000000002',1),
('Revature - Tanaya','tanaya@revature.com','9000000003',1);

INSERT INTO ContactPerson (CustomerId, Name, Email, Phone, Title, IsPrimary)
VALUES
(1,'Harshali','harshali@revature.com','9000000001','HR',1),
(2,'Prajakta','prajakta@revature.com','9000000002','Developer',1),
(3,'Tanaya','tanaya@revature.com','9000000003','Manager',1);

INSERT INTO CustomerAddress (CustomerId, AddressType, Street, City, State, PostalCode, Country)
VALUES
(1,'Office','Hinjewadi','Pune','Maharashtra','411057','India'),
(2,'Office','Hinjewadi','Pune','Maharashtra','411057','India'),
(3,'Office','Hinjewadi','Pune','Maharashtra','411057','India');

INSERT INTO CustomerInteraction (CustomerId, InteractionType, Subject, Details)
VALUES
(1,'Meeting','Onboarding Discussion','Discussed onboarding process'),
(2,'Call','Project Allocation','Assigned new project'),
(3,'Email','Performance Review','Sent performance feedback');

INSERT INTO CustomerAudit (CustomerId, ChangedField, OldValue, NewValue)
VALUES
(2,'Phone','9000000002','9111111111');
GO

CREATE OR ALTER PROCEDURE GetActiveCustomers
AS
BEGIN
    SELECT * FROM Customer WHERE IsDeleted = 0;
END;
GO

CREATE OR ALTER PROCEDURE GetCustomerInteractions
    @CustomerId INT
AS
BEGIN
    SELECT * FROM CustomerInteraction WHERE CustomerId = @CustomerId;
END;
GO