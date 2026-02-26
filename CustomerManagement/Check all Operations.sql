-- Select your database
USE CustomerManagementDB;

-- 1. Check all customers (Insert / Update / Delete verification)
PRINT '--- ALL CUSTOMERS ---';
SELECT * FROM Customer;

-- 2. Check specific customer update (example: phone change)
PRINT '--- CUSTOMER PHONE CHECK ---';
SELECT CustomerId, CustomerName, Phone
FROM Customer;

-- 3. Filter customers (Age > 25)
PRINT '--- FILTER CUSTOMERS AGE > 25 ---';
SELECT CustomerId, CustomerName, Age
FROM Customer
WHERE Age > 25;

-- 4. Sort customers by name
PRINT '--- SORT CUSTOMERS BY NAME ---';
SELECT CustomerId, CustomerName, Age
FROM Customer
ORDER BY CustomerName;

-- 5. Join Customers and Orders
PRINT '--- JOIN CUSTOMERS & ORDERS ---';
SELECT c.CustomerId, c.CustomerName, o.OrderId, o.TotalAmount
FROM Customer c
LEFT JOIN Orders o ON c.CustomerId = o.CustomerId;

-- 6. Grouping and aggregation
PRINT '--- TOTAL ORDER AMOUNT PER CUSTOMER ---';
SELECT c.CustomerName, SUM(o.TotalAmount) AS TotalSpent
FROM Customer c
LEFT JOIN Orders o ON c.CustomerId = o.CustomerId
GROUP BY c.CustomerName;

-- 7. Projection example
PRINT '--- CUSTOMER PROJECTION (NAME + EMAIL ONLY) ---';
SELECT CustomerName, Email
FROM Customer;