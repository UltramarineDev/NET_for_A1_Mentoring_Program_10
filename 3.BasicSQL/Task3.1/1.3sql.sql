BEGIN
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Region')
BEGIN
	EXEC sp_rename '[dbo].[Region]', 'Regions';
END
END

BEGIN
IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'FoundedDate')
BEGIN

ALTER TABLE [dbo].[Customers]
	ADD [FoundedDate] [datetime] NULL
END
END