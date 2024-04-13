/* SQL Commands I have used:
    - specific names for pk and fk to be easy to identify (if not specified, SQL generated "random" ones)
    - the local database is added as well    
*/

CREATE TABLE [dbo].[Categories]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [CategoryName] NVARCHAR(50),
    [Description] NVARCHAR(250),
	CONSTRAINT [PK_Categories_Id] PRIMARY KEY CLUSTERED([Id] ASC)
)

CREATE TABLE [dbo].[Assets]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [AssetName] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(250),
    [CategoryId] INT NOT NULL,
    CONSTRAINT [FK_CategoryId] FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
	CONSTRAINT [PK_Assets_Id] PRIMARY KEY CLUSTERED([Id] ASC)
)


CREATE TABLE [dbo].[CabinAssets]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [AssetId] INT NOT NULL,
    [State] SMALLINT NOT NULL,
    [Description] NVARCHAR(250),
    [Address] NVARCHAR(250),
    [NoOfRooms] SMALLINT,
    [NoOfBathrooms] SMALLINT,
	CONSTRAINT [FK_Cabin_AssetId] FOREIGN KEY (AssetId) REFERENCES Assets(Id),
	CONSTRAINT [PK_CabinAssets_Id] PRIMARY KEY CLUSTERED([Id] ASC)
)

CREATE TABLE [dbo].[CarAssets]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    AssetId INT NOT NULL,
    State SMALLINT NOT NULL,
    Description NVARCHAR(MAX),
    Mark NVARCHAR(MAX),
    Model NVARCHAR(MAX),
    Year SMALLINT,
    Mileage INT,
	CONSTRAINT FK_Car_AssetId FOREIGN KEY (AssetId) REFERENCES Assets(Id),
	CONSTRAINT [PK_CarAssets_Id] PRIMARY KEY CLUSTERED([Id] ASC)
)