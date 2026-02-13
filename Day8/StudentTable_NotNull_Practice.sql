-- Check if table exists and delete it
IF OBJECT_ID('Stud') IS NOT NULL
DROP TABLE Stud;

-- Create table
CREATE TABLE Stud (
    FirstName VARCHAR(50) NULL,
    LastName VARCHAR(50),
    Gender VARCHAR(10),
    Age INT,
    ID INT
);

-- Insert records (Specify column names)
INSERT INTO Stud (FirstName, LastName, Gender, Age, ID) VALUES
(NULL,'Sharma','Male',21,1),
(NULL,'Patil','Female',22,2),
('Amit','Jadhav','Male',20,3),
(NULL,'Kumar','Male',23,4),
('Neha','Singh','Female',21,5);

-- Replace NULL values
UPDATE Stud
SET FirstName = 'Unknown'
WHERE FirstName IS NULL;

-- Add NOT NULL constraint
ALTER TABLE Stud
ALTER COLUMN FirstName VARCHAR(50) NOT NULL;

-- Display Output
SELECT * FROM Stud;
