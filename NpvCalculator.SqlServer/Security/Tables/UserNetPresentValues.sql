CREATE TABLE [Security].[UserNetPresentValues]
(
      [UserNetPresentValueId] INT NOT NULL IDENTITY(1, 1)
    , [UserId] UNIQUEIDENTIFIER NOT NULL
	, [NetPresentValueId] INT NOT NULL

    , CONSTRAINT [PK_UserNetPresentValues_UserNetPresentValueId]
      PRIMARY KEY NONCLUSTERED ([UserNetPresentValueId] ASC)

    , CONSTRAINT [UK_UserNetPresentValues_UserId_NetPresentValueId]
      UNIQUE ([UserId], [NetPresentValueId])

    , CONSTRAINT [FK_UserNetPresentValues_UserId]
      FOREIGN KEY ([UserId])
      REFERENCES [Security].[Users] ([UserId])
      ON UPDATE CASCADE
      ON DELETE NO ACTION

    , CONSTRAINT [FK_UserNetPresentValues_NetPresentValueId]
      FOREIGN KEY ([NetPresentValueId])
      REFERENCES [Calculator].[NetPresentValues] ([NetPresentValueId])
      ON UPDATE CASCADE
      ON DELETE NO ACTION
)
