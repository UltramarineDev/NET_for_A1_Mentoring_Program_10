--SELECT FName, LName, Date_Joined, Salary
--FROM STAFF
USE [Northwind]
GO

--������� 1.1. ������� ���������� ������.

--1.������� � ������� Orders ������, ������� ���� ���������� ����� 
--6 ��� 1998 ���� (������� ShippedDate) ������������ � ������� ���������� 
--� ShipVia >= 2. ������ ������ ���������� ������ ������� OrderID, ShippedDate � ShipVia. 
SELECT OrderId, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate >= '1998-05-06' AND ShipVia >= 2

--2.�������� ������, ������� ������� ������ �������������� ������ �� ������� Orders. 
--� ����������� ������� ���������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped� 
--(������������ ��������� ������� CAS�). 
--������ ������ ���������� ������ ������� OrderID � ShippedDate.
SELECT OrderID, ShippedDate =
CASE
	WHEN ShippedDate IS NULL THEN 'Not Shipped'
END
FROM Orders
WHERE ShippedDate IS NULL

--3.������� � ������� Orders ������, ������� ���� ���������� ����� 6 ��� 1998 ���� (ShippedDate) 
--�� ������� ��� ���� ��� ������� ��� �� ����������. � ������� ������ ������������ ������ ������� OrderID (������������� � Order Number) � 
--ShippedDate (������������� � Shipped Date). � ����������� ������� ���������� ��� ������� ShippedDate ������ �������� NULL ������ �Not Shipped�, 
--��� ��������� �������� ���������� ���� � ������� �� ���������.
SELECT OrderId AS 'Order Number',
CASE
	WHEN ShippedDate IS NULL THEN 'Not Shipped' 
END AS 'Shipped Date'
FROM Orders
WHERE ShippedDate > '1998-05-06' OR ShippedDate IS NULL

--������� 1.2. ������������� ���������� IN, DISTINCT, ORDER BY, NOT

--1.������� �� ������� Customers ���� ����������, ����������� � USA � Canada. 
--������ ������� � ������ ������� ��������� IN. ���������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
--����������� ���������� ������� �� ����� ���������� � �� ����� ����������.
SELECT CustomerID, Country
FROM Customers
WHERE Country IN ('USA', 'CANADA')
ORDER BY CustomerID, Country

--2.������� �� ������� Customers ���� ����������, �� ����������� � USA � Canada.
-- ������ ������� � ������� ��������� IN. ���������� ������� � ������ ������������ � ��������� ������ � ����������� �������. 
--����������� ���������� ������� �� ����� ����������.
SELECT CustomerID, Country
FROM Customers
WHERE Country NOT IN ('USA', 'CANADA')
ORDER BY CustomerID

--3.������� �� ������� Customers ��� ������, � ������� ��������� ���������. 
--������ ������ ���� ��������� ������ ���� ��� � ������ ������������ �� ��������. 
--�� ������������ ����������� GROUP BY. ���������� ������ ���� ������� � ����������� �������. 
SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC

--������� 1.3. ������������� ��������� BETWEEN, DISTINCT

--1.������� ��� ������ (OrderID) �� ������� Order Details (������ �� ������ �����������),
-- ��� ����������� �������� � ����������� �� 3 �� 10 ������������ � ��� ������� Quantity � ������� Order Details. ������������ �������� BETWEEN. 
--������ ������ ���������� ������ ������� OrderID.
SELECT DISTINCT OrderId
FROM [dbo].[Order Details]
WHERE Quantity BETWEEN 3 AND 10

--2.������� ���� ���������� �� ������� Customers, 
--� ������� �������� ������ ���������� �� ����� �� ��������� b � g. ������������ �������� BETWEEN.
-- ���������, ��� � ���������� ������� �������� Germany. 
--������ ������ ���������� ������ ������� CustomerID � Country � ������������ �� Country.
SELECT CustomerID, Country
FROM Customers
WHERE Country BETWEEN 'b' AND 'h'
ORDER BY Country

--3.������� ���� ���������� �� ������� Customers, � ������� �������� 
--������ ���������� �� ����� �� ��������� b � g, �� ��������� �������� BETWEEN. 
SELECT CustomerID, Country
FROM Customers
WHERE Country >= 'b' AND Country <= 'h'
ORDER BY Country

--������� 1.4. ������������� ��������� LIKE

--1.� ������� Products ����� ��� �������� (������� ProductName), ��� ����������� ��������� 'chocolade'.
-- ��������, ��� � ��������� 'chocolade' ����� ���� �������� 
--���� ����� 'c' � �������� - ����� ��� ��������, ������� ������������� ����� �������. 
SELECT *
FROM Products
WHERE ProductName LIKE '%cho_olade%'

--������� 2.1. ������������� ���������� ������� (SUM, COUNT)

--1.����� ����� ����� ���� ������� �� ������� Order Details � ������ ���������� ����������� ������� � ������ �� ���. 
--����������� ������� ������ ���� ���� ������ � ����� �������� � ��������� ������� 'Totals'.
SELECT SUM(UnitPrice * Quantity * (1 - Discount)) AS 'Totals'
FROM [dbo].[Order Details]

--2.�� ������� Orders ����� ���������� �������, ������� ��� �� ���� ���������� 
--(�.�. � ������� ShippedDate ��� �������� ���� ��������). ������������ ��� ���� ������� ������ �������� COUNT. 
--�� ������������ ����������� WHERE � GROUP.
SELECT COUNT
(CASE WHEN ShippedDate IS NULL THEN 1 ELSE NULL END)
FROM Orders

