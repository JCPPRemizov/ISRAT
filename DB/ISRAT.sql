CREATE TABLE [Roles](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] VARCHAR(255) NOT NULL,
[Description] TEXT NOT NULL
);

CREATE TABLE [Users](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] VARCHAR(255) NOT NULL,
[Surname] VARCHAR(255) NOT NULL,
[Middle_Name] VARCHAR(255) NOT NULL,
[Login] VARCHAR(20) NOT NULL UNIQUE,
[Password] VARCHAR(20) NOT NULL,
[Role_ID] INT REFERENCES Roles(ID) NOT NULL
);

CREATE TABLE [Status](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] VARCHAR(255) NOT NULL
);

CREATE TABLE [Projects](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] VARCHAR(255) NOT NULL,
[Description] TEXT NOT NULL
);

CREATE TABLE [Resources](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] VARCHAR(255) NOT NULL,
[Characteristics] TEXT NOT NULL,
[Quantity] INT NOT NULL
);

CREATE TABLE [Tasks](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] VARCHAR(255) NOT NULL,
[Description] TEXT NOT NULL,
[Priority] VARCHAR(255) NOT NULL,
[Status_ID] INT REFERENCES [Status](ID) NOT NULL,
[Responsible_user_ID] INT REFERENCES [Users](ID) NOT NULL,
[Start_date] DATE NOT NULL,
[Due_date] DATE NOT NULL,
[Project_ID] INT REFERENCES [Projects](ID) NOT NULL
);

CREATE TABLE [ResourcesAllocation](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Task_ID] INT REFERENCES [Tasks](ID) NOT NULL,
[Resource_ID] INT REFERENCES [Resources](ID) NOT NULL,
[Quantity] INT NOT NULL
);

CREATE TABLE [ResourcesOrder](
[ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Resource_ID] INT REFERENCES [Resources](ID) NOT NULL,
[Quantity] INT NOT NULL,
[Task_ID] INT REFERENCES [Tasks](ID) NOT NULL,
[Status_ID] INT REFERENCES [Status](ID) NOT NULL,
[User_ID] INT REFERENCES [Users](ID) NOT NULL
);