CREATE TABLE [dbo].[EmployeeCards]
(
	[CardID] INT NOT NULL PRIMARY KEY, 
    [CardNumber] NUMERIC(12) NOT NULL, 
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
GO
CREATE  INDEX "EmployeeID" ON "dbo"."EmployeeCards"("EmployeeID")
