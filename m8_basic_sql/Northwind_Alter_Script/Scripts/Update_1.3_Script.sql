if EXISTS(SELECT * FROM sys.tables WHERE [name]='Region')
BEGIN  
EXEC sp_rename 'Region', 'Regions'
END
ELSE
BEGIN
PRINT 'table Region not exists'
END
Alter table Customers
Add SomeDate NVARCHAR null