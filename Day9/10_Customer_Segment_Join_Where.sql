USE CustomerManagementDB;

SELECT 
    C.CustomerName,
    S.SegmentName
FROM Customer C
INNER JOIN Segment S
ON C.SegmentId = S.SegmentId
WHERE S.SegmentName = 'Corporate';
