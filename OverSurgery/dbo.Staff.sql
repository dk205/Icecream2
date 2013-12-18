CREATE TABLE [dbo].[Staff] (
    [StaffID]          INT           IDENTITY (1, 1) NOT NULL,
    [Surname]          NVARCHAR (50) NOT NULL,
    [First Name]       NVARCHAR (50) NOT NULL,
    [Sex]              NCHAR(10)    NOT NULL ,
    [Staff Role/Title] NVARCHAR (50) NOT NULL,
    [Contact Number]   NUMERIC (18)  NOT NULL,
    PRIMARY KEY CLUSTERED ([StaffID] ASC)
);

