drop table if exists vote_info;
drop table if exists item;
drop table if exists ballot_item;
drop table if exists election;
drop table if exists voter;

create table voter(
voter_id int primary key,
first_name varchar(255),
last_name varchar(255),
birthday DATE,
ss_number int,
password varchar(255),
admin_check BIT,
);

create table vote_info(
vote_info_id int identity primary key,
voter_id int,
election_id int,
votes varchar(255)
);

create table election(
election_id int primary key,
start_date Date,
end_date Date
);

create table ballot_item(
ballot_item_id int primary key,
election_id int,
issue varchar(255),
victory_id int
);

create table item(
item_id int primary key,
title varchar(255),
party varchar(255),
ballot_id int
);

Insert into voter(voter_id,first_name,last_name,birthday,ss_number,password,admin_check) Values(1,'Matthew','Donsig','2003-04-08',1234,'password',0);
Insert into voter(voter_id,first_name,last_name,birthday,ss_number,password,admin_check) Values(2,'Steve','Smith','1985-09-08',4321,'12hjrah!',0);
Insert into voter(voter_id,first_name,last_name,birthday,ss_number,password,admin_check) Values(3,'John','Peterson','2000-01-12',5678,'@eiwohf&7',0);
Insert into voter(voter_id,first_name,last_name,birthday,ss_number,password,admin_check) Values(4,'Eli','Vanto','1999-06-12',7694,'MrAdmin!43',1);

Insert into election(election_id, start_date, end_date) Values(1,'2023-11-1', '2023-12-25');
Insert into ballot_item(ballot_item_id,election_id,issue,victory_id) Values(1, 1, 'Mayor', null);
Insert into ballot_item(ballot_item_id,election_id,issue,victory_id) Values(2, 1, 'Issue 21', null);
Insert into item(item_id,title,party,ballot_id) Values(1, 'Allan Witham', 'Democrat', 1);
Insert into item(item_id,title,party,ballot_id) Values(2, 'J.P. Williams', 'Republican', 1);
Insert into item(item_id,title,party,ballot_id) Values(3, 'Yes', 'N/A', 2);
Insert into item(item_id,title,party,ballot_id) Values(4, 'No', 'N/A', 2);

Insert into vote_info(voter_id,election_id,votes) Values(1,1,'2,3');
Insert into vote_info(voter_id,election_id,votes) Values(3,1,'1,3');

Insert into election(election_id, start_date, end_date) Values(2,'2023-12-1', '2024-02-02');
Insert into ballot_item(ballot_item_id,election_id,issue,victory_id) Values(3, 2, 'Governor', null);
Insert into item(item_id,title,party,ballot_id) Values(5, 'James Wilson','Democrat', 3);
Insert into item(item_id,title,party,ballot_id) Values(6, 'Emma Davis','Republican', 3);
Insert into item(item_id,title,party,ballot_id) Values(7, 'David Miller','Independent', 3);

Insert into vote_info(voter_id,election_id,votes) Values(2,2,'6');
Insert into vote_info(voter_id,election_id,votes) Values(1,2,'7');