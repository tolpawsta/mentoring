--> Using oprators BETWEEN, DISTINCT
-->1
select distinct od.OrderID as [ID]
from [Order Details] as od
where od.Quantity between 3 and 10

-->2
select c.CustomerID, c.Country
from Customers as c
where Substring(c.Country,1,1) between 'b' and 'g'
order by c.Country

-->3
select *
from Customers as c
where Substring(c.Country,1,1) >='b' and Substring(c.Country,1,1)<= 'g'