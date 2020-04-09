CREATE TABLE [dbo].[CreditDetails] (
    [CreditID]       INT           IDENTITY (1, 1) NOT NULL,
    [CustomerID]     NCHAR (5)     NULL,
    [CardNumber]     INT           NOT NULL,
    [ExpirationDate] DATETIME      NULL,
    [CardHolderName] NVARCHAR (40) NULL,
    FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID])
);

