use northwind;
--1List all orders with the customer name and the employee who handled the order.
select a.ContactName, c.Firstname from Customers a join Orders b on a.CustomerID=b.CustomerID join Employees c on c.EmployeeID=b.EmployeeID;

--2Get a list of products along with their category and supplier name.
select a.ProductName ,b.CategoryName, c.ContactName as Suppliername from Products a join Categories b on a.CategoryID=b.CategoryID join Suppliers c on c.SupplierID=a.SupplierID;

--3 Show all orders and the products included in each order with quantity and unit price.
select a.OrderId,b.Quantity,b.UnitPrice,c.ProductName from Orders a join [Order Details] b on a.OrderID=b.OrderID join Products c on c.ProductID=b.ProductID;

--4 List employees who report to other employees (manager-subordinate relationship).
select a.FirstName,a.ReportsTo from Employees a join Employees b on a.ReportsTo=b.EmployeeID;

--5 Display each customer and their total order count.
select b.CustomerID, Count(*) as total_orders from Orders a join Customers b on a.CustomerID=b.CustomerID group by b.CustomerID; 

--6 Find the average unit price of products per category.
select avg(a.UnitPrice) , c.CategoryName from [Order Details] a join Products b on a.ProductID = b.ProductID join Categories c on b.CategoryID=c.CategoryID group by c.CategoryName;

--7 List customers where the contact title starts with 'Owner'.
select * from Customers where ContactTitle like 'Owner%';

--8 Show the top 5 most expensive products.
Select Top 5 a.ProductName , b.UnitPrice from Products a join [Order Details] b on a.ProductID=b.ProductID order by b.UnitPrice desc;

--9 Return the total sales amount (quantity × unit price) per order.
select a.OrderId,sum(b.UnitPrice*b.Quantity) as totalsales from Orders a join [Order Details] b on a.OrderID=b.OrderID group by a.OrderID;

--10 Create a stored procedure that returns all orders for a given customer ID.
select * from Customers;
create or alter procedure proc_GetOrderByCustomerId (@customerid nvarchar(20))
as
begin
    select * from Orders where CustomerID=@customerid
end
go
proc_GetOrderByCustomerId 'BERGS'

--11 Write a stored procedure that inserts a new product.
create or alter procedure proc_AddProduct
(@name nvarchar(20), @sid int,@cid int, @qnt nvarchar(100), @up int, @uis int, @uoo int, @rol int, @discontinued int)
as
begin
    insert into Products(ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued)
    values (@name,@sid,@cid,@qnt,@up,@uis,@uoo,@rol,@discontinued)
end
 
proc_AddProduct 'Orange', 1,1,'5 boxes x 10 bags', 20.0, 40, 0, 10,0



--12 Create a stored procedure that returns total sales per employee.
create or alter proc gettotalsales
as 
begin
	select a.EmployeeId,sum(c.UnitPrice*c.Quantity) as totalsales from Employees a join Orders b on a.EmployeeID=b.EmployeeID join [Order Details] c on c.OrderID=b.OrderID group by a.EmployeeID;
end

gettotalsales


select a.EmployeeId,sum(c.UnitPrice*c.Quantity) as totalsales from Employees a join Orders b on a.EmployeeID=b.EmployeeID join [Order Details] c on c.OrderID=b.OrderID group by a.EmployeeID;

--13 Use a CTE to rank products by unit price within each category.
with RankedProducts as
( select ProductID, ProductName, CategoryID,
  ROW_NUMBER() over (partition by CategoryID order by UnitPrice desc) AS RankWithinCategory
  from Products       
)
 
Select * from RankedProducts
order by CategoryID, RankWithinCategory;

sp_help Products;

--14 Create a CTE to calculate total revenue per product and filter products with revenue > 10,000.
with ProductRevenue as(
	select ProductId,sum(unitPrice*Quantity) as total_revenue from [Order Details] group by ProductID
)
select a.total_revenue, b.ProductName from ProductRevenue a join Products b on a.ProductID=b.ProductID where a.total_revenue>10000;


--15 Use a CTE with recursion to display employee hierarchy.

with EmployeeHierarchy as (
    select EmployeeID, FirstName, LastName, ReportsTo, 0 as hierarchy_level from Employees where ReportsTo IS NULL
 
    union all
	
	select e.EmployeeID, e.FirstName, e.LastName, e.ReportsTo, eh.hierarchy_level + 1 from Employees e inner join EmployeeHierarchy eh ON e.ReportsTo = eh.EmployeeID
)
 
select EmployeeID, FirstName, LastName, ReportsTo, hierarchy_level from EmployeeHierarchy order by hierarchy_level, EmployeeID;