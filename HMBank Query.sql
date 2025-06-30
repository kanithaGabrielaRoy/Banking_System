--Tasks 1: Database Design:  

--1. Create the database named "HMBank"  
create database HMBank

--2. Define the schema for the Customers, Accounts, and Transactions tables based on the provided schema. 

--create database tables:

--1. Create table 'Customers':
create table Customers(
customer_id int primary key,
first_name varchar(20),
last_name varchar(20),
DOB date,
email varchar(40),
phone_number varchar(20) ,
address varchar(200))

--2. Create table 'Accounts':
create table Accounts(
account_id varchar(20) primary key,
customer_id int not null
foreign key references Customers(customer_id),
account_type varchar(40),
balance int)

--3. Create table 'Transactions':
create table Transactions(
transaction_id int primary key,
account_id varchar(20) not null
foreign key references Accounts(account_id),
transaction_type varchar(30),
amount decimal ,
transaction_date date)


--Tasks 2: Select, Where, Between, AND, LIKE: 

----1. Insert at least 10 sample records into each of the following tables.   
-----------------• Customers  
-----------------• Accounts 
-----------------• Transactions

----Customers :
insert into Customers(customer_id,first_name,last_name,DOB,email,phone_number,address) values(1,'Kani',null,'2004-03-18','kani@gmail.com','8733542981','Chennai')
insert into Customers(customer_id,first_name,last_name,DOB,email,phone_number,address) values(2,'Timothy','Roy','2006-09-16','roy234@gmail.com','9823776447','Madurai')
insert into Customers(customer_id,first_name,last_name,DOB,email,phone_number,address) values(3,'Gabriela','Roy','2003-05-24','gabi24@gmail.com','6348284116','Chennai')

insert into Customers(customer_id,first_name,last_name,DOB,email,phone_number,address) values(4,'Sharon','Joy','2000-07-28','sharon_joy@gmail.com','7452879034','Trichy'),
                                                                                               (5,'Saanvika','Shree','1996-02-10','ss1996@gmail.com','4529056239','Salem'),                                                
                                                                                                (6,'Rani','Lakshmi','1974-01-02','rani02lak@gmail.com','8148653361','Madurai'),
                                                                                                  (7,'Reena','Joseph','2001-08-22','reenajoseph@gmail.com','9443716886','Bangalore'),
                                                                                                    (8,'Lavanya','Ravikumar','1992-07-17','lav1770@gmail.com','4578658834','Salem'),
                                                                                                      (9,'Daniel',null,'1999-11-23','daniel1999@gmail.com','9236647210','Dindugal'),
                                                                                                        (10,'Nancee','Varsha','1996-12-26','nancee_26@gmail.com','7205271528','Thirumangalam')



----Accounts :
insert into Accounts(account_id,customer_id,account_type,balance) values('HM000111',1,'savings',500),
                                                              ('HM000101',2,'current',4000),
                                                                ('HM000110',3,'savings',0),
                                                                  ('HM001001',4,'zero_balance',5000),
                                                                    ('HM010111',5,'current',10500),
                                                                      ('HM010101',6,'current',70),
                                                                        ('HM100111',7,'zero_balance',4300),
                                                                          ('HM100110',8,'savings',63000),
                                                                            ('HM000100',9,'zero_balance',900),
                                                                              ('HM110111',10,'current',1800)


----Transactions :
insert into Transactions(transaction_id,account_id,transaction_type,amount,transaction_date) values(1,'HM000111','deposit',5500,'2025-06-11'),
                                                                                                     (2,'HM000101','transfer',600,'2025-04-17'),
                                                                                                       (3,'HM000110','deposit',30000,'2025-03-25'),
                                                                                                         (4,'HM001001','withdrawal',1000,'2025-05-12'),
                                                                                                           (5,'HM010111','transfer',2500,'2025-04-16'),
                                                                                                             (6,'HM010101','deposit',5000,'2025-05-28'),
                                                                                                               (7,'HM100111','withdrawal',1300,'2025-05-10'),
                                                                                                                 (8,'HM100110','withdrawal',33000,'2025-03-30'),
                                                                                                                   (9,'HM000100','transfer',100,'2025-06-03'),
                                                                                                                     (10,'HM110111','deposit',10000,'2025-05-23'),
                                                                                                                       (11,'HM001001','deposit',13000,'2025-06-03'),
                                                                                                                         (12,'HM100111','transfer',150,'2025-06-01'),
                                                                                                                           (13,'HM110111','withdrawal',1500,'2025-06-10')


---2. Write SQL queries for the following tasks: 

-----1. Write a SQL query to retrieve the name, account type and email of all customers.  

select c.first_name, c.last_name, c.email, a.account_type
from Customers c
join Accounts a 
on c.customer_id = a.customer_id

-----2. Write a SQL query to list all transaction corresponding customer. 

select c.first_name,c.last_name,t.*
from Transactions t
join Accounts a 
on t.account_id = a.account_id
join Customers c 
on a.customer_id = c.customer_id


-----3. Write a SQL query to increase the balance of a specific account by a certain amount. 
------Consider increasing it by 650

update Accounts set balance = balance+650 
where account_id = 'HM010101'


-----4. Write a SQL query to Combine first and last names of customers as a full_name. 

select concat(first_name,'  ',last_name) 
as full_name from Customers

-----5. Write a SQL query to remove accounts with a balance of zero where the account type is savings. 

delete from Accounts 
where balance = 0 and account_type = 'savings' 
select * from Accounts

-----6. Write a SQL query to Find customers living in a specific city. 
---- Consider finding customers from 'Madurai'

select * from Customers 
where Address ='Madurai'

-----7. Write a SQL query to Get the account balance for a specific account. 

