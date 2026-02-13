USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    C.Email,
    S.SegmentName,
    A.City,
    CP.Name AS ContactPerson
FROM Customer C
LEFT JOIN Segment S 
    ON C.SegmentId = S.SegmentId
LEFT JOIN CustomerAddress A 
    ON C.CustomerId = A.CustomerId
LEFT JOIN ContactPerson CP 
    ON C.CustomerId = CP.CustomerId;
