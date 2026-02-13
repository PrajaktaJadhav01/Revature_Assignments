USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    A.Street,
    A.City,
    A.Country
FROM Customer C
LEFT JOIN CustomerAddress A
ON C.CustomerId = A.CustomerId;
