USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    COUNT(I.InteractionId) AS TotalInteractions
FROM Customer C
LEFT JOIN CustomerInteraction I
ON C.CustomerId = I.CustomerId
GROUP BY C.CustomerName;
