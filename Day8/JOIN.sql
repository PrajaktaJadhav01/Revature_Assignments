SELECT EmpName, DeptName
FROM Employee
JOIN Department
ON Employee.DeptID = Department.DeptID;