--3.�� ������� Orders ����� ���������� ��������� ����������� (CustomerID), 
--��������� ������. ������������ ������� COUNT � �� ������������ ����������� WHERE � GROUP.
SELECT COUNT(DISTINCT CustomerId)
FROM Orders

--������� 2.2. ���������� ������, ������������� ���������� ������� � ����������� GROUP BY � HAVING 

--1.�� ������� Orders ����� ���������� ������� � ������������ �� �����. 
--� ����������� ������� ���� ���������� ��� ������� c ���������� Year � Total.
-- �������� ����������� ������, ������� ��������� ���������� ���� �������.
SELECT COUNT(*) AS 'Total', datepart(yyyy, [OrderDate]) AS 'Year'
FROM Orders
GROUP BY datepart(yyyy, [OrderDate])

SELECT COUNT(*)
FROM Orders

--�� ������� Orders ����� ���������� �������, c�������� ������ ���������. 
--����� ��� ���������� �������� � ��� ����� ������ � ������� Orders, ��� � ������� EmployeeID 
--������ �������� ��� ������� ��������. � ����������� ������� ���� ���������� ������� � ������
-- �������� (������ ������������� ��� ���������� ������������� LastName & FirstName. 
--��� ������ LastName & FirstName ������ ���� �������� ��������� �������� � ������� ��������� �������. 
--����� �������� ������ ������ ������������ ����������� �� EmployeeID.) � ��������� ������� �Seller� � 
--������� c ����������� ������� ���������� � ��������� 'Amount'. 
--���������� ������� ������ ���� ����������� �� �������� ���������� �������.
SELECT COUNT(Orders.EmployeeID) AS 'Amount',
	(SELECT CONCAT(Employees.FirstName, ' ', Employees.LastName) 
     FROM Employees 
	 WHERE Orders.EmployeeID = Employees.EmployeeID)  AS 'Seller'
FROM Orders
JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID
GROUP BY Orders.EmployeeID, Employees.FirstName
ORDER BY 'Amount' DESC

--3.�� ������� Orders ����� ���������� �������, ��������� ������ ��������� � ��� ������� ����������. 
--���������� ���������� ��� ������ ��� �������, ��������� � 1998 ����. 

SELECT EmployeeID, CustomerID, COUNT(*)
FROM Orders
WHERE datepart(yyyy, [OrderDate]) = '1998'
GROUP BY EmployeeID, CustomerID
ORDER BY EmployeeID

--4.����� ����������� � ���������, ������� ����� � ����� ������. ���� � ������ ����� ������ ���� ��� 
--��������� ���������, ��� ������ ���� ��� ��������� �����������, �� ���������� � ����� ���������� � 
--��������� �� ������ �������� � �������������� �����. �� ������������ ����������� JOIN. 

SELECT Customers.CustomerID, Customers.City, Employees.EmployeeID, Employees.City
FROM Customers, Orders, Employees
WHERE Customers.CustomerID = Orders.CustomerID 
	AND Employees.EmployeeID = Orders.EmployeeID 
	AND Employees.City = Customers.City

SELECT Customers.CustomerID, Customers.City, Employees.EmployeeID, Employees.City
FROM Customers
JOIN Orders ON Orders.CustomerID = Customers.CustomerID
JOIN Employees ON Employees.EmployeeID = Orders.EmployeeID 
WHERE Employees.City = Customers.City

--5.����� ���� �����������, ������� ����� � ����� ������. 
SELECT City, CustomerID
FROM Customers
GROUP BY City, CUstomerID

--6.�� ������� Employees ����� ��� ������� �������� ��� ������������.
SELECT EmployeeID, ReportsTo
FROM Employees

--������� 2.3. ������������� JOIN
--1.���������� ���������, ������� ����������� ������ 'Western' (������� Region). 

SELECT Employees.FirstName, Region.RegionDescription
FROM Employees
JOIN Region ON Region.RegionDescription = Employees.Region

--2.������ � ����������� ������� ����� ���� ���������� �� ������� Customers � ��������� ���������� 
--�� ������� �� ������� Orders. ������� �� ��������, ��� � ��������� ���������� ��� �������, �� 
--��� ����� ������ ���� �������� � ����������� �������. 
--����������� ���������� ������� �� ����������� ���������� �������.

SELECT Customers.ContactName, COUNT(Orders.OrderID) AS 'Count'
FROM Customers
JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.ContactName
ORDER BY 'Count'

------������� 2.4. ������������� �����������

--������ ���� ����������� (������� CompanyName � ������� Suppliers), 
--� ������� ��� ���� �� ������ �������� �� ������ (UnitsInStock � ������� Products ����� 0). 
--������������ ��������� SELECT ��� ����� ������� � �������������� ��������� IN. 
SELECT CompanyName 
FROM Suppliers
WHERE Suppliers.SupplierID IN
	(SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0) 

SELECT *
FROM Products
WHERE UnitsInStock = 0

--2.������ ���� ���������, ������� ����� ����� 150 �������. ������������ ��������� SELECT.

SELECT COUNT(OrderID), EmployeeID, 
	(SELECT FirstName
	FROM Employees
	WHERE Employees.EmployeeID = Orders.EmployeeID)
FROM Orders
GROUP BY EmployeeID
HAVING COUNT(OrderID) > 150

--3.������ ���� ���������� (������� Customers), ������� ��
-- ����� �� ������ ������ (��������� �� ������� Orders). ������������ �������� EXISTS.

SELECT CustomerID, ContactName
FROM Customers
WHERE EXISTS 
	(SELECT COUNT(*)
	FROM Orders
	WHERE Customers.CustomerID = Orders.CustomerID
	GROUP BY CustomerID
	HAVING COUNT(*) = 0)

SELECT *
FROM Orders
WHERE CustomerID IN ('GROSR','LAZYK')
