
IF OBJECT_ID('dbo.Team', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Team]

CREATE TABLE [dbo].[Team]
( [Year] INT NOT NULL
, [Number] INT NOT NULL
, [Name] VARCHAR(100) NOT NULL
, [DivisionCode] VARCHAR(20) NOT NULL )

IF OBJECT_ID('dbo.Racer', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Racer]
  
CREATE TABLE [dbo].[Racer]
( [Year] INT NOT NULL
, [TeamNumber] INT NOT NULL
, [Name] VARCHAR(100) NOT NULL
, [Leg] CHAR(2) NOT NULL
, [Sex] CHAR(1) NULL )

IF OBJECT_ID('dbo.Result', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Result]
  
CREATE TABLE [dbo].[Result]
( [Year] INT NOT NULL
, [TeamNumber] INT NOT NULL
, [Leg] TIME NOT NULL
, [FinishTime] TIME NULL
, [Place] INT NULL
, [DivisionPlace] INT NULL )

IF OBJECT_ID('dbo.Time', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Time]
  
CREATE TABLE [dbo].[Time]
( [Year] INT NOT NULL
, [TeamDisqualified] BIT NOT NULL DEFAULT 0
, [LastLegFinished] VARCHAR(2) NULL
, [IsTeamTime] BIT NOT NULL DEFAULT 0
, [TeamNumber] INT NOT NULL
, [Leg] VARCHAR(2) NOT NULL
, [StartTime] DATETIME NOT NULL
, [FinishTime] DATETIME NOT NULL
, [Duration] TIME NULL
, [PlaceAfter] INT NULL
, [Place] INT NULL
, [Disqualified] BIT NOT NULL DEFAULT 0
, [EarlyRelease] BIT NOT NULL DEFAULT 0
, [DivisionPlace] INT NULL )

IF OBJECT_ID('dbo.Division', 'U') IS NOT NULL 
  DROP TABLE [dbo].[Division]
  
CREATE TABLE [dbo].[Division]
( [Year] INT NOT NULL
, [Code] VARCHAR(2) NOT NULL
, [Name] VARCHAR(50) NOT NULL )