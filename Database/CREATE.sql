CREATE DATABASE Dictionary;
USE Dictionary;

CREATE TABLE Users(
	UserId varchar(6) primary key,
	Username varchar(30) not null,
	Pswd varchar(128) not null	-- hash value
);
CREATE TABLE History(
	HistoryId varchar(7) primary key,
	UserId varchar(6) foreign key references Users(UserId) not null,
	EncodedWord varchar(30) not null,
	DecodedWord varchar(5000) not null,
	CreatedTime datetime not null
);




