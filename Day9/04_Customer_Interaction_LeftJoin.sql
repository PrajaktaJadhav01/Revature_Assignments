USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    A.City,
    A.Country
FROM Customer C
RIGHT JOIN CustomerAddress A
ON C.CustomerId = A.CustomerId;
