USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    CP.Name,
    CP.Email,
    CP.Phone
FROM Customer C
INNER JOIN ContactPerson CP
ON C.CustomerId = CP.CustomerId;
