-->1 Case 1
select s.CompanyName, gp.Sum_UnitInStock
from (select p.SupplierID, 
		Sum(p.UnitsInStock) as Sum_UnitInStock	
	   from Products as p 
	   where p.UnitsInStock not in(0)
	   group by p.SupplierID) as gp
join Suppliers as s on gp.SupplierID=s.SupplierID
group by s.CompanyName, gp.Sum_UnitInStock

-->1 Case 2
select s.CompanyName
from Suppliers as s, Products as p
where s.SupplierID in (
		select p.SupplierID
		from Products as p
		group by p.SupplierID
		having sum(p.UnitsInStock) > 0)
order by s.CompanyName

-->2
select CONCAT(e.FirstName,' ',e.LastName) as Employee_FullName,
ord.Number_orders as [Number_orders]
from (select o.EmployeeID, Count(o.OrderDate) as [Number_orders]
		from Orders as o
		group by o.EmployeeID) as ord
join Employees as e on ord.EmployeeID=e.EmployeeID
where ord.Number_orders>150

-->3 Case 1
select c.ContactName
from Customers as c
where not exists 
		(select o.CustomerID
		from Orders as o
		where o.CustomerID =c.CustomerID)
order by c.ContactName

-->3 Case 2 Test with join
select c.ContactName, Count(o.OrderDate) as [Number_orders]
from Customers as c
left join Orders as o on c.CustomerID=o.CustomerID
group by c.ContactName
having Count(o.OrderDate)=0
order by Number_orders