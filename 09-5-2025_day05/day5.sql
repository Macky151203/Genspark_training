use dvdrental
select * from actor
--1 List all films with their length and rental rate, sorted by length descending.
select title,length,rental_rate from film order by length desc

--2 Find the top 5 customers who have rented the most films.
select b.customer_id,count(*) as totalrental from rental a join customer b on a.customer_id=b.customer_id group by b.customer_id order by totalrental desc limit 5;

--3 Display all films that have never been rented.
select a.film_id,a.title,c.rental_id from film a left join inventory b on a.film_id=b.film_id left join rental c on b.inventory_id=c.inventory_id where c.rental_id is null

--4 List all actors who appeared in the film ‘Academy Dinosaur’.
select a.title,concat(c.first_name,' ',c.last_name) as fullname from film a join film_actor b on a.film_id=b.film_id join actor c on b.actor_id=c.actor_id where a.title like 'Academy Dinosaur'

--5 List each customer along with the total number of rentals they made and the total amount paid.
select count(*) as total_rental,sum(c.amount) as totalamt,a.first_name from customer a join rental b on a.customer_id=b.customer_id join payment c on a.customer_id=c.customer_id group by a.customer_id

--6 Using a CTE, show the top 3 rented movies by number of rentals.
with toprentals as(
	select a.title as title,count(c.rental_id) as totalcount from film a join inventory b on a.film_id=b.film_id join rental c on b.inventory_id=c.inventory_id group by a.title
)
select title,totalcount from toprentals order by totalcount limit 3

--7 Find customers who have rented more than the average number of films.
with customerrental as (
	select customer_id,count(*) as total_rental from rental group by customer_id
)
select customer_id,total_rental from customerrental where total_rental>(select avg(total_rental) from customerrental) 

--8 Write a function that returns the total number of rentals for a given customer ID.
select * from rental;
create function getrentals(c_id int)
returns int as $$
declare
total_r int;
begin 
	select count(*) into total_r from rental where customer_id=getrentals.c_id;
	return total_r;
end
$$ LANGUAGE plpgsql;

drop function getrentals(integer);

select getrentals(222); 

--9 Write a stored procedure that updates the rental rate of a film by film ID and new rate.
create procedure updaterate(id int,newrate int)
language plpgsql
as $$
begin 
	update film set rental_rate=updaterate.newrate where film_id=updaterate.id;
end
$$

select film_id,rental_rate from film;
call updaterate(1,2)
select film_id,rental_rate from film where film_id=1;

--10 Write a procedure to list overdue rentals (return date is NULL and rental date older than 7 days)

create or replace procedure getoverdue()
language plpgsql
as $$
begin 
	select rental_id,rental_date,NOW()-rental_date as overdue from rental where return_date is null and NOW()-rental_date> INTERVAL '7 days';
end
$$
call getoverdue()


--afternoon session

--Triggers

--1 Write a trigger that logs whenever a new customer is inserted.
create or replace function triggerfunc()
returns trigger as $$
begin
	Raise Notice 'New customer with id-% inserted',New.customer_id;
	return New;
end;
$$ language plpgsql;

create trigger customerinsert 
after insert on customer
for each row
execute function triggerfunc();

insert into customer (store_id, first_name, last_name, email, address_id, activebool, create_date, last_update, active)
values (1, 'Kane', 'Harry', 'Harrykane@tot.com', 100, TRUE, '2025-05-09', '2025-05-09', 1);

--2 Create a trigger that prevents inserting a payment of amount 0.
create or replace function payment_trigger_func()
returns trigger as $$
begin
	if New.amount=0.00 then 
		raise exception 'Amount cannot be 0';
	end if;
	return New;
end;
$$ language plpgsql;

create or replace trigger paymentinsert 
after insert on payment
for each row
execute function payment_trigger_func();

insert into payment (payment_id, customer_id, staff_id, rental_id, amount, payment_date)
VALUES (1, 101, 1, 202, 0.00, '2025-05-09');
--for checking fk in staff table to avoid fk_constarint error
select * from staff;


--3 Set up a trigger to automatically set last_update on the film table before update.

create or replace function last_update_function()
returns trigger as $$
begin
	New.last_update=NOW();
	Raise notice 'Last updated set using trigger';
	return New;
end;
$$ language plpgsql;

create or replace trigger on_film_update
after update on film
for each row
execute function last_update_function();

select * from film where film_id=2;
update film set title='Ace' where film_id=2;

--4 Create a trigger to log changes in the inventory table (insert/delete).

create or replace function inventory_log()
returns trigger as $$
begin
	if TG_OP='INSERT' then
		Raise notice 'Data inserted to inventory with id %',New.inventory_id;
		return New;
	end if;
	if TG_OP='DELETE' then 
		Raise notice 'Data with id % has been deleted',old.inventory_id;
		return New;
	end if;
	return null;
end;
$$ language plpgsql;

create or replace trigger inv_insert
after insert on inventory
for each row 
execute function inventory_log();

create or replace trigger inv_delete
after delete on inventory
for each row 
execute function inventory_log();

