USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    A.City
FROM Customer C
FULL OUTER JOIN CustomerAddress A
ON C.CustomerId = A.CustomerId;
