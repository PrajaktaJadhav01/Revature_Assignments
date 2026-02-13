IF OBJECT_ID('Stud') IS NOT NULL
DROP TABLE Stud;

CREATE TABLE Stud (
    FirstName VARCHAR(50) NULL,
    LastName VARCHAR(50),
    ID INT
);

INSERT INTO Stud VALUES
(NULL,'Sharma',1),
(NULL,'Patil',2),
(NULL,'Jadhav',3),
(NULL,'Kumar',4),
(NULL,'Singh',5);

UPDATE Stud
SET FirstName = 'Unknown'
WHERE FirstName IS NULL;

ALTER TABLE Stud
ALTER COLUMN FirstName VARCHAR(50) NOT NULL;


INSERT INTO Stud VALUES ('Rahul','Test',6);


SELECT * FROM Stud;

