CREATE TABLE [Security].[UserPermissions]
(
	  [UserPermissionId] INT NOT NULL IDENTITY(1, 1)
    , [UserId] UNIQUEIDENTIFIER NOT NULL
	, [PermissionId] INT NOT NULL

    , CONSTRAINT [PK_UserPermissions_UserPermissionId]
      PRIMARY KEY NONCLUSTERED ([UserPermissionId] ASC)

    , CONSTRAINT [UK_UserPermissions_UserId_PermissionId]
      UNIQUE ([UserId], [PermissionId])

    , CONSTRAINT [FK_UserPermissions_UserId]
      FOREIGN KEY ([UserId])
      REFERENCES [Security].[Users] ([UserId])
      ON UPDATE CASCADE
      ON DELETE CASCADE

    , CONSTRAINT [FK_UserPermissions_PermissionId]
      FOREIGN KEY ([PermissionId])
      REFERENCES [Security].[Permissions] ([PermissionId])
      ON UPDATE CASCADE
      ON DELETE CASCADE
);
