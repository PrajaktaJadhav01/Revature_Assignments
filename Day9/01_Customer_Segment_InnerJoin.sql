USE CustomerManagementDB;

SELECT 
    C.CustomerId,
    C.CustomerName,
    C.Email,
    S.SegmentName
FROM Customer C
INNER JOIN Segment S
ON C.SegmentId = S.SegmentId;
