--SELECT FName, LName, Date_Joined, Salary
--FROM STAFF
USE [Northwind]
GO

--Задание 1.1. Простая фильтрация данных.

--1.Выбрать в таблице Orders заказы, которые были доставлены после 
--6 мая 1998 года (колонка ShippedDate) включительно и которые доставлены 
--с ShipVia >= 2. Запрос должен возвращать только колонки OrderID, ShippedDate и ShipVia. 
SELECT OrderId, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate >= '1998-05-06' AND ShipVia >= 2

--2.Написать запрос, который выводит только недоставленные заказы из таблицы Orders. 
--В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’ 
--(использовать системную функцию CASЕ). 
--Запрос должен возвращать только колонки OrderID и ShippedDate.
SELECT OrderID, ShippedDate =
CASE
	WHEN ShippedDate IS NULL THEN 'Not Shipped'
END
FROM Orders
WHERE ShippedDate IS NULL

--3.Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года (ShippedDate) 
--не включая эту дату или которые еще не доставлены. В запросе должны возвращаться только колонки OrderID (переименовать в Order Number) и 
--ShippedDate (переименовать в Shipped Date). В результатах запроса возвращать для колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, 
--для остальных значений возвращать дату в формате по умолчанию.
SELECT OrderId AS 'Order Number',
CASE
	WHEN ShippedDate IS NULL THEN 'Not Shipped' 
END AS 'Shipped Date'
FROM Orders
WHERE ShippedDate > '1998-05-06' OR ShippedDate IS NULL

--Задание 1.2. Использование операторов IN, DISTINCT, ORDER BY, NOT

--1.Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada. 
--Запрос сделать с только помощью оператора IN. Возвращать колонки с именем пользователя и названием страны в результатах запроса. 
--Упорядочить результаты запроса по имени заказчиков и по месту проживания.
SELECT CustomerID, Country
FROM Customers
WHERE Country IN ('USA', 'CANADA')
ORDER BY CustomerID, Country

--2.Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada.
-- Запрос сделать с помощью оператора IN. Возвращать колонки с именем пользователя и названием страны в результатах запроса. 
--Упорядочить результаты запроса по имени заказчиков.
SELECT CustomerID, Country
FROM Customers
WHERE Country NOT IN ('USA', 'CANADA')
ORDER BY CustomerID

--3.Выбрать из таблицы Customers все страны, в которых проживают заказчики. 
--Страна должна быть упомянута только один раз и список отсортирован по убыванию. 
--Не использовать предложение GROUP BY. Возвращать только одну колонку в результатах запроса. 
SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC

--Задание 1.3. Использование оператора BETWEEN, DISTINCT

--1.Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться),
-- где встречаются продукты с количеством от 3 до 10 включительно – это колонка Quantity в таблице Order Details. Использовать оператор BETWEEN. 
--Запрос должен возвращать только колонку OrderID.
SELECT DISTINCT OrderId
FROM [dbo].[Order Details]
WHERE Quantity BETWEEN 3 AND 10

--2.Выбрать всех заказчиков из таблицы Customers, 
--у которых название страны начинается на буквы из диапазона b и g. Использовать оператор BETWEEN.
-- Проверить, что в результаты запроса попадает Germany. 
--Запрос должен возвращать только колонки CustomerID и Country и отсортирован по Country.
SELECT CustomerID, Country
FROM Customers
WHERE Country BETWEEN 'b' AND 'h'
ORDER BY Country

--3.Выбрать всех заказчиков из таблицы Customers, у которых название 
--страны начинается на буквы из диапазона b и g, не используя оператор BETWEEN. 
SELECT CustomerID, Country
FROM Customers
WHERE Country >= 'b' AND Country <= 'h'
ORDER BY Country

--Задание 1.4. Использование оператора LIKE

--1.В таблице Products найти все продукты (колонка ProductName), где встречается подстрока 'chocolade'.
-- Известно, что в подстроке 'chocolade' может быть изменена 
--одна буква 'c' в середине - найти все продукты, которые удовлетворяют этому условию. 
SELECT *
FROM Products
WHERE ProductName LIKE '%cho_olade%'

--Задание 2.1. Использование агрегатных функций (SUM, COUNT)

--1.Найти общую сумму всех заказов из таблицы Order Details с учетом количества закупленных товаров и скидок по ним. 
--Результатом запроса должна быть одна запись с одной колонкой с названием колонки 'Totals'.
SELECT SUM(UnitPrice * Quantity * (1 - Discount)) AS 'Totals'
FROM [dbo].[Order Details]

--2.По таблице Orders найти количество заказов, которые еще не были доставлены 
--(т.е. в колонке ShippedDate нет значения даты доставки). Использовать при этом запросе только оператор COUNT. 
--Не использовать предложения WHERE и GROUP.
SELECT COUNT
(CASE WHEN ShippedDate IS NULL THEN 1 ELSE NULL END)
FROM Orders

