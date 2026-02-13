USE CustomerManagementDB;
GO

-- Transaction example
BEGIN TRANSACTION;

UPDATE Customer
SET IsDeleted = 0
WHERE CustomerId = 2;

COMMIT;
GO


-- Isolation level example
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN TRANSACTION;
SELECT * FROM Customer;
COMMIT;
GO


-- Create index on Email column
DROP INDEX IF EXISTS idx_customer_email ON Customer;

CREATE INDEX idx_customer_email
ON Customer(Email);
GO


-- Create view for active customers
DROP VIEW IF EXISTS ActiveCustomersView;
GO

CREATE VIEW ActiveCustomersView AS
SELECT CustomerName, Email, Phone
FROM Customer
WHERE IsDeleted = 0;
GO

SELECT * FROM ActiveCustomersView;
GO


-- Create stored procedure to get address by customer id
DROP PROCEDURE IF EXISTS GetCustomerAddress;
GO

CREATE PROCEDURE GetCustomerAddress
@CustomerId INT
AS
BEGIN
SELECT *
FROM CustomerAddress
WHERE CustomerId = @CustomerId;
END;
GO

EXEC GetCustomerAddress 1;
GO


-- Create function to count interactions
DROP FUNCTION IF EXISTS GetInteractionCount;
GO

CREATE FUNCTION GetInteractionCount (@CustomerId INT)
RETURNS INT
AS
BEGIN
DECLARE @Count INT;

SELECT @Count = COUNT(*)
FROM CustomerInteraction
WHERE CustomerId = @CustomerId;

RETURN @Count;
END;
GO

SELECT CustomerName,
dbo.GetInteractionCount(CustomerId) AS TotalInteractions
FROM Customer;
GO


-- Create trigger to log customer name changes
DROP TRIGGER IF EXISTS trg_CustomerUpdate;
GO

CREATE TRIGGER trg_CustomerUpdate
ON Customer
AFTER UPDATE
AS
BEGIN
INSERT INTO CustomerAudit
(CustomerId, ChangedField, OldValue, NewValue)
SELECT 
i.CustomerId,
'CustomerName',
d.CustomerName,
i.CustomerName
FROM inserted i
JOIN deleted d
ON i.CustomerId = d.CustomerId
WHERE i.CustomerName <> d.CustomerName;
END;
GO


-- CTE example to count interactions
WITH InteractionSummary AS
(
SELECT CustomerId, COUNT(*) AS TotalInteractions
FROM CustomerInteraction
GROUP BY CustomerId
)

SELECT 
C.CustomerName,
ISNULL(I.TotalInteractions,0) AS TotalInteractions
FROM Customer C
LEFT JOIN InteractionSummary I
ON C.CustomerId = I.CustomerId;
GO


-- Complex join report
SELECT 
C.CustomerName,
S.SegmentName,
COUNT(I.InteractionId) AS TotalInteractions,
A.City
FROM Customer C
LEFT JOIN Segment S ON C.SegmentId = S.SegmentId
LEFT JOIN CustomerInteraction I ON C.CustomerId = I.CustomerId
LEFT JOIN CustomerAddress A ON C.CustomerId = A.CustomerId
GROUP BY C.CustomerName, S.SegmentName, A.City;
GO

