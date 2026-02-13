USE CustomerManagementDB;

SELECT 
    C1.CustomerName AS ChildCustomer,
    C2.CustomerName AS ParentCustomer
FROM Customer C1
LEFT JOIN Customer C2
ON C1.ParentCustomerId = C2.CustomerId;
