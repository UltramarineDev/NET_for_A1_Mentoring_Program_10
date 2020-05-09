set identity_insert Suppliers on
GO

BEGIN
   IF NOT EXISTS (SELECT "SupplierID" FROM Suppliers WHERE "SupplierID" = 1)
   BEGIN
	INSERT  Suppliers ("SupplierID","CompanyName","ContactName","ContactTitle","Address","City","Region","PostalCode","Country","Phone","Fax","HomePage") VALUES(1,'Exotic Liquids','Charlotte Cooper','Purchasing Manager','49 Gilbert St.','London',NULL,'EC1 4SD','UK','(171) 555-2222',NULL,NULL)
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "SupplierID" FROM Suppliers WHERE "SupplierID" = 2)
   BEGIN
	INSERT  Suppliers ("SupplierID","CompanyName","ContactName","ContactTitle","Address","City","Region","PostalCode","Country","Phone","Fax","HomePage") VALUES(2,'New Orleans Cajun Delights','Shelley Burke','Order Administrator','P.O. Box 78934','New Orleans','LA','70117','USA','(100) 555-4822',NULL,'#CAJUN.HTM#')
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "SupplierID" FROM Suppliers WHERE "SupplierID" = 3)
   BEGIN
	INSERT  Suppliers ("SupplierID","CompanyName","ContactName","ContactTitle","Address","City","Region","PostalCode","Country","Phone","Fax","HomePage") VALUES(3,'Grandma Kelly''s Homestead','Regina Murphy','Sales Representative','707 Oxford Rd.','Ann Arbor','MI','48104','USA','(313) 555-5735','(313) 555-3349',NULL)
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "SupplierID" FROM Suppliers WHERE "SupplierID" = 4)
   BEGIN
	INSERT  Suppliers ("SupplierID","CompanyName","ContactName","ContactTitle","Address","City","Region","PostalCode","Country","Phone","Fax","HomePage") VALUES(4,'Tokyo Traders','Yoshi Nagase','Marketing Manager','9-8 Sekimai Musashino-shi','Tokyo',NULL,'100','Japan','(03) 3555-5011',NULL,NULL)
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "SupplierID" FROM Suppliers WHERE "SupplierID" = 5)
   BEGIN
	INSERT  Suppliers ("SupplierID","CompanyName","ContactName","ContactTitle","Address","City","Region","PostalCode","Country","Phone","Fax","HomePage") VALUES(5,'Cooperativa de Quesos ''Las Cabras''','Antonio del Valle Saavedra','Export Administrator','Calle del Rosal 4','Oviedo','Asturias','33007','Spain','(98) 598 76 54',NULL,NULL)
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "SupplierID" FROM Suppliers WHERE "SupplierID" = 6)
   BEGIN
	INSERT  Suppliers ("SupplierID","CompanyName","ContactName","ContactTitle","Address","City","Region","PostalCode","Country","Phone","Fax","HomePage") VALUES(6,'Mayumi''s','Mayumi Ohno','Marketing Representative','92 Setsuko Chuo-ku','Osaka',NULL,'545','Japan','(06) 431-7877',NULL,'Mayumi''s (on the World Wide Web)#http://www.microsoft.com/accessdev/sampleapps/mayumi.htm#')
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "SupplierID" FROM Suppliers WHERE "SupplierID" = 7)
   BEGIN
	INSERT  Suppliers ("SupplierID","CompanyName","ContactName","ContactTitle","Address","City","Region","PostalCode","Country","Phone","Fax","HomePage") VALUES(7,'Pavlova, Ltd.','Ian Devling','Marketing Manager','74 Rose St. Moonie Ponds','Melbourne','Victoria','3058','Australia','(03) 444-2343','(03) 444-6588',NULL)
   END
END
GO

set identity_insert Suppliers off
GO