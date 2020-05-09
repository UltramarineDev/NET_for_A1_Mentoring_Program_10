CREATE TABLE [dbo].[CreditCardDetails]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CardNumber] INT NOT NULL, 
    [ExpiredDate] DATETIME NULL, 
    [CardHolder] NVARCHAR(50) NULL, 
    [EmployeeID] INT REFERENCES Employees(EmployeeID) NOT NULL
)
