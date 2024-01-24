-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-01-20 18:05:07.726

-- tables
-- Table: Observation
CREATE TABLE Observation (
    User_Id uniqueidentifier  NOT NULL,
    User_2_Id uniqueidentifier  NOT NULL,
    CONSTRAINT Observation_pk PRIMARY KEY  (User_Id,User_2_Id)
);

-- Table: Opinion
CREATE TABLE Opinion (
    User_Id uniqueidentifier  NOT NULL,
    Thought_Id uniqueidentifier  NOT NULL,
    IsPositive bit  NOT NULL,
    CONSTRAINT Opinion_pk PRIMARY KEY  (User_Id,Thought_Id)
);

-- Table: Thought
CREATE TABLE Thought (
    Id uniqueidentifier  NOT NULL,
    Content varchar(500)  NOT NULL,
    User_Id uniqueidentifier  NOT NULL,
    Timestamp datetime  NOT NULL,
    Parent_Id uniqueidentifier  NULL,
    CONSTRAINT Thought_pk PRIMARY KEY  (Id)
);

-- Table: User
CREATE TABLE "User" (
    Id uniqueidentifier  NOT NULL,
    Username varchar(100)  NOT NULL,
    Email varchar(100)  NOT NULL,
    PasswordHash varchar(300)  NOT NULL,
    CONSTRAINT User_pk PRIMARY KEY  (Id)
);

-- foreign keys
-- Reference: Like_Thought (table: Opinion)
ALTER TABLE Opinion ADD CONSTRAINT Like_Thought
    FOREIGN KEY (Thought_Id)
    REFERENCES Thought (Id);

-- Reference: Like_User (table: Opinion)
ALTER TABLE Opinion ADD CONSTRAINT Like_User
    FOREIGN KEY (User_Id)
    REFERENCES "User" (Id);

-- Reference: Observe_User (table: Observation)
ALTER TABLE Observation ADD CONSTRAINT Observe_User
    FOREIGN KEY (User_Id)
    REFERENCES "User" (Id);

-- Reference: Observe_User2 (table: Observation)
ALTER TABLE Observation ADD CONSTRAINT Observe_User2
    FOREIGN KEY (User_2_Id)
    REFERENCES "User" (Id);

-- Reference: Thought_Thought (table: Thought)
ALTER TABLE Thought ADD CONSTRAINT Thought_Thought
    FOREIGN KEY (Parent_Id)
    REFERENCES Thought (Id);

-- Reference: Thought_User (table: Thought)
ALTER TABLE Thought ADD CONSTRAINT Thought_User
    FOREIGN KEY (User_Id)
    REFERENCES "User" (Id);

-- End of file.

-- Dodaj dane do tabeli User
INSERT INTO "User" (Id, Username, Email, PasswordHash)
VALUES (NEWID(), 'user1', 'user1@example.com', 'AQAAAAIAAYagAAAAENCOJ/V/jTadem0qbBXC8lvGXtFQq3UNr/CTJppQ2QysLOiymrZSmvVmjZPILaDUtw=='),
       (NEWID(), 'user2', 'user2@example.com', 'AQAAAAIAAYagAAAAENCOJ/V/jTadem0qbBXC8lvGXtFQq3UNr/CTJppQ2QysLOiymrZSmvVmjZPILaDUtw=='),
       (NEWID(), 'user3', 'user3@example.com', 'AQAAAAIAAYagAAAAENCOJ/V/jTadem0qbBXC8lvGXtFQq3UNr/CTJppQ2QysLOiymrZSmvVmjZPILaDUtw==');

-- Dodaj dane do tabeli Thought
INSERT INTO Thought (Id, Content, User_Id, Timestamp, Parent_Id)
VALUES (NEWID(), 'Thought 1', (SELECT Id FROM "User" WHERE Username = 'user1'), GETDATE(), NULL),
       (NEWID(), 'Thought 2', (SELECT Id FROM "User" WHERE Username = 'user2'), GETDATE(), NULL),
       (NEWID(), 'Thought 3', (SELECT Id FROM "User" WHERE Username = 'user3'), GETDATE(), NULL);

-- Dodaj dane do tabeli Opinion
INSERT INTO Opinion (User_Id, Thought_Id, IsPositive)
VALUES ((SELECT Id FROM "User" WHERE Username = 'user1'), (SELECT Id FROM Thought WHERE Content = 'Thought 2'), 1),
       ((SELECT Id FROM "User" WHERE Username = 'user2'), (SELECT Id FROM Thought WHERE Content = 'Thought 3'), 0),
       ((SELECT Id FROM "User" WHERE Username = 'user3'), (SELECT Id FROM Thought WHERE Content = 'Thought 1'), 1);