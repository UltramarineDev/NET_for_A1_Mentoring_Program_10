BEGIN
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CreditDetails')
BEGIN
CREATE TABLE [dbo].[CreditDetails](
[CreditID] [int] IDENTITY(1,1) NOT NULL,
[CustomerID] [nchar](5) NULL,
[CardNumber] [int] NOT NULL,
[ExpirationDate] [datetime] NULL,
[CardHolderName] [nvarchar](40) NULL,
[EmployeeId] INT REFERENCES dbo.Employees ([EmployeeID])
)
END
END