-- Create Table
CREATE TABLE Student (
    ID INT,
    Name VARCHAR(50),
    Age INT
);

-- Insert Data
INSERT INTO Student VALUES (1, 'Prajakta', 21);
INSERT INTO Student VALUES (2, 'Rahul', 22);

-- Update Data
UPDATE Student
SET Age = 23
WHERE ID = 1;

-- Select Data
SELECT * FROM Student;

-- Delete Data
DELETE FROM Student
WHERE ID = 2;
