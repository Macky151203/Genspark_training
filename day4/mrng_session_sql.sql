use new_db;

create table people
(id int primary key,
name nvarchar(20),
age int)

create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
  Begin try
	   declare @insertQuery nvarchar(max)

	   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
	   with(
	   FIRSTROW =2,
	   FIELDTERMINATOR='','',
	   ROWTERMINATOR = ''\n'')'

	   exec sp_executesql @insertQuery

	   insert into BulkInsertLog(filepath,status,message)
	   values(@filepath,'Success','Bulk insert completed')
  end try
  begin catch
		 insert into BulkInsertLog(filepath,status,message)
		 values(@filepath,'Failed',Error_Message())
  END Catch
end

select * from people;


create table BulkInsertLog
(LogId int identity(1,1) primary key,
FilePath nvarchar(1000),
status nvarchar(50) constraint chk_status Check(status in('Success','Failed')),
Message nvarchar(1000),
InsertedOn DateTime default GetDate())

truncate table people;

select * from people;

select * from BulkInsertLog;

proc_BulkInsert 'C:\Users\VC\Downloads\Data(in).csv';


use pubs;

with cteAuthors
as
(select au_id, concat(au_fname,' ',au_lname) author_name from authors)

select * from cteAuthors

select * from titles;
create or alter proc getdata(@pagenum int, @pagesize int)
as 
begin
	with PaginatedBooks as
	( select  title_id,title, price, ROW_NUMBER() over (order by price desc) as RowNum from titles)
	select * from PaginatedBooks where RowNum between(((@pagenum-1)*@pageSize) +1) and (@pagenum*@pageSize)
end

getdata 3,5;

select * from titles order by price desc offset 10 rows fetch next 5 rows only;


create function  fn_CalculateTax(@baseprice float, @tax float)
returns float
as
begin
   return (@baseprice +(@baseprice*@tax/100))
end

select title,price,dbo.fn_CalculateTax(price,10) as totalprice from titles;

create function fn_tableSample(@minprice float)
returns table
as
  return select title,price from titles where price>= @minprice


select * from dbo.fn_tableSample(10)