select * from inventory;
select * from rental where inventory_id=4;
delete from payment where rental_id in (10883,14624);
delete from rental where inventory_id=4;
delete from inventory where inventory_id=4;


--cursor
--1 Write a cursor that loops through all films and prints titles longer than 120 minutes.
do $$
declare
    cursor_film cursor for select title from film where length > 120;
    title text;
begin
    for title in cursor_film loop
        raise notice 'Title: %', title;
    end loop;
end $$;

--2 Create a cursor that iterates through all customers and counts how many rentals each made.

do $$
declare
	cursor_rentals cursor for 
		select c.customer_id, c.first_name, c.last_name,count(*) as rental_count 
		from rental r
		join customer c on c.customer_id = r.customer_id
		group by c.customer_id, c.first_name, c.last_name
		order by c.customer_id;
    rec record;
begin
    for rec in cursor_rentals 
	loop
        raise notice 'cust_id: %, first_name: %, last name: %, count: %', 
		rec.customer_id, rec.first_name, rec.last_name, rec.rental_count;
    end loop;
end $$;

--3 Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.

do $$
declare
	cursor_rental_update cursor for
		select f.film_id, f.rental_rate from film f
		join(
			select film_id, count(rental_id) as rental_count
			from inventory i
			left join rental r on i.inventory_id = r.inventory_id
			group by film_id
		) rental_stats 
		on f.film_id = rental_stats.film_id
		where rental_stats.rental_count < 5;
	rec record;
begin
	for rec in cursor_rental_update loop
    	update film
    	set rental_rate = rec.rental_rate + 1
    	where film_id = rec.film_id;
		raise notice 'Updated film_id: %, new rental_rate: %', rec.film_id, rec.rental_rate + 1;
	end loop;
end $$;

--4 Create a function using a cursor that collects titles of all films from a particular category.
 
create or replace function films_by_category(cname text)
returns void as $$
declare
    cursor_films_by_category cursor for
        select f.title, c.name from film_category fc
        join film f on fc.film_id=f.film_id
        join category c on fc.category_id=c.category_id
        where c.name= cname;
    rec record;
begin
    for rec in cursor_films_by_category loop
        raise notice 'Title: %',rec.title;
    end loop;
end;
$$
language plpgsql

select * from category;
 
select * from films_by_category('Action');

--5 Loop through all stores and count how many distinct films are available in each store using a cursor.
do
$$
declare
    cursor_films_per_store cursor for
        select store_id, count(film_id) as film_count from inventory
        group by store_id;
    rec record;
begin
    for rec in cursor_films_per_store loop
        raise notice 'store id: %, count: %',rec.store_id, rec.film_count;
    end loop;
end;
$$


--Transactions

--1 Write a transaction that inserts a customer and an initial rental in one atomic operation.
DO $$
DECLARE new_customer_id INT;
BEGIN
    INSERT INTO public.customer (store_id, address_id, first_name, last_name, email, create_date)
    VALUES (1, 1, 'Amit', 'Cat', 'amitcat@smthg.com', NOW())
    RETURNING customer_id INTO new_customer_id;
 
    INSERT INTO public.rental (rental_date, inventory_id, customer_id, staff_id, return_date)
    VALUES (NOW(), 1, new_customer_id, 1, NULL);
END;
$$;
 
--2 Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.
DO $$
BEGIN
    UPDATE public.film 
    SET rental_rate = 5.11
    WHERE film_id = 1;
 
    INSERT INTO public.inventory (film_id, store_id) 
    VALUES (9999, 1);
 
EXCEPTION
    WHEN others THEN
        ROLLBACK;
        RAISE NOTICE 'Transaction rolled back due to error.';
END;
$$;
 
--3 Create a transaction that transfers an inventory item from one store to another.
DO $$
DECLARE v_inventory_id INT := 100;
DECLARE v_from_store_id INT := 1;
DECLARE v_to_store_id INT := 2;
BEGIN
    UPDATE public.inventory
    SET store_id = v_to_store_id
    WHERE inventory_id = v_inventory_id
    AND store_id = v_from_store_id;
 
    IF NOT FOUND THEN
        RAISE EXCEPTION 'Inventory item % not found in store %', v_inventory_id, v_from_store_id;
    END IF;
 
    RAISE NOTICE 'Inventory item % moved from store % to store %', v_inventory_id, v_from_store_id, v_to_store_id;
END;
$$;
 
--4 Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.
BEGIN;
 
UPDATE public.payment
SET amount = amount + 5
WHERE payment_id = 1;
 
SAVEPOINT payment_update;
 
UPDATE public.payment
SET amount = amount + 10
WHERE payment_id = 2;
 
ROLLBACK TO SAVEPOINT payment_update;
 
COMMIT;
 
--5 Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
DO $$
DECLARE v_customer_id INT := 100;
BEGIN
    DELETE FROM public.payment
    WHERE customer_id = v_customer_id;
 
    DELETE FROM public.rental
    WHERE customer_id = v_customer_id;
 
    DELETE FROM public.customer
    WHERE customer_id = v_customer_id;
END;
$$;