--3.По таблице Orders найти количество различных покупателей (CustomerID), 
--сделавших заказы. Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
SELECT COUNT(DISTINCT CustomerId)
FROM Orders

--Задание 2.2. Соединение таблиц, использование агрегатных функций и предложений GROUP BY и HAVING 

--1.По таблице Orders найти количество заказов с группировкой по годам. 
--В результатах запроса надо возвращать две колонки c названиями Year и Total.
-- Написать проверочный запрос, который вычисляет количество всех заказов.
SELECT COUNT(*) AS 'Total', datepart(yyyy, [OrderDate]) AS 'Year'
FROM Orders
GROUP BY datepart(yyyy, [OrderDate])

SELECT COUNT(*)
FROM Orders

--По таблице Orders найти количество заказов, cделанных каждым продавцом. 
--Заказ для указанного продавца – это любая запись в таблице Orders, где в колонке EmployeeID 
--задано значение для данного продавца. В результатах запроса надо возвращать колонку с именем
-- продавца (Должно высвечиваться имя полученное конкатенацией LastName & FirstName. 
--Эта строка LastName & FirstName должна быть получена отдельным запросом в колонке основного запроса. 
--Также основной запрос должен использовать группировку по EmployeeID.) с названием колонки ‘Seller’ и 
--колонку c количеством заказов возвращать с названием 'Amount'. 
--Результаты запроса должны быть упорядочены по убыванию количества заказов.
SELECT COUNT(Orders.EmployeeID) AS 'Amount',
	(SELECT CONCAT(Employees.FirstName, ' ', Employees.LastName) 
     FROM Employees 
	 WHERE Orders.EmployeeID = Employees.EmployeeID)  AS 'Seller'
FROM Orders
JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID
GROUP BY Orders.EmployeeID, Employees.FirstName
ORDER BY 'Amount' DESC

--3.По таблице Orders найти количество заказов, сделанных каждым продавцом и для каждого покупателя. 
--Необходимо определить это только для заказов, сделанных в 1998 году. 

SELECT EmployeeID, CustomerID, COUNT(*)
FROM Orders
WHERE datepart(yyyy, [OrderDate]) = '1998'
GROUP BY EmployeeID, CustomerID
ORDER BY EmployeeID

--4.Найти покупателей и продавцов, которые живут в одном городе. Если в городе живут только один или 
--несколько продавцов, или только один или несколько покупателей, то информация о таких покупателя и 
--продавцах не должна попадать в результирующий набор. Не использовать конструкцию JOIN. 

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

--5.Найти всех покупателей, которые живут в одном городе. 
SELECT City, CustomerID
FROM Customers
GROUP BY City, CUstomerID

--6.По таблице Employees найти для каждого продавца его руководителя.
SELECT EmployeeID, ReportsTo
FROM Employees

--Задание 2.3. Использование JOIN
--1.Определить продавцов, которые обслуживают регион 'Western' (таблица Region). 

SELECT Employees.FirstName, Region.RegionDescription
FROM Employees
JOIN Region ON Region.RegionDescription = Employees.Region

--2.Выдать в результатах запроса имена всех заказчиков из таблицы Customers и суммарное количество 
--их заказов из таблицы Orders. Принять во внимание, что у некоторых заказчиков нет заказов, но 
--они также должны быть выведены в результатах запроса. 
--Упорядочить результаты запроса по возрастанию количества заказов.

SELECT Customers.ContactName, COUNT(Orders.OrderID) AS 'Count'
FROM Customers
JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.ContactName
ORDER BY 'Count'

------Задание 2.4. Использование подзапросов

--Выдать всех поставщиков (колонка CompanyName в таблице Suppliers), 
--у которых нет хотя бы одного продукта на складе (UnitsInStock в таблице Products равно 0). 
--Использовать вложенный SELECT для этого запроса с использованием оператора IN. 
SELECT CompanyName 
FROM Suppliers
WHERE Suppliers.SupplierID IN
	(SELECT SupplierID
	FROM Products
	WHERE UnitsInStock = 0) 

SELECT *
FROM Products
WHERE UnitsInStock = 0

--2.Выдать всех продавцов, которые имеют более 150 заказов. Использовать вложенный SELECT.

SELECT COUNT(OrderID), EmployeeID, 
	(SELECT FirstName
	FROM Employees
	WHERE Employees.EmployeeID = Orders.EmployeeID)
FROM Orders
GROUP BY EmployeeID
HAVING COUNT(OrderID) > 150

--3.Выдать всех заказчиков (таблица Customers), которые не
-- имеют ни одного заказа (подзапрос по таблице Orders). Использовать оператор EXISTS.

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
