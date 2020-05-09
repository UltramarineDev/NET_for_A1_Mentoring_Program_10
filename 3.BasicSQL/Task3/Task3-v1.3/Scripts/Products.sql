set identity_insert  Products on
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 1)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(1,'Chai',1,1,'10 boxes x 20 bags',18,39,0,10,0)
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 2)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(2,'Chang',1,1,'24 - 12 oz bottles',19,17,40,25,0)
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 3)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(3,'Aniseed Syrup',1,2,'12 - 550 ml bottles',10,13,70,25,0)
   END
END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 4)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(4,'Chef Anton''s Cajun Seasoning',2,2,'48 - 6 oz jars',22,53,0,0,0)
   END

END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 5)
   BEGIN
	INSERT Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(5,'Chef Anton''s Gumbo Mix',2,2,'36 boxes',21.35,0,0,0,1)
   END

END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 6)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(6,'Grandma''s Boysenberry Spread',3,2,'12 - 8 oz jars',25,120,0,25,0)
   END

END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 7)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(7,'Uncle Bob''s Organic Dried Pears',3,7,'12 - 1 lb pkgs.',30,15,0,10,0)
   END

END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 8)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(8,'Northwoods Cranberry Sauce',3,2,'12 - 12 oz jars',40,6,0,0,0)
   END

END
GO

BEGIN
   IF NOT EXISTS (SELECT "ProductID" FROM Products WHERE "ProductID" = 9)
   BEGIN
	INSERT  Products ("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(9,'Mishi Kobe Niku',4,6,'18 - 500 g pkgs.',97,29,0,0,1)
   END

END
GO

set identity_insert Products off
GO