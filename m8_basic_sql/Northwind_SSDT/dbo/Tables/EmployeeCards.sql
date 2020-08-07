CREATE TABLE [dbo].[EmployeeCards]
(
	[CardNumber] NCHAR(12) NOT NULL PRIMARY KEY,
	[ExpirationDate] DATE NOT NULL, 
    [CardHolder] NVARCHAR(100) NULL, 
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
