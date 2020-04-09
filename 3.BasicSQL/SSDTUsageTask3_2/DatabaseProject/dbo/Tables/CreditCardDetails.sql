CREATE TABLE [dbo].[CreditCardDetails]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ExspirationDate] DATETIME NULL, 
    [EmployeeId] INT NULL,
	FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
)
