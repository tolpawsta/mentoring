--> Using aggregate GROUP BY and NAVING
-->1
select Year(o.OrderDate) as Year, Count(o.OrderDate) as Total
from Orders as o
Group by Year(o.OrderDate)

-->verification query
select Count(o.OrderDate) as [Count orders]
from Orders as o

-->2
select (select Concat(FirstName,' ',LastName) 
from Employees as e where e.EmployeeID =o.EmployeeID) as Seller,
COUNT(o.OrderDate) as Amount
from Orders as o 
group by o.EmployeeID
order by Amount desc

-->3
select Year(o.OrderDate) as [Year order],
(select Concat(FirstName,' ',LastName) 
from Employees as e where e.EmployeeID =o.EmployeeID) as Seller,
(select c.ContactName from Customers as c where c.CustomerID=o.CustomerID) as Customer,
Count(o.OrderDate) as Amount
from Orders as o
group by o.CustomerID, o.EmployeeID,Year(o.OrderDate)
having Year(o.OrderDate)=1998
order by o.EmployeeID

-->4
select o.CustomerID,o.EmployeeID, 
(select City from Customers where CustomerID=o.CustomerID) as Customer_city,
(select City from Employees where EmployeeID=o.EmployeeID) as Employee_city
from Orders as o
where ((select City from Customers where CustomerID=o.CustomerID) in (select City from Employees where EmployeeID=o.EmployeeID))
group by o.CustomerID,o.EmployeeID
--having Customer_city = Employee_city

-->5
select c.City, c.ContactName, c.CustomerID
from Customers as c
group by c.City,c.ContactName, c.CustomerID
order by c.City

-->6
select  Concat(FirstName,' ',LastName) as Employee,
(select Concat(FirstName,' ',LastName) from Employees where EmployeeID=e.ReportsTo) as Leader
from Employees as e
--having e.ReportsTo is not null

-->6.1
select  e.EmployeeID,
(select Concat(FirstName,' ',LastName) from Employees where EmployeeID=e.EmployeeID) as Employee,
e.ReportsTo,
(select Concat(FirstName,' ',LastName) from Employees where EmployeeID=e.ReportsTo) as Leader
from Employees as e
group by e.EmployeeID,e.ReportsTo
--having e.ReportsTo is not null
