select title from titles;

select title from titles where pub_id=1389;

select title from titles where price between 10 and 15;

select title from titles where price is null;

select title from titles where title like 'The%';

select title from titles where title not like '%v%';

select * from titles order by royalty;

select * from titles order by pub_id desc,type asc, price desc;

select avg(price) as avg_price,type from titles group by type;

--10th
select distinct type from titles;

--11
select top 2 price from titles order by price desc;

--12
select title from titles where type='business' and price<20 and advance>7000;

--13
select pub_id, count(*) as book_count from titles where price between 10 and 25 and title like '%It%' group by pub_id having count(*) >=2 order by book_count;

--14
select * from authors where state='CA';

--15
select state,count(*) as auth_count from authors group by state;