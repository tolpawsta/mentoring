CREATE TABLE [dbo].[EmployeeCards] (
    [CardNumber]  NCHAR (12)     CONSTRAINT [DF_EmployeeCards_CardNumber] DEFAULT ((0)) NOT NULL,
    [ExpiredDate] NCHAR (4)      NOT NULL,
    [CardHolder]  NVARCHAR (100) NOT NULL,
    [EmployeeID]  INT            NOT NULL,
    CONSTRAINT [PK_EmployeeCards_1] PRIMARY KEY CLUSTERED ([CardNumber] ASC),
    CONSTRAINT [FK_EmployeeCards_Employees] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

