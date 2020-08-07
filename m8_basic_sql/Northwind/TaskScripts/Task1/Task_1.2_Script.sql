--> Using IN, DISTINCT, ORDER BY, NOT operators
-->1
select c.ContactName as [Customer Name], c.Country
from Customers as c
where c.Country in('USA','Canada')
order by c.ContactName, c.Country 

-->2
select c.ContactName as [Customer Name], c.Country
from Customers as c
where c.Country not in('USA','Canada')
order by c.ContactName

-->3
select distinct c.Country
from Customers as c
where c.Country is not null
order by c.Country desc
