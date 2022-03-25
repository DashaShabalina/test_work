--CREATE DATABASE Task1;

USE Task1;

CREATE TABLE Equipment(
	id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	name NVARCHAR(80) NOT NULL 
);

GO

CREATE TABLE Location (
	id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	id_equipment INT REFERENCES Equipment (id),
	latitude float NOT NULL CHECK(latitude>=-90 AND latitude<=90),
	longitude float NOT NULL CHECK(longitude>=-180 AND longitude<=180),
);