USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    A.ChangedField,
    A.OldValue,
    A.NewValue,
    A.ChangedDate
FROM Customer C
INNER JOIN CustomerAudit A
ON C.CustomerId = A.CustomerId;
