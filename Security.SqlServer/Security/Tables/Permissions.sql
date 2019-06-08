CREATE TABLE [Security].[Permissions]
(
	  [PermissionId] INT NOT NULL IDENTITY(1, 1)
	, [Name] VARCHAR(128) NOT NULL
    , [Description] VARCHAR(512) NULL

    , CONSTRAINT [PK_Permissions_PermissionId]
      PRIMARY KEY NONCLUSTERED ([PermissionId] ASC)
);
