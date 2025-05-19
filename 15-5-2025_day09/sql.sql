--1.Create a stored procedure to encrypt a given text

CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE OR REPLACE PROCEDURE encrypt(
	IN inp text,
	IN secretkey text,
	OUT enc_text text
)
LANGUAGE plpgsql AS $$
BEGIN
	enc_text:=pgp_sym_encrypt(inp,secretkey);
	RAISE NOTICE 'encrypted text is- %',enc_text;
END;
$$;
DROP PROCEDURE encrypt(text,text);
CALL encrypt('abc@gmail.com','yoichiisagi',NULL);

--\xc30d0407030291c0ef0bd67cb7ba6ed23e01f46fed99746cb21f08a4c3322cd6dbcbecaf8cf02a0d19e4df6d6418c9c8f9803679d0c0eb13d81ec4500c881d347c9b8b90c5b4920d65937c4212efc5

--2. compare 2 encrypted texts

CREATE OR REPLACE FUNCTION cmp(text1 text,text2 text,secretkey text)
RETURNS boolean
LANGUAGE plpgsql AS $$
DECLARE p1 text; p2 text;
BEGIN 
	p1:=pgp_sym_decrypt(text1::bytea, secretkey);
	p2:=pgp_sym_decrypt(text2::bytea, secretkey);
	RETURN p1=p2;
END;
$$;


DO $$
DECLARE res1 boolean; res2 boolean;
BEGIN
	
res1:=cmp('\xc30d0407030291c0ef0bd67cb7ba6ed23e01f46fed99746cb21f08a4c3322cd6dbcbecaf8cf02a0d19e4df6d6418c9c8f9803679d0c0eb13d81ec4500c881d347c9b8b90c5b4920d65937c4212efc5
','\xc30d04070302fd3a1311bfd746be63d23d013f2d30e9377cc3c869210efc86fe2900023160e8606fa7b5c105a85f12d7869cecfedc2629a62cc60355b3d5b955cb00259d4d8a1719482580056964','yoichiisagi');
res2:=cmp('\xc30d0407030291c0ef0bd67cb7ba6ed23e01f46fed99746cb21f08a4c3322cd6dbcbecaf8cf02a0d19e4df6d6418c9c8f9803679d0c0eb13d81ec4500c881d347c9b8b90c5b4920d65937c4212efc5
','\xc30d040703029185669ec382f2fd70d23e018dcc91acc71d4a9e1320371248701554423b915d8a203ec88bae78ef436b7a44c3415349c3fb78f66e798f42b2b8588bcf7c903499a012c30521becbdc','yoichiisagi');
RAISE NOTICE 'the res are % and %',res1,res2;

END;
$$;

--3 Create a stored procedure to partially mask a given text

CREATE OR REPLACE PROCEDURE sp_mask_text(
    IN input_text TEXT,
    OUT masked_text TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    middle_length INT;
BEGIN
    IF length(input_text) <= 4 THEN
        masked_text := input_text;
    ELSE
        middle_length := length(input_text) - 4;
        masked_text := 
            substring(input_text FROM 1 FOR 2) || 
            repeat('*', middle_length) || 
            substring(input_text FROM length(input_text) - 1 FOR 2);
    END IF;
END;
$$;

call sp_mask_text('Thisisauser',null);

--4. Create a procedure to insert into customer with encrypted email and masked name

create table customer(
	id integer primary key,
	firstname text,
	email text
)

CREATE OR REPLACE FUNCTION sp_encrypt_text(
    plain_text TEXT,
    secret_key TEXT
)
RETURNS BYTEA
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN pgp_sym_encrypt(plain_text, secret_key);
END;
$$;

CREATE OR REPLACE PROCEDURE sp_insert_customer_secure(
	IN inpid INTEGER,
    IN first_name TEXT,
    IN email TEXT,
    IN secret_key TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
    masked_name TEXT;
    encrypted_email BYTEA;
BEGIN
    CALL sp_mask_text(first_name, masked_name);
    encrypted_email := sp_encrypt_text(email, secret_key);
    INSERT INTO customer (id, firstname, email)
    VALUES (inpid, masked_name, encrypted_email);
END;
$$;

call sp_insert_customer_secure(2,'salahisgoat','ballondoe@winner.com','post');

SELECT * FROM customer;

--5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
 

CREATE OR REPLACE PROCEDURE sp_read_customer_masked(secret_key TEXT)
LANGUAGE plpgsql
AS $$
DECLARE
    rec RECORD;
    decrypted_email TEXT;
	cr cursor for SELECT id, firstname, email FROM customer;
BEGIN
    FOR rec IN cr LOOP
        decrypted_email := pgp_sym_decrypt(rec.email::BYTEA, secret_key);
        RAISE NOTICE 'ID: %, Name: %, Email: %', rec.id, rec.firstname, decrypted_email;
    END LOOP;
END;
$$;

CALL sp_read_customer_masked('post');