select balance
from Accounts 
where account_id = 'HM100110'

-----8. Write a SQL query to List all current accounts with a balance greater than $1,000. 

select * from Accounts 
where balance > 1000

-----9. Write a SQL query to Retrieve all transactions for a specific account. 

select * from Transactions 
where account_id = 'HM100111'

-----10. Write a SQL query to Calculate the interest accrued on savings accounts based on a given interest rate. 
---Consider interest as 10%

select account_id, balance, balance * 0.10 as interest
from Accounts
where account_type = 'savings'

-----11. Write a SQL query to Identify accounts where the balance is less than a specified overdraft limit.
-------Consider the minimum balance to be maintained is 600

select * from Accounts 
where balance < 600


-----12. Write a SQL query to Find customers not living in a specific city.
-------Consider the customers not living in Bangalore

select * from Customers 
where address!='Bangalore'

--Tasks 3: Aggregate functions, Having, Order By, GroupBy and Joins: 


-----1. Write a SQL query to Find the average account balance for all customers. 

select Avg(balance) as [Average Account Balance]
from Accounts

-----2. Write a SQL query to Retrieve the top 10 highest account balances.  

select top 10 * from Accounts
order by balance desc 


-----3. Write a SQL query to Calculate Total Deposits for All Customers in specific date. 

select SUM(amount) as total_amount
from Transactions
where transaction_type = 'deposit' and transaction_date = '2025-06-11'


-----4. Write a SQL query to Find the Oldest and Newest Customers. (Categorizing the customers based on DOB)

select * from Customers
where DOB = (select MAX(DOB) from Customers) 
or DOB = (select MIN(DOB) from Customers) 


-----5. Write a SQL query to Retrieve transaction details along with the account type. 

select t.*, a.account_type
from Transactions t
join Accounts a 
on t.account_id = a.account_id

-----6. Write a SQL query to Get a list of customers along with their account details. 

select concat(first_name,'  ',last_name) as Customer_name, a.*
from Customers c
join Accounts a 
on c.customer_id = a.customer_id

-----7. Write a SQL query to Retrieve transaction details along with customer information for a specific account.

select t.*, c.*
from Transactions t
join Accounts a 
on t.account_id = a.account_id
join Customers c 
on a.customer_id = c.customer_id
where t.account_id = 'HM001001'


-----8. Write a SQL query to Identify customers who have more than one account. 

select customer_id
from Accounts
group by customer_id
having count(account_id) > 1


-----9. Write a SQL query to Calculate the difference in transaction amounts between deposits and withdrawals. 

select (SUM transaction_type ='deposit') - SUM (transaction_type ='withdrawal')  AS difference
FROM Transactions

-----10. Write a SQL query to Calculate the average daily balance for each account over a specified period. 

select a.account_id,
avg(a.balance) as average_daily_balance
from Accounts a
join Transactions t 
on a.account_id = t.account_id
where t.transaction_date BETWEEN '2025-05-01' AND '2025-06-07'
group by a.account_id
order by average_daily_balance desc

-----11. Calculate the total balance for each account type.

select account_type, SUM(balance) as total_balance
from Accounts
group by account_type

-----12. Identify accounts with the highest number of transactions order by descending order. 

select account_id, count(*) 
as transaction_count
from Transactions
group by account_id
order by transaction_count desc


-----13. List customers with high aggregate account balances, along with their account types. 

select c.customer_id,c.first_name,c.last_name,a.account_type,sum(a.balance) as total_balance
from Customers c
join Accounts a 
on c.customer_id = a.customer_id
group by c.customer_id, c.first_name, c.last_name, a.account_type
having sum(a.balance) > 10000 
order by total_balance desc

-----14. Identify and list duplicate transactions based on transaction amount, date, and account.

select account_id,amount,transaction_date,count(*)
from Transactions
group by account_id, amount, transaction_date
having count(*) > 1 




--Tasks 4: Subquery and its type: 


-----1. Retrieve the customer(s) with the highest account balance. 

select * from Customers
where customer_id IN (
select customer_id
from Accounts
where balance = (select max(balance) from Accounts))

-----2. Calculate the average account balance for customers who have more than one account. 

select avg(balance) as avg_balance
from Accounts
where customer_id IN (
select customer_id
from Accounts
group by customer_id
having count(*) > 1)

-----3. Retrieve accounts with transactions whose amounts exceed the average transaction amount. 

select distinct account_id
from Transactions
where amount > (select avg(amount) from Transactions)

-----4. Identify customers who have no recorded transactions. 

select * from Customers
where customer_id NOT IN (
select distinct customer_id
from Accounts a
join Transactions t 
on a.account_id = t.account_id)

-----5. Calculate the total balance of accounts with no recorded transactions. 

select sum(balance) as Total_balance
from Accounts
where account_id NOT IN (
select distinct  account_id from Transactions)

-----6. Retrieve transactions for accounts with the lowest balance. 

select * from Transactions
where account_id IN (
select account_id
from Accounts
where balance = (select min(balance) from Accounts))

-----7. Identify customers who have accounts of multiple types. 

select customer_id
from Accounts
group by customer_id
having count(distinct account_type) > 1

-----8. Calculate the percentage of each account type out of the total number of accounts. 

select account_type,
count(*) * 100.0/(select count(*) from Accounts) as percentage
from Accounts
group by account_type

-----9. Retrieve all transactions for a customer with a given customer_id. 

select a.customer_id,t.*
from Transactions t
join Accounts a 
on t.account_id = a.account_id
where a.customer_id = 7

-----10. Calculate the total balance for each account type, including a subquery within the SELECT clause.

select distinct account_type,
(select sum(balance) 
from Accounts a2 
where a2.account_type = a1.account_type) 
as total_balance
from Accounts a1
