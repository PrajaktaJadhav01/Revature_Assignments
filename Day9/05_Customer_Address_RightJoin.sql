USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    I.InteractionType,
    I.Subject
FROM Customer C
LEFT JOIN CustomerInteraction I
ON C.CustomerId = I.CustomerId;
