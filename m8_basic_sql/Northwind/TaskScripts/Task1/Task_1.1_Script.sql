--> Simple data filtration
--> 1 
select ord.OrderID, ord.ShippedDate, ord.ShipVia
from Orders as ord
where ord.ShippedDate>='1998-05-06' and ord.ShipVia>=2

--> 2
SELECT ord.OrderID,
CASE 
WHEN ord.ShippedDate IS NULL THEN 'Not shipped'
END as ShippedDate
FROM Orders AS ord
WHERE ord.ShippedDate IS NULL

--> 3
SELECT ord.OrderID as [Order Number],
CASE 
WHEN ord.ShippedDate IS NULL THEN 'Not shipped'
else CONVERT(nvarchar,ord.ShippedDate,127)
END as [Shipped Date]
FROM Orders AS ord
WHERE ord.ShippedDate>'1998-05-06' or ord.ShippedDate is null

