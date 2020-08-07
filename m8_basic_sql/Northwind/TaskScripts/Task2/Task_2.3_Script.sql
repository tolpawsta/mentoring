-->1 Case 1 wiyh 1 join
select  distinct (select concat(FirstName,' ',LastName) from Employees where EmployeeID=et.EmployeeID) as [Employee],
(select RegionDescription from Region where RegionID= t.RegionID) as [Region]
from EmployeeTerritories as et Join Territories as t
on et.TerritoryID=t.TerritoryID
where t.RegionID=(select RegionID from Region where RegionDescription='Western')

-->1 Case 2 with 3 join
select distinct Concat(e.FirstName,' ',e.LastName) as Seller,r.RegionDescription
from Employees as e 
join EmployeeTerritories as et on e.EmployeeID=et.EmployeeID
join Territories as t on et.TerritoryID=t.TerritoryID
join Region as r on t.RegionID=r.RegionID
where r.RegionDescription='Western'

-->2
select c.ContactName, Count(o.OrderDate) as [Number_orders]
from Customers as c left outer join Orders as o
on c.CustomerID = o.CustomerID
group by c.ContactName
order by [Number_orders]