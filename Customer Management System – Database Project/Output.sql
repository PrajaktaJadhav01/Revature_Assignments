USE CustomerManagementDB;
GO

SELECT * FROM Customer;
SELECT * FROM ContactPerson;
SELECT * FROM CustomerAddress;
SELECT * FROM CustomerInteraction;
SELECT * FROM CustomerAudit;

EXEC GetActiveCustomers;
EXEC GetCustomerInteractions 1;
EXEC GetCustomerInteractions 2;
EXEC GetCustomerInteractions 3;

SELECT 
    C.CustomerName,
    CP.Name AS ContactPerson,
    CA.City,
    CI.InteractionType,
    CI.Subject
FROM Customer C
LEFT JOIN ContactPerson CP ON C.CustomerId = CP.CustomerId
LEFT JOIN CustomerAddress CA ON C.CustomerId = CA.CustomerId
LEFT JOIN CustomerInteraction CI ON C.CustomerId = CI.CustomerId;