IF (NOT EXISTS (SELECT * FROM sys.tables WHERE [name] = 'EmployeeCards'))
BEGIN
CREATE TABLE [dbo].[EmployeeCards]
(
	[CardNumber] NCHAR(12) NOT NULL PRIMARY KEY, 
    [ExpirationDate] DATE NOT NULL, 
    [CardHolder] NCHAR(100) NULL, 
    [EmployeeID] INT NOT NULL,
    CONSTRAINT "FK_EmployeeCards_Employees" FOREIGN KEY 
	(
		"EmployeeID"
	) REFERENCES "dbo"."Employees" (
		"EmployeeID"
	),
)

CREATE  INDEX "EmployeeID" ON "dbo"."EmployeeCards"("EmployeeID")
END
ELSE
BEGIN
PRINT 'EmployeeCards allready exists'
END