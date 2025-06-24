use pubs;
select * from titles;

--publisher details with no books published
select * from publishers where pub_id not in (select distinct pub_id from titles);

--select author details with title
select t.title_id,t.title,a.au_id,concat(b.au_fname,' ',b.au_lname) as author_name from titleauthor a join titles t on a.title_id=t.title_id join authors b on b.au_id=a.au_id ;
select title,au_id from titleauthor a join titles t on a.title_id=t.title_id;

--publisher name,book name and order date
select a.pub_name,b.title,c.ord_date from publishers a join titles b on a.pub_id=b.pub_id join sales c on c.title_id=b.title_id;

--first book date for publisher
select a.pub_name,min(c.ord_date) as first_order_date from publishers a join titles b on a.pub_id=b.pub_id join sales c on c.title_id=b.title_id group by a.pub_name;
select a.pub_name,min(c.ord_date) as first_order_date from publishers a left outer join titles b on a.pub_id=b.pub_id left outer join sales c on c.title_id=b.title_id group by a.pub_name order by 2 desc;

--bookname and store address
select a.title,c.stor_address as store_address from titles a join sales b on a.title_id=b.title_id join stores c on b.stor_id=c.stor_id order by 1;

--procedure
create procedure procedure_1
as
begin
	print 'hmm'
end

exec procedure_1

create table Products(
	id int identity(1,1) constraint pk_productId primary key,
	name varchar(100) not null,
	details nvarchar(max) not null
)

create or alter procedure InsertToProducts (@name varchar(100), @details nvarchar(max))
as 
begin
	insert into Products(name,details) values(@name,@details)
end

InsertToProducts 'Laptop','{"brand":"Dell","spec":{"ram":"16GB","cpu":"i5"}}'

select * from Products;

select JSON_QUERY(details,'$.spec') Product_specs from Products;

create procedure UpdateSpec(@pid int,@newvalue varchar(20))
as 
begin
	update Products set details=JSON_MODIFY(details,'$.spec.ram',@newvalue) where id=@pid
end

UpdateSpec 1,'128GB'

select id,name,JSON_QUERY(details,'$.spec') Brand from Products;
select id,name,JSON_VALUE(details,'$.brand') Brand from Products;

create table Posts(
	id int primary key,
	title nvarchar(100),
	user_id int,
	body nvarchar(max))

create proc BulkJson(@jsondata nvarchar(max))
as
begin
	insert into Posts(user_id,id,title,body)
	select userId,id,title,body from openjson(@jsondata)
	with(userId int,id int,title nvarchar(100),body nvarchar(max))
end

BulkJson '
[
  {
    "userId": 2,
    "id": 19,
    "title": "adipisci placeat illum aut reiciendis qui",
    "body": "illum quis cupiditate provident sit magnam\nea sed aut omnis\nveniam maiores ullam consequatur atque\nadipisci quo iste expedita sit quos voluptas"
  },
  {
    "userId": 2,
    "id": 20,
    "title": "doloribus ad provident suscipit at",
    "body": "qui consequuntur ducimus possimus quisquam amet similique\nsuscipit porro ipsam amet\neos veritatis officiis exercitationem vel fugit aut necessitatibus totam\nomnis rerum consequatur expedita quidem cumque explicabo"
  },
  {
    "userId": 3,
    "id": 21,
    "title": "asperiores ea ipsam voluptatibus modi minima quia sint",
    "body": "repellat aliquid praesentium dolorem quo\nsed totam minus non itaque\nnihil labore molestiae sunt dolor eveniet hic recusandae veniam\ntempora et tenetur expedita sunt"
  }
]'

select * from Posts;

select * from Products where JSON_VALUE(details,'$.spec.cpu')='i5';
select * from Products where try_cast(JSON_VALUE(details,'$.spec.cpu') as nvarchar(20))='i5';

create proc GetPost(@user_id int)
as 
begin
	select * from Posts where user_id=@user_id;
end

GetPost 1
