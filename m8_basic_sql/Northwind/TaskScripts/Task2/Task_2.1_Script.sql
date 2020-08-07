--> Using agregate functions SUM, COUNT
-->1
select Sum(od.Quantity*od.UnitPrice*(1-od.Discount)) as Totals
from [Order Details] as od

-->2
select Count(o.ShippedDate) as [Count shipped orders]
from Orders as o

-->3
select COUNT(distinct o.CustomerID)
from Orders as o