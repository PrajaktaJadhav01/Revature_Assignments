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


-- Segment table
CREATE TABLE Segment(
    SegmentId INT IDENTITY(1,1) PRIMARY KEY,
    SegmentName VARCHAR(100) NOT NULL,
    Description VARCHAR(200)
);
GO


-- Customer table
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


-- ORDERS TABLE (Added for your C# project)
CREATE TABLE Orders(
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(150),
    Quantity INT,
    TotalAmount DECIMAL(10,2),
    CustomerId INT,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
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


-- Insert customers
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


-- Insert Orders (Added)
INSERT INTO Orders(ProductName, Quantity, TotalAmount, CustomerId)
VALUES
('Laptop',2,150000,1),
('Consulting Package',1,50000,2),
('Software License',5,200000,3);
GO